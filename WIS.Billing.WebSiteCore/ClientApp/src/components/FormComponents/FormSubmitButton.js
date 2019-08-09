import React, { Component } from 'react';
import { connect } from 'formik-custom';

export class InternalSubmitButton extends Component {

    //getLoadingSpinner = () => {
    //    if (this.props.formik.isSubmitting) {
    //        return (
    //            <Spinner animation="border" role="status" size="sm">
    //                <span className="sr-only">Loading...</span>
    //            </Spinner>
    //        );
    //    }

    //    return null;
    //}

    render() {
        //const className = this.props.className ? this.props.className : "btn btn-primary";

        //return (
        //    <button type="submit" className={className} disabled={this.props.formik.isSubmitting}>{this.props.value}</button>
        //);
        const disabled = this.props.formik.isSubmitting;

        const className = this.props.className ? this.props.className : "btn btn-primary";

        return (
            <button
                type="submit"
                className={className}
                disabled={disabled}
            >
                {this.props.value}
                
            </button>
        );
    }
}

export const SubmitButton = connect(InternalSubmitButton);

