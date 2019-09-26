import React, { Component } from 'react';
import { Row } from './GridRow';
import { RowSpanContainer } from './GridRowSpanContainer';
import ScrollSyncPane from '../ScrollSyncPane';
import withScrollContext from './WithScrollContext';

class InternalBodyAdaptive extends Component {
    constructor(props) {
        super(props);
    }

    shouldComponentUpdate(nextProps) {
        return !nextProps.scrollContext.isScrolling;
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

    getRowPlaceholderStyle = () => {
        return {
            width: this.props.columns.reduce((prevVal, currVal) => prevVal + currVal.width, 0)
        };
    }

    getContent() {
        const rows = this.getRows();

        if (rows === null) {
            return (
                <div className="gr-body empty" style={this.getEmptyBodyStyle()} >
                    <div className="gr-row-placeholder" style={this.getRowPlaceholderStyle()} >
                        <span><i className="fas fa-exclamation-triangle" /> Sin resultados</span>
                    </div>
                </div>
            );
        }

        return (
            <ScrollSyncPane group="horizontal">
                <div className="gr-body" style={this.getBodyStyle()}>
                    <RowSpanContainer height={this.props.rowTopSpan} />
                    {rows}
                    <RowSpanContainer height={this.props.rowBottomSpan} />
                </div>
            </ScrollSyncPane>
        );
    }
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
                disabledButtons={row.disabledButtons}
                highlight={this.props.highlight}
                addCellHighlight={this.props.addCellHighlight}
                addCellHighlightGroup={this.props.addCellHighlightGroup}
                updateCellValue={this.props.updateCellValue}
                performButtonAction={this.props.performButtonAction}
                openDropdown={this.props.openDropdown}
            />
        ));
    }

    render() {        
        return (
            <div
                className="gr-body-scroll"       
                ref={this.bodyRef}                
            >
                {this.getContent()}
            </div>
        );
    }
}

export const BodyAdaptive = withScrollContext(InternalBodyAdaptive);