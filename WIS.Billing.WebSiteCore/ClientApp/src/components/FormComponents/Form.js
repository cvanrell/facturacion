import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Field } from './FormField';
import { FieldSelect } from './FormSelect';
import { FieldSelectAsync } from './FormSelectAsync';
import { FieldSelectLegacy } from './FormSelectLegacy';
import { FieldDate } from './FormFieldDate';
import { FieldDateTime } from './FormFieldDateTime';
import { StatusMessage } from './FormStatusMessage';
import { SubmitButton } from './FormSubmitButton';
import { Button } from './FormButton';
import { Formik } from 'formik-custom';
import withFormDataProvider from './WithFormDataProvider';
import { FormCore } from './FormCore';

export class InternalForm extends Component {
    static propTypes = {
        initialValues: PropTypes.object,
        onBeforeInitialize: PropTypes.func,
        onAfterInitialize: PropTypes.func,
        onBeforeValidateField: PropTypes.func,
        onAfterValidateField: PropTypes.func,
        onBeforeSubmit: PropTypes.func,
        onAfterSubmit: PropTypes.func,
        onBeforeButtonAction: PropTypes.func,
        onAfterButtonAction: PropTypes.func
    }

    static defaultProps = {
        validationSchema: {}
    }

    constructor(props) {
        super(props);

        this.submitHandler = null;
        
    }

    registerSubmitHandler = (handler) => {
        this.submitHandler = handler;
    }

    submitCaller = async (values, actions) => {
        return await this.submitHandler(values, actions);
    }

    render() {
        return (
            <Formik
                initialValues={this.props.initialValues}
                validateOnChange={false}
                onSubmit={this.submitCaller}
                render={
                    formikProps => (
                        <FormCore
                            id={this.props.id}
                            formik={formikProps}
                            validationSchema={this.props.validationSchema}
                            registerSubmitHandler={this.registerSubmitHandler}
                            formInitialize={this.props.formInitialize}
                            formValidateField={this.props.formValidateField}
                            formSubmit={this.props.formSubmit}
                            formPerformButtonAction={this.props.formPerformButtonAction}
                            onBeforeInitialize={this.props.onBeforeInitialize}
                            onAfterInitialize={this.props.onAfterInitialize}
                            onBeforeSubmit={this.props.onBeforeSubmit}
                            onAfterSubmit={this.props.onAfterSubmit}
                            onBeforeValidateField={this.props.onBeforeValidateField}
                            onAfterValidateField={this.props.onAfterValidateField}
                            onBeforeButtonAction={this.props.onBeforeButtonAction}
                            onAfterButtonAction={this.props.onAfterButtonAction}
                        >
                            {this.props.children}
                        </FormCore>
                    )                            
                }
            />
        );
    }    
}

export const Form = withFormDataProvider(InternalForm);

export {
    Field,
    FieldSelect,
    FieldSelectAsync,
    FieldSelectLegacy,
    FieldDate,
    FieldDateTime,
    SubmitButton,
    Button,
    StatusMessage
};