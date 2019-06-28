import React, { Component } from 'react';
import { CellSelection } from './GridCellSelection';

export class RowSelection extends Component {
    shouldComponentUpdate(nextProps) {
        return this.props.isSelectionInverted !== nextProps.isSelectionInverted
            || this.props.selection !== nextProps.selection;
    }

    handleClick = () => {
        this.props.updateSelection(this.props.id);
    }

    getContent = () => {
        return (
            <CellSelection
                rowIsNew={this.props.isNew}
                handleClick={this.handleClick}
                checked={this.isChecked()}
            />
        );
    }
    getClassName = () => {
        return "gr-row" + (this.props.isNew ? " new" : "");
    }

    isChecked = () => {
        const index = this.props.selection.indexOf(this.props.id);

        return (this.props.isSelectionInverted && index < 0)
            || (!this.props.isSelectionInverted && index >= 0);
    }

    render() {
        return (
            <div className={this.getClassName()} >
                {this.getContent()}
            </div>
        );
    }
}