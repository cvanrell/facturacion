import React, { Component } from 'react';

export class CellButton extends Component {
    shouldComponentUpdate(nextProps) {
        return this.props.content.value !== nextProps.content.value;
    }

    handleClick = (evt) => {
        evt.preventDefault();

        this.props.performButtonAction(this.props.rowId, this.props.column.id, evt.target.value);
    }

    getButtonContent = (btn) => {
        if (btn.cssClass) {
            return (
                <i className={btn.cssClass} />
            );
        }

        return btn.label;
    }    
    getButtons = () => {
        if (this.props.rowIsNew)
            return null;

        if (!this.props.column.buttons) {
            return null;
        }

        return this.props.column.buttons.map(btn => (
            <button key={btn.id} className="gr-btn" value={btn.id} onClick={this.handleClick} tooltip={btn.label}>
                {this.getButtonContent(btn)}
            </button>
        ));
    }
    
    render() {
        return (
            <div className="gr-btn-list">
                {this.getButtons()}
            </div>
        );
    }
}