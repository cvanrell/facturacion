import React, { Component } from 'react';
import { ViewPortHeaderLeft } from './GridViewPortHeaderLeft';
import { ViewPortHeaderCenter } from './GridViewPortHeaderCenter';
import { ViewPortHeaderRight } from './GridViewPortHeaderRight';
import { ViewPortHeaderSelection } from './GridViewPortHeaderSelection';
import { Header } from './GridHeader';
import { HeaderSelection } from './GridHeaderSelection';
import { columnFixed } from '../Enums';

export class HeaderPane extends Component {
    shouldComponentUpdate(nextProps) {
        return !nextProps.isResizing;
    }

    getColumnsCenter() {
        return this.props.columns.filter(d => d.fixed === columnFixed.none);
    }
    getColumnsFixedLeft() {
        return this.props.columns.filter(d => d.fixed === columnFixed.left);
    }
    getColumnsFixedRight() {
        return this.props.columns.filter(d => d.fixed === columnFixed.right);
    }

    getFilters(columns) {
        if (columns && this.props.filters) {
            return this.props.filters.filter(f => columns.some(col => col.id === f.columnId));
        }

        return null;
    }
       
    getViewPortHeaderSelectionContent() {
        if (!this.props.enableSelection)
            return null;

        return (
            <HeaderSelection
                selection={this.props.selection}
                filterStatus={this.props.filterStatus}
                isSelectionInverted={this.props.isSelectionInverted}
                invertSelection={this.props.invertSelection}
                updateFilter={this.props.updateFilter}
                applyFilter={this.props.applyFilter}
            />
        );
    }

    shouldDisplayBorderLeft() {
        return !this.props.enableSelection;
    }

    render() {
        const columnsCenter = this.getColumnsCenter();
        const columnsFixedLeft = this.getColumnsFixedLeft();
        const columnsFixedRight = this.getColumnsFixedRight();        

        const filtersCenter = this.getFilters(columnsCenter);
        const filtersFixedLeft = this.getFilters(columnsFixedLeft);
        const filtersFixedRight = this.getFilters(columnsFixedRight);

        return (
            <div className="gr-header-pane">
                <ViewPortHeaderSelection>
                    {this.getViewPortHeaderSelectionContent()}
                </ViewPortHeaderSelection>
                <ViewPortHeaderLeft width={this.props.widthLeft}>
                    <Header
                        columns={columnsFixedLeft}
                        filters={filtersFixedLeft}
                        filterStatus={this.props.filterStatus}
                        highlightLast={this.props.highlightLast}
                        sorts={this.props.sorts}
                        displayBorderLeft={this.shouldDisplayBorderLeft()}
                        columnResizeBegin={this.props.columnResizeBegin}
                        updateFilter={this.props.updateFilter}
                        updateColumnOrder={this.props.updateColumnOrder}
                        applyFilter={this.props.applyFilter}
                        applySort={this.props.applySort}
                        isResizing={this.props.isResizing}
                    />
                </ViewPortHeaderLeft>
                <ViewPortHeaderCenter>                    
                    <Header
                        columns={columnsCenter}
                        filters={filtersCenter}
                        filterStatus={this.props.filterStatus}
                        highlightLast={this.props.highlightLast}
                        sorts={this.props.sorts}
                        columnResizeBegin={this.props.columnResizeBegin}
                        updateFilter={this.props.updateFilter}
                        updateColumnOrder={this.props.updateColumnOrder}
                        applyFilter={this.props.applyFilter}
                        applySort={this.props.applySort}
                        isResizing={this.props.isResizing}
                    />
                </ViewPortHeaderCenter>
                <ViewPortHeaderRight
                    width={this.props.widthRight}
                    isVScrollActive={this.props.isVScrollActive}
                >
                    <Header
                        resizeLeft
                        columns={columnsFixedRight}
                        filters={filtersFixedRight}
                        filterStatus={this.props.filterStatus}
                        highlightLast={this.props.highlightLast}
                        sorts={this.props.sorts}
                        columnResizeBegin={this.props.columnResizeBegin}                                                
                        updateFilter={this.props.updateFilter}
                        updateColumnOrder={this.props.updateColumnOrder}
                        applyFilter={this.props.applyFilter}
                        applySort={this.props.applySort}
                        isResizing={this.props.isResizing}                        
                    />
                </ViewPortHeaderRight>
            </div>
        );
    }
}