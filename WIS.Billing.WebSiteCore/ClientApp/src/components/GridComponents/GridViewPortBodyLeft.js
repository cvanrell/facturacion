import React, { Component } from 'react';
import { Body } from './GridBody';
import withScrollContext from './WithScrollContext';
import { columnFixed } from '../Enums';

class InternalViewPortBodyLeft extends Component {
    shouldComponentUpdate(nextProps) {
        return !nextProps.scrollContext.isScrolling;
    }

    getColumns = () => {
        return this.props.columns.filter(d => d.fixed === columnFixed.left);
    }

    getStyle = () => {
        return {
            minWidth: this.props.widthLeft,
            maxWidth: this.props.widthLeft
        };
    }

    render() {
        const columns = this.getColumns();

        return (
            <div className="gr-vp-body-left" style={this.getStyle(columns)}>
                <Body
                    rows={this.props.rows}
                    columns={columns}
                    highlight={this.props.highlight}
                    hasFixedColumnsLeft={this.props.hasFixedColumnsLeft}
                    hasFixedColumnsRight={this.props.hasFixedColumnsRight}
                    addCellHighlight={this.props.addCellHighlight}
                    addCellHighlightGroup={this.props.addCellHighlightGroup}
                    isScrolling={this.props.scrollContext.isScrolling}
                    updateCellValue={this.props.updateCellValue}
                    performButtonAction={this.props.performButtonAction}
                    openDropdown={this.props.openDropdown}
                    totalHeight={this.props.totalHeight}
                    rowTopSpan={this.props.rowTopSpan}
                    rowBottomSpan={this.props.rowBottomSpan}
                    cellDisplayBorderLeft={this.props.cellDisplayBorderLeft}
                />
            </div>
        );
    }
}

export const ViewPortBodyLeft = withScrollContext(InternalViewPortBodyLeft);