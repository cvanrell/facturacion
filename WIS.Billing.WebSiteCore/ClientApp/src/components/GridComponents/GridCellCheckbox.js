import React, { Component } from 'react';
import InputCheckbox from './GridInputCheckbox';

export class CellCheckbox extends Component {
    shouldComponentUpdate(nextProps) {
        return this.props.content.value !== nextProps.content.value
            || this.props.content.old !== nextProps.content.old;
    }

    handleChange = (event) => {
        if (this.props.content.editable) {
            const value = event.target.checked ? "S" : "N";

            this.props.updateCellValue(this.props.rowId, this.props.column.id, value);
        }
    }

    isChecked = () => {
        if (this.props.content.value)
            return this.props.content.value.toUpperCase() === "S";

        return false;
    }

    render() {
        return (
            <InputCheckbox isChecked={this.isChecked()} onChange={this.handleChange} />
        );
    }
}