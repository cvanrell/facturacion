import React, { Component } from 'react';
import { connect, getIn } from 'formik-custom';

export class InternalStatusMessage extends Component {
    isErrorEmptyObject(error) {
        return (typeof error === "object") && !Object.keys(error).length;
    }

    render() {
        const error = getIn(this.props.formik.errors, this.props.for);
        const touch = getIn(this.props.formik.touched, this.props.for);

        return (
            <div className="invalid-feedback">
                {error && !this.isErrorEmptyObject(error) && touch ? error : ''}
            </div>
        );
    }
}

export const StatusMessage = connect(InternalStatusMessage);