import React, { Component } from 'react';
import { ViewPortBodyLeft } from './GridViewPortBodyLeft';
import { ViewPortBodyCenter } from './GridViewPortBodyCenter';
import { ViewPortBodyRight } from './GridViewPortBodyRight';
import { ViewPortBodySelection } from './GridViewPortBodySelection';
import withScrollContext from './WithScrollContext';

class InternalBodyPane extends Component {
    constructor(props) {
        super(props);

        this.bodyPaneRef = React.createRef();
    }

    shouldComponentUpdate(nextProps) {
        return !nextProps.isResizing;
    }
    componentDidUpdate(prevProps) {
        //Volver al principio de la lista cuando se recrean las filas
        if (this.bodyPaneRef.current && prevProps.rowTotalRows > this.props.rowTotalRows) {
            this.bodyPaneRef.current.scrollTop = 0;
        }
    }

    shouldCellDisplayBorderLeft = () => {
        return !this.props.enableSelection;
    }

    handleScroll = (evt) => {
        if (evt.target.className.indexOf("gr-body-pane") > -1) {
            this.props.updateScrollPosition(evt.target.scrollTop);
        }
    }
    handleMouseOver(evt) {
        //Manual, por motivos de performance
        let row = evt.target.closest(".gr-row");

        if (row) {
            let pane = row.closest(".gr-body-pane");

            var index = Array.prototype.indexOf.call(row.parentNode.children, row) + 1;

            let current = pane.querySelectorAll(".highlight");

            if (current) {
                for (var i = 0; i < current.length; i++) {
                    current[i].classList.toggle("highlight", false);
                }
            }

            const rowSelection = pane.querySelector(".gr-vp-body-selection .gr-row:nth-child(" + index + ")");
            const rowLeft = pane.querySelector(".gr-vp-body-left .gr-row:nth-child(" + index + ")");
            const rowCenter = pane.querySelector(".gr-vp-body-center .gr-row:nth-child(" + index + ")");
            const rowRight = pane.querySelector(".gr-vp-body-right .gr-row:nth-child(" + index + ")");

            if (rowSelection)
                rowSelection.classList.toggle("highlight", true);

            if(rowCenter)
                rowCenter.classList.toggle("highlight", true);

            if(rowLeft)
                rowLeft.classList.toggle("highlight", true);

            if(rowRight)
                rowRight.classList.toggle("highlight", true);
        }
    }
    handleMouseLeave(evt) {
        let current = evt.target.closest(".gr-body-pane").querySelectorAll(".highlight");

        if (current) {
            for (var i = 0; i < current.length; i++) {
                current[i].classList.toggle("highlight", false);
            }
        }
    }

    getRows = () => {
        let rowsToShow = [];

        if (this.props.rows.length > 0) {
            for (var i = this.props.rowDisplayStart; i < this.props.rowDisplayEnd; i++) {
                if(this.props.rows[i])
                    rowsToShow.push(this.props.rows[i]);
            }
        }

        return rowsToShow;
    }

    getBodyHeight = () => {
        if (this.props.rows.length === 0)
            return 40;

        return this.props.rows.length * this.props.rowHeight;
    }
    getTopRowSpan = () => {
        const value = this.props.rowDisplayStart * this.props.rowHeight;
        return value > 0 ? value : 0;
    }
    getBottomRowSpan = () => {
        const value = (this.props.rows.length - this.props.rowDisplayEnd) * this.props.rowHeight;
        return value > 0 ? value : 0;
    }
    getStyle = () => {
        return {
            maxHeight: this.props.maxHeight
        };
    }
    
    render() {
        const rows = this.getRows();
        const topRowSpan = this.getTopRowSpan();
        const bottomRowSpan = this.getBottomRowSpan();
        const bodyHeight = this.getBodyHeight();

        return (
            <div className="gr-body-pane"
                onMouseOver={this.handleMouseOver}
                onMouseLeave={this.handleMouseLeave}
                onScroll={this.handleScroll}
                style={this.getStyle()}
                ref={this.bodyPaneRef}
            >
                <div className="gr-body-pane-container" style={{ height: this.getBodyHeight() }} >
                    <ViewPortBodySelection
                        rows={rows}
                        selection={this.props.selection}
                        enableSelection={this.props.enableSelection}
                        updateSelection={this.props.updateSelection}
                        totalHeight={bodyHeight}
                        rowTopSpan={topRowSpan}
                        rowBottomSpan={bottomRowSpan}
                        isSelectionInverted={this.props.isSelectionInverted}
                    />
                    <ViewPortBodyLeft
                        columns={this.props.columns}
                        rows={rows}
                        highlight={this.props.highlight}
                        hasFixedColumnsLeft={this.props.hasFixedColumnsLeft}
                        hasFixedColumnsRight={this.props.hasFixedColumnsRight}
                        addCellHighlight={this.props.addCellHighlight}
                        addCellHighlightGroup={this.props.addCellHighlightGroup}
                        updateCellValue={this.props.updateCellValue}
                        performButtonAction={this.props.performButtonAction}
                        openDropdown={this.props.openDropdown}
                        cellDisplayBorderLeft={this.shouldCellDisplayBorderLeft()}
                        totalHeight={bodyHeight}
                        rowTopSpan={topRowSpan}
                        rowBottomSpan={bottomRowSpan}
                        widthLeft={this.props.widthLeft}
                    />
                    <ViewPortBodyCenter
                        columns={this.props.columns}
                        rows={rows}
                        highlight={this.props.highlight}
                        hasFixedColumnsLeft={this.props.hasFixedColumnsLeft}
                        hasFixedColumnsRight={this.props.hasFixedColumnsRight}
                        perfScrollbarUpdate={this.props.perfScrollbarUpdate}
                        addCellHighlight={this.props.addCellHighlight}
                        addCellHighlightGroup={this.props.addCellHighlightGroup}
                        totalHeight={bodyHeight}
                        rowTopSpan={topRowSpan}
                        rowBottomSpan={bottomRowSpan}
                        rowTotalRows={this.props.rowTotalRows}
                        updateScrollPosition={this.props.updateScrollPosition}
                        updateCellValue={this.props.updateCellValue}
                        openDropdown={this.props.openDropdown}
                        performButtonAction={this.props.performButtonAction}
                        isFetching={this.props.isFetching}
                    />
                    <ViewPortBodyRight
                        columns={this.props.columns}
                        rows={rows}
                        highlight={this.props.highlight}
                        hasFixedColumnsLeft={this.props.hasFixedColumnsLeft}
                        hasFixedColumnsRight={this.props.hasFixedColumnsRight}
                        addCellHighlight={this.props.addCellHighlight}
                        addCellHighlightGroup={this.props.addCellHighlightGroup}
                        updateCellValue={this.props.updateCellValue}
                        performButtonAction={this.props.performButtonAction}
                        openDropdown={this.props.openDropdown}
                        totalHeight={bodyHeight}
                        rowTopSpan={topRowSpan}
                        rowBottomSpan={bottomRowSpan}
                        widthRight={this.props.widthRight}
                    />
                </div>
            </div>            
        );
    }
}

export const BodyPane = withScrollContext(InternalBodyPane);