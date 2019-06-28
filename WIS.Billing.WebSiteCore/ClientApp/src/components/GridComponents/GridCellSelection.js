import React, { Component } from 'react';

export class CellSelection extends Component {
    getClassName = () => {
        const checked = this.props.checked ? "checked" : "";

        return "gr-selection-checkbox " + checked;
    }
    getIcon = () => {
        if (this.props.checked) {
            return "fas fa-check-square";
        }

        return "fas fa-square";
    }

    render() {
        if (this.props.rowIsNew) {
            return <div className="gr-selection-placeholder" />;
        }

        return (
            <div className={this.getClassName()} onClick={this.props.handleClick}>
                <i className={this.getIcon()} />
            </div>
        );
    }
}