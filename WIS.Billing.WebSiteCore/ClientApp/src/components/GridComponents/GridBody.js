import React, { Component } from 'react';
import { Row } from './GridRow';
import { RowSpanContainer } from './GridRowSpanContainer';
import withScrollContext from './WithScrollContext';

class InternalBody extends Component {
    getRows = () => {
        if (this.props.rows.length === 0 || this.props.columns.length === 0)
            return null;

        return this.props.rows.map(row => (
            <Row
                isNew={row.isNew}
                isDeleted={row.isDeleted}
                key={row.id}
                id={row.id}
                index={row.index}
                cells={row.cells}
                columns={this.props.columns}
                highlight={this.props.highlight}                
                addCellHighlight={this.props.addCellHighlight}
                addCellHighlightGroup={this.props.addCellHighlightGroup}
                cellDisplayBorderLeft={this.props.cellDisplayBorderLeft}
                updateCellValue={this.props.updateCellValue}
                performButtonAction={this.props.performButtonAction}
                openDropdown={this.props.openDropdown}
            />
        ));
    }

    getBodyStyle = () => {
        return {
            height: "calc(100% + " + (this.props.scrollContext.scrollbarWidth === 0 ? 60 : this.props.scrollContext.scrollbarWidth) + "px)"
        };
    }
    getEmptyBodyStyle = () => {
        return {
            height: "calc(40px + " + (this.props.scrollContext.scrollbarWidth === 0 ? 60 : this.props.scrollContext.scrollbarWidth) + "px)"
        };
    }

    getContent = () => {
        const rows = this.getRows();

        if (rows === null) {
            return (
                <div className="gr-body empty" style={this.getEmptyBodyStyle()} >
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
            <div className="gr-body-scroll">
                {this.getContent()}
            </div>
        );
    }
}

export const Body = withScrollContext(InternalBody);