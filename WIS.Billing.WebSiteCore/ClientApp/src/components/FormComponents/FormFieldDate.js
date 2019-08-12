import React, { useEffect, useLayoutEffect, useRef } from 'react';
import { connect, getIn } from 'formik-custom';
import { withFormContext } from './WithFormContext';
import DatePicker from 'react-datepicker';
import debounce from "debounce-promise";

function FieldDateInternal(props) {
    const fieldData = {
        props: {
            name: props.name,
            hidden: props.hidden,
            readOnly: props.readOnly,
            disabled: props.disabled
        }
    };

    useLayoutEffect(() => props.formProps.registerField(fieldData), []);
    useEffect(() => () => props.formProps.unregisterField(props.name), []);

    const updateValue = (date) => {
        const isoDate = date.toISOString();

        let values = {
            [props.name]: isoDate
        };
        let errors = {
            [props.name]: undefined
        };
        let touched = {
            [props.name]: true
        };

        props.formik.setFieldValue(props.name, isoDate, false);

        setTimeout(() => {
            props.formProps.validateField(props.name, isoDate).catch(error => {
                errors[props.name] = error;
            }).then(d => {
                props.formik.setAllProperties(values, errors, touched);
            });
        }, 100); //Tengo que ejecutarlo despues del setState de formik, pero setFieldValue no retorna una promesa
    };

    const handleChange = debounce(async (value) => {
        return await updateValue(value);
    }, 1000, { leading: true });

    const fieldProps = props.formProps.getFieldProps(props.name);

    const error = getIn(props.formik.errors, props.name);
    const touch = getIn(props.formik.touched, props.name);
    const value = getIn(props.formik.values, props.name);

    const errorEmpty = touch && error && !(error && (typeof error === "object") && !Object.keys(error).length);

    const classValid = (touch && error && errorEmpty ? " is-invalid" : (errorEmpty ? '' : (touch ? " is-valid" : "")));

    const classNameInput = "form-control " + classValid;
    const classNameContainer = "form-field-date " + classValid;

    const dateValue = value ? new Date(value) : null;

    return (
        <div className={classNameContainer}>
            <DatePicker
                selected={dateValue}
                className={classNameInput}
                dateFormat="dd/MM/yyyy"
                onChange={handleChange}
                {...fieldProps}
            />
        </div>
    );
}

export const FieldDate = withFormContext(connect(FieldDateInternal));