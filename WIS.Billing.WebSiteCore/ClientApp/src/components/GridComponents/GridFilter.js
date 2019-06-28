import React, { Component } from 'react';
import { FilterText } from './GridFilterText';
import { filterStatus } from '../Enums';

export class Filter extends Component {
    constructor(props) {
        super(props);

        this.inputRef = React.createRef();
    }

    shouldComponentUpdate(nextProps) {
        return this.props.filterStatus !== nextProps.filterStatus
            || this.props.filters !== nextProps.filters
            || this.props.width !== nextProps.width
            || this.props.highlightLast !== nextProps.highlightLast;
    }    

    getValue = () => {
        if (this.props.filterStatus === filterStatus.review) {
            const filter = this.props.filters.find(f => f.columnId === this.props.columnId);
            
            return filter ? filter.value : null;
        }

        return null;
    }
    getClassName = () => {
        const displayBorder = this.props.displayBorderLeft && this.props.isFirst ? " display-border-left" : "";

        return "gr-filter" + displayBorder;
    }

    render() {
        if (this.props.filterStatus === filterStatus.closed)
            return null;

        return (
            <div className={this.getClassName()} style={{ minWidth: this.props.width }}>
                <FilterText
                    columnId={this.props.columnId}
                    value={this.getValue()}
                    highlightLast={this.props.highlightLast}
                    updateFilter={this.props.updateFilter}
                    applyFilter={this.props.applyFilter}
                />
            </div>
        );
    }
}