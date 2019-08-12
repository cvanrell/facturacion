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

    getLoadingSpinner = () => {
        if (this.props.isLoading) {
            return (
                <Spinner animation="border" role="status" size="sm">
                    <span className="sr-only">Loading...</span>
                </Spinner>
            );
        }

        return null;
    }

    render() {
        const disabled = this.props.formik.isSubmitting || this.props.isLoading;

        return (
            <button
                type="button"
                className="btn btn-primary"
                onClick={this.handleClick}
                disabled={disabled}
                {...this.getProps()}
            >
                {this.props.value}
                {this.getLoadingSpinner()}
            </button>
        );
    }
}

export const Button = withFormContext(connect(InternalButton));

