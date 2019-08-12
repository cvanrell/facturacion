import React, { Component } from 'react';
import { FastField as FormikField, connect, getIn } from 'formik-custom';
import { withFormContext } from './WithFormContext';

export class InternalField extends Component {
    constructor(props) {
        super(props);

        props.formProps.registerField(this);
    }

    componentWillUnmount() {
        this.props.formProps.unregisterField(this.props.name);
    }

    handleValidate = (value) => {
        return this.props.formProps.validateField(this.props.name, value);
    }
    
    getProps = () => {
        const { formProps, formik, ...result } = this.props;

        return result;
    }
    getStatusClass = (error, touch) => {
        const errorEmpty = this.isErrorEmptyObject(error);

        if (this.props.readOnly || this.props.disabled)
            return "";

        return (touch && error && !errorEmpty ? "is-invalid" : (errorEmpty ? '' : (touch ? "is-valid" : "")));
    }

    isErrorEmptyObject(error) {
        //Si error es vacío, por lo general significa que la validación con Yup tiró excepción
        return error && (typeof error === "object") && !Object.keys(error).length;
    }

    render() {
        const error = getIn(this.props.formik.errors, this.props.name);
        const touch = getIn(this.props.formik.touched, this.props.name);
        const fieldProps = this.props.formProps.getFieldProps(this.props.name);

        const className = this.props.className + " form-control " + this.getStatusClass(error, touch);

        return (
            <FormikField
                {...this.getProps()}
                {...fieldProps}
                className={className}
                validate={this.handleValidate}
            />
        );
    }
}

export const Field = withFormContext(connect(InternalField));