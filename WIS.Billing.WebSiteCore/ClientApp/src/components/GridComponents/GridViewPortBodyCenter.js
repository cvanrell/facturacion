import React, { Component } from 'react';
import { BodyAdaptive } from './GridBodyAdaptive';
import withScrollContext from './WithScrollContext';
import { columnFixed } from '../Enums';

class InternalViewPortBodyCenter extends Component {
    shouldComponentUpdate(nextProps) {
        return !nextProps.scrollContext.isScrolling;
    }

    getColumns = () => {
        return this.props.columns.filter(d => d.fixed === columnFixed.none);
    }

    render() {
        return (
            <div className="gr-vp-body-center">
                <BodyAdaptive
                    rows={this.props.rows}
                    columns={this.getColumns()}
                    highlight={this.props.highlight}
                    hasFixedColumnsLeft={this.props.hasFixedColumnsLeft}
                    hasFixedColumnsRight={this.props.hasFixedColumnsRight}
                    addCellHighlight={this.props.addCellHighlight}
                    addCellHighlightGroup={this.props.addCellHighlightGroup}
                    perfScrollbarUpdate={this.props.scrollContext.perfScrollbarUpdate}
                    rowDisplayStart={this.props.rowDisplayStart}
                    rowDisplayEnd={this.props.rowDisplayEnd}
                    totalHeight={this.props.totalHeight}
                    rowTopSpan={this.props.rowTopSpan}
                    rowBottomSpan={this.props.rowBottomSpan}
                    rowTotalRows={this.props.rowTotalRows}
                    updateScrollPosition={this.props.updateScrollPosition}
                    updateCellValue={this.props.updateCellValue}
                    performButtonAction={this.props.performButtonAction}
                    openDropdown={this.props.openDropdown}
                    isScrolling={this.props.scrollContext.isScrolling}
                    isFetching={this.props.isFetching}
                />
            </div>
        );
    }
}

export const ViewPortBodyCenter = withScrollContext(InternalViewPortBodyCenter);