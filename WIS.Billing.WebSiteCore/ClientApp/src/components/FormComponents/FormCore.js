import React, { Component } from 'react';
import { Form as FormikForm, yupToFormErrors } from 'formik-custom';
import { FormContextProvider } from './WithFormContext';
import { withPageContext } from '../WithPageContext';
import { formTouchValue, formStatus, componentType } from '../Enums';
import * as Yup from 'yup';

class InternalFormCore extends Component {
    constructor(props) {
        super(props);

        this.fields = [];

        this.serverSideValidationSchema = this.getServerValidationSchema();
        this.validationSchema = Yup.object().shape(this.props.validationSchema);

        this.props.nexus.registerComponent(this.props.id, componentType.form, this.getApi());

        this.props.registerSubmitHandler(this.handleSubmit);
    }

    componentDidMount() {
        this.initialize();

        this.mounted = true;
    }
    componentWillUnmount() {
        this.props.nexus.unregisterComponent(this.props.id);

        this.mounted = false;
    }

    initialize = (parameters) => {
        console.log("initializing " + this.props.id);

        this.props.formik.setSubmitting(true);

        const data = {
            form: {
                id: this.props.id,
                fields: this.getFields()
            },
            query: {
                parameters: []
            }
        };

        if (parameters)
            data.query.parameters = [...parameters];

        const context = {
            abortServerCall: false,
            forceUpdateFields: false
        };

        if (this.props.onBeforeInitialize)
            this.props.onBeforeInitialize(context, data.form, data.query, this.props.nexus);

        if (context.forceUpdateFields)
            this.updateFields(data.form.fields);

        if (context.abortServerCall)
            return false;

        return this.props.formInitialize(data).then(this.initializeProcessResponse);
    }
    initializeProcessResponse = (response) => {
        try {
            const data = JSON.parse(response.Data);

            const context = {
                abortFieldUpdate: false
            };

            if (data) {
                if (this.props.onAfterInitialize)
                    this.props.onAfterInitialize(context, data.form, data.query, this.props.nexus);

                if (!context.abortFieldUpdate)
                    this.updateFields(data.form.fields, formTouchValue.clean);
            }

            this.props.formik.setSubmitting(false);
        }
        catch (ex) {
            //TODO: Mostrar mensaje error
            console.log(ex);
        }
    }

    reset = (parameters) => {
        this.props.formik.resetForm(null, () => {
            this.initialize(parameters);
        });
    }

    validateField = (name, value) => {
        const context = {
            formId: this.props.id,
            updateFields: this.updateFields,
            currentField: name,
            getFields: this.getFields,
            formValidateField: this.props.formValidateField,
            onBeforeValidateField: this.props.onBeforeValidateField,
            onAfterValidateField: this.props.onAfterValidateField,
            nexus: this.props.nexus,
            mounted: this.mounted
        };

        return Yup.reach(this.validationSchema, name).validate(value, {
            abortEarly: true
        })
            .then(res => {
                return this.serverSideValidationSchema.validate(value, { context: context }).catch(error => {
                    throw error;
                });
            })
            .catch(error => {
                throw this.parseError(error, name);
            })
            .then(res => {});
    }

    handleSubmit = async (values, actions) => {
        return await this.validationSchema.validate(values, { abortEarly: false })
            .then(this.submit)
            .catch(errors => {
                if (errors.inner) {
                    errors.inner.map(error => {
                        actions.setFieldError(error.path, error.message);
                        actions.setFieldTouched(error.path, true, false);

                        return error;
                    });
                }

                actions.setSubmitting(false);
            });
    }
    submit = async () => {
        console.log("submitting: " + this.props.id);

        const data = {
            form: {
                id: this.props.id,
                fields: this.getFields()
            },
            query: {
                parameters: []
            }
        };

        const context = {
            abortServerCall: false,
            forceUpdateFields: false
        };

        if (this.props.onBeforeSubmit)
            this.props.onBeforeSubmit(context, data.form, data.query, this.props.nexus);

        if (!this.mounted)
            return false;

        if (context.forceUpdateFields)
            this.updateFields(data.form.fields);

        if (context.abortServerCall)
            return false;

        return await this.props.formSubmit(data).then(this.submitProcessResponse);
    }
    submitProcessResponse = (response) => {
        try {
            const data = JSON.parse(response.Data);

            const context = {
                abortFieldUpdate: false
            };

            if (this.props.onAfterSubmit)
                this.props.onAfterSubmit(context, data.form, data.query, this.props.nexus);

            if (!this.mounted)
                return false;

            if (data.query.redirect)
                return this.props.nexus.redirect(data.query.redirect);

            if (data.form.fields.some(f => f.status === formStatus.error)) {
                if (!context.abortFieldUpdate)
                    this.updateFields(data.form.fields, formTouchValue.touch);
            }
            else if (data.query.resetForm) {
                return this.reset();
            }

            this.props.formik.setSubmitting(false);

            //TODO: Mensaje OK
        }
        catch (ex) {
            this.props.formik.setSubmitting(false);

            console.log(ex);
            //TODO: Mostrar mensaje error
        }        
    }

    performButtonAction = (btnId) => {
        console.log("performing button action " + this.props.id);

        const data = {
            form: {
                id: this.props.id,
                fields: this.getFields()
            },
            query: {
                buttonId: btnId,
                parameters: []
            }
        };

        const context = {
            abortServerCall: false,
            forceUpdateFields: false
        };

        if (this.props.onBeforeButtonAction)
            this.props.onBeforeButtonAction(context, data.form, data.query, this.props.nexus);

        if (!this.mounted)
            return false;

        if (context.forceUpdateFields)
            this.updateFields(data.form.fields);

        if (context.abortServerCall)
            return false;

        return this.props.formPerformButtonAction(data).then(this.performButtonActionProcessResponse);
    }
    performButtonActionProcessResponse = (response) => {
        try {
            const data = JSON.parse(response.Data);

            const context = {
                abortFieldUpdate: false
            };

            if (this.props.onAfterButtonAction)
                this.props.onAfterButtonAction(context, data.form, data.query, this.props.nexus);

            if (!this.mounted)
                return false;

            if (data.query.redirect)
                return this.props.nexus.redirect(data.query.redirect);

            if (!context.abortFieldUpdate)
                this.updateFields(data.form.fields);
        }
        catch (ex) {
            //TODO: Mostrar mensaje error
            console.log(ex);
        }
    }

    getServerValidationSchema = () => {
        return Yup.string().test("server-side-validation", "Ocurrió un error al validar los datos", this.serverValidation);
    }

    async serverValidation(value) {
        console.log("oops I server'd again");

        const data = {
            form: {
                id: this.options.context.formId,
                fields: this.options.context.getFields()
            },
            query: {
                fieldId: this.options.context.currentField,
                parameters: []
            }
        };
        
        const context = {
            abortServerCall: false,
            forceUpdateFields: false
        };

        if (this.options.context.onBeforeValidateField)
            this.options.context.onBeforeValidateField(context, data.form, data.query, this.options.context.nexus);

        if (!this.options.context.mounted)
            return true;

        if (context.forceUpdateFields)
            this.options.context.updateFields(data.form.fields);

        if (context.abortServerCall)
            return true;
        
        const result = await this.options.context.formValidateField(data).then((res) => JSON.parse(res.Data));

        const responseContext = {
            abortFieldUpdate: false,
            abortValidation: false
        };

        if (this.options.context.onAfterValidateField)
            this.options.context.onAfterValidateField(responseContext, result.form, result.query, this.options.context.nexus);

        if (!this.options.context.mounted || responseContext.abortValidation)
            return true;

        if (result.query.redirect)
            return this.options.context.nexus.redirect(result.query.redirect);

        if (result && result.form && result.query) {
            const field = result.form.fields.find(d => d.id === result.query.fieldId);

            if (result.form.fields && !responseContext.abortFieldUpdate) {
                this.options.context.updateFields(result.form.fields);
            }

            if (field.status === 1)
                return true;

            return this.createError({ path: this.path, message: field.errorMessage });
        }

        return false;
    }

    searchSelectValue = async (fieldId, value) => {
        try {
            const data = {
                form: {
                    id: this.props.id,
                    fields: this.getFields()
                },
                query: {
                    fieldId: fieldId,
                    searchValue: value,
                    parameters: []
                }
            };

            const context = {
                abortServerCall: false
            };

            if (this.props.onBeforeSelectSearch)
                this.props.onBeforeSelectSearch(context, data.form, data.query, this.props.nexus);

            if (!this.mounted)
                return false;

            if (context.abortServerCall)
                return false;

            const result = await this.props.formSelectSearch(data).then((res) => JSON.parse(res.Data));

            if (result.Status === "ERROR")
                throw new Error(result.Message);

            const responseContext = {
                abortFieldUpdate: false
            };

            if (this.props.onAfterSelectSearch)
                this.props.onAfterSelectSearch(responseContext, result.options, result.query, this.props.nexus);

            if (!this.mounted)
                return true;

            this.props.nexus.toastNotifications(result.notifications);

            return result.options;
        }
        catch (ex) {
            this.props.nexus.toastException(ex);
        }
    }


    updateFields = (fields, forceTouch, forceValue) => {

        this.fields = fields;
        let values = {};
        let errors = {};
        let touched = {};        


        fields.forEach(field => {
            if (this.fields && this.fields.some(d => d.id === field.id && (forceValue || d.value !== field.value))) {
                values[field.id] = field.value || "";
            }

            if (field.status === formStatus.error) {
                errors[field.id] = field.errorMessage;
            }

            if (forceTouch === formTouchValue.touch) {
                if (!field.readOnly && !field.disabled)
                    touched[field.id] = true;
            }
            else if (forceTouch === formTouchValue.clean) {
                touched[field.id] = false;
            }
        });

        this.fields = fields;

        console.log(this.fields);

        this.props.formik.setAllProperties(values, errors, touched);
    }
    registerField = (field) => {
        const data = {
            id: field.props.name,
            hidden: field.props.hidden || false,
            readOnly: field.props.readOnly || false,
            disabled: field.props.disabled
        };

        this.addOrUpdateField(data);
    };
    unregisterField = (name) => {
        this.removeField(name);
    };

    addOrUpdateField = (data) => {
        const fieldIndex = this.fields.findIndex(f => f.id === data.id);

        if (fieldIndex < 0) {
            this.fields = [
                ...this.fields,
                data
            ];
        }
        else {
            this.fields = [
                ...this.fields.slice(0, fieldIndex),
                data,
                ...this.fields.slice(fieldIndex + 1)
            ];
        }
    };
    removeField = (name) => {
        const fieldIndex = this.fields.findIndex(f => f.id === name);

        if (fieldIndex <= 0) {
            this.fields = [
                ...this.fields.slice(0, fieldIndex),
                ...this.fields.slice(fieldIndex + 1)
            ];
        }
    };
    getFields = () => {
        if (this.fields) {
            return this.fields.map(field => {
                return {
                    id: field.id,
                    hidden: field.hidden,
                    readOnly: field.readOnly,
                    disabled: field.disabled,
                    value: this.props.formik.values[field.id],
                    options: field.options
                };
            });
        }

        return [];
    }

    updateOptions = (fieldId, options) => {
        const field = this.fields.find(d => d.id === fieldId);

        if (field) {
            field.options = options;
        }
    }
    
    parseError = (error, name) => {
        error.path = name;

        if (error.inner.length > 0) {
            for (let i = 0; i < error.inner.length; i++) {
                error.inner[i].path = name;
            }
        }

        return yupToFormErrors(error)[name];
    }

    getInitialValues = () => {
        return Object.keys(this.fields).reduce((values, current) => {
            values[current.name] = "";

            return values;
        }, {});
    }    

    getFieldProps = (name) => {
        const field = this.fields.find(field => field.id === name);

        if (field) {
            return {
                hidden: field.hidden,
                readOnly: field.readOnly,
                disabled: field.disabled,
                options: field.options
            };
        }

        return {};
    }
    getFormProps = () => {
        return {
            validateField: this.validateField,
            registerField: this.registerField,
            unregisterField: this.unregisterField,
            getFieldProps: this.getFieldProps,
            performButtonAction: this.performButtonAction,
            searchSelectValue: this.searchSelectValue,
            updateOptions: this.updateOptions
        };
    }

    setFieldValue = (fieldId, value) => {
        this.props.formik.setFieldValue(fieldId, value, false);
    }

    getApi() {
        return {
            reset: this.reset,
            submit: this.submit,
            setFieldValue: this.setFieldValue,
            addOrUpdateField: this.addOrUpdateField
        };
    }

    render() {
        return (
            <FormContextProvider value={this.getFormProps()}>
                <FormikForm onSubmit={this.props.formik.handleSubmit}>
                    {this.props.children}
                </FormikForm>
            </FormContextProvider>
        );
    }
}

export const FormCore = withPageContext(InternalFormCore);
