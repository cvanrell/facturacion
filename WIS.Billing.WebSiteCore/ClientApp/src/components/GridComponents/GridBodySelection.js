import React, { Component } from 'react';
import { RowSelection } from './GridRowSelection';
import { RowSpanContainer } from './GridRowSpanContainer';

export class BodySelection extends Component {
    shouldComponentUpdate(nextProps) {
        return this.props.bodyStyle !== nextProps.bodyStyle
            || this.props.rowTopSpan !== nextProps.rowTopSpan
            || this.props.rowBottomSpan !== nextProps.rowBottomSpan;
    }

    getRows = () => {
        if (this.props.rows.length === 0)
            return null;

        return this.props.rows.map(row => (
            <RowSelection
                key={row.id}
                id={row.id}
                isNew={row.isNew}
                index={row.index}
                selection={this.props.selection}
                updateSelection={this.props.updateSelection}
                isSelectionInverted={this.props.isSelectionInverted}
            />
        ));
    }

    getBodyStyle = () => {
        return {
            height: this.props.totalHeight
        };
    }
    getContent = () => {
        const rows = this.getRows();

        if (rows === null) {
            return (
                <div className="gr-body empty">
                    <div className="gr-row-placeholder" />
                </div>
            );
        }

        return (
            <div className="gr-body" style={this.getBodyStyle()} >
                <RowSpanContainer height={this.props.rowTopSpan} />
                {this.getRows()}
                <RowSpanContainer height={this.props.rowBottomSpan} />
            </div>
        );
    }

    render() {
        return (
            <div className="gr-body-scroll" style={this.props.bodyStyle}>
                {this.getContent()}
            </div>
        );
    }
}