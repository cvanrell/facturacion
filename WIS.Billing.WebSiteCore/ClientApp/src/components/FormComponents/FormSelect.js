import React, { useEffect, useLayoutEffect, useRef } from 'react';
import { connect, getIn } from 'formik-custom';
import { withFormContext } from './WithFormContext';
import Select from 'react-select';
import { useTranslation } from 'react-i18next';

function FieldSelectInternal(props) {
    const { t } = useTranslation();

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
    const valueRef = useRef(null);

    const handleChange = (option) => {
        let values = {
            [props.name]: option.value
        };
        let errors = {
            [props.name]: undefined
        };
        let touched = {
            [props.name]: true
        };

        props.formik.setFieldValue(props.name, option.value, false);

        setTimeout(() => {
            props.formProps.validateField(props.name, option.value).catch(error => {
                errors[props.name] = error;
            }).then(d => {
                props.formik.setAllProperties(values, errors, touched);
            });
        }, 100); //Tengo que ejecutarlo despues del setState de formik, pero setFieldValue no retorna una promesa
    };

    const getNoOptionMessage = () => {
        return t("General_Sec0_lbl_SELECT_NO_OPTIONS");
    };

    const { options, ...fieldProps } = props.formProps.getFieldProps(props.name);
    let { className, ...elementProps } = props;

    const error = getIn(props.formik.errors, props.name);
    const touch = getIn(props.formik.touched, props.name);
    const value = getIn(props.formik.values, props.name);

    if (!props.readOnly && !props.disabled) {
        const errorEmpty = touch && error && !(error && (typeof error === "object") && !Object.keys(error).length);

        className = (className || "") + " form-select " + (touch && error && errorEmpty ? " is-invalid" : (errorEmpty ? '' : (touch ? " is-valid" : "")));
    }

    const selectedOption = options ? options.find(d => d.value === value) : null;

    valueRef.current = selectedOption ? selectedOption: null;

    console.log(selectedOption);

    if (fieldProps.readOnly) {
        return (
            <input
                className="form-control"
                readOnly
                value={selectedOption ? selectedOption.label : ""}
            />
        );
    }
    else {
        return (
            <Select
                className={className}
                options={options}
                onChange={handleChange}
                value={valueRef.current}
                captureMenuScroll
                isDisabled={fieldProps.disabled}
                placeholder={("Seleccionar")}
                //noOptionsMessage={getNoOptionMessage}
                {...fieldProps}
                {...elementProps}
            />
        );
    }

}

export const FieldSelect = withFormContext(connect(FieldSelectInternal));