import React, { Component } from 'react';
import { Column } from './GridColumn';
import { Filter } from './GridFilter';

export class Header extends Component {
    constructor(props) {
        super(props);

        this.headerRef = React.createRef();
    }

    shouldComponentUpdate(nextProps) {
        return this.props.columns !== nextProps.columns
            || this.props.filterStatus !== nextProps.filterStatus
            || this.props.filters !== nextProps.filters
            || this.props.sorts !== nextProps.sorts
            || this.props.highlightLast !== nextProps.highlightLast;
    }
    
    getScrollLeft = () => {
        if (this.headerRef.current)
            return this.headerRef.current.scrollLeft;

        return 0;
    }

    getColumns = () => {
        return this.props.columns.map((col, index) => (
            <Column
                key={col.id}
                columnId={col.id}
                name={col.name}
                width={col.width}
                order={col.order}
                sorts={this.props.sorts}                
                displayBorderLeft={this.props.displayBorderLeft}
                applySort={this.props.applySort}
                getScrollLeft={this.getScrollLeft}
                resizeLeft={this.props.resizeLeft}
                columnResizeBegin={this.props.columnResizeBegin}
                updateColumnOrder={this.props.updateColumnOrder}
                isFirst={index === 0}
                isResizing={this.props.isResizing}
            />
        ));
    }
    getFilters = () => {
        return this.props.columns.map((col, index) => (
            <Filter
                key={col.id}
                columnId={col.id}
                width={col.width}
                filters={this.props.filters}
                filterStatus={this.props.filterStatus}
                isFirst={index === 0}
                displayBorderLeft={this.props.displayBorderLeft}
                highlightLast={this.props.highlightLast}
                updateFilter={this.props.updateFilter}
                applyFilter={this.props.applyFilter}
            />
        ));
    }

    render() {
        return (
            <div className="gr-header" ref={this.headerRef}>
                <div className="gr-header-row">
                    {this.getColumns()}
                </div>
                <div className="gr-filter-row">
                    {this.getFilters()}
                </div>
            </div>
        );       
    }
}