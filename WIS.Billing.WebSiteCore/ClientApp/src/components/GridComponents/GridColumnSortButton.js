import React, { Component } from 'react';

export class ColumnSortButton extends Component {
    shouldComponentUpdate(nextProps) {
        return this.props.order !== nextProps.order
            || this.props.direction !== nextProps.direction;
    }

    handleClick = (evt) => {
        this.props.applySort(this.props.columnId);
    }

    render() {
        const className = "gr-col-sort " + (this.props.direction === 0 ? "" : this.props.direction === 1 ? "ascending" : "descending");

        return (
            <div className={className} onClick={this.handleClick}>
                <div className="gr-col-sort-marker-up">
                    <i className="fas fa-angle-up" />
                </div>
                <div className="gr-col-sort-marker-down">
                    <i className="fas fa-angle-down" />
                </div>
                <div className="gr-col-sort-order">
                    {this.props.order}
                </div>
            </div>
        );
    }
}