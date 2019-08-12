import React, { Component } from 'react';
import { withFormContext } from './WithFormContext';
import { connect } from 'formik-custom';
import { Spinner } from 'react-bootstrap';

class InternalButton extends Component {
    handleClick = (evt) => {
        this.props.formProps.performButtonAction(this.props.id);
    }

    getProps = () => {
        const { formProps, isLoading, ...result } = this.props;

        return result;
    }

    render() {
        return (
            <button type="button" className="btn btn-primary" onClick={this.handleClick} disabled={this.props.formik.isSubmitting} {...this.getProps()}>{this.props.value}</button>
        );
    }
}

export const Button = withFormContext(connect(InternalButton));

