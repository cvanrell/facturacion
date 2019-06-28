import React, { Component } from 'react';
import { connect } from 'formik-custom';

export class InternalSubmitButton extends Component {
    render() {
        const className = this.props.className ? this.props.className : "btn btn-primary";

        return (
            <button type="submit" className={className} disabled={this.props.formik.isSubmitting}>{this.props.value}</button>
        );
    }
}

export const SubmitButton = connect(InternalSubmitButton);

