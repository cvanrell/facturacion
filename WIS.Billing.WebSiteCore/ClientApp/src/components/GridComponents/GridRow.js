import React, { Component } from 'react';
import { Cell } from './GridCell';

export class Row extends Component {
    shouldComponentUpdate(nextProps) {
        return this.props.cells !== nextProps.cells
            || this.isColumnDiff(this.props.columns, nextProps.columns)
            || this.isRowHighlighted(this.props.highlight)
            || this.isRowHighlighted(nextProps.highlight);
    }

    getHighlight = () => {
        return this.props.highlight.find(sel => sel.rowId === this.props.id);
    }   
    
    getCells = () => {
        const highlight = this.getHighlight();

        return this.props.columns.map((col, index) => {
            return (
                <Cell
                    key={col.id}
                    rowIsNew={this.props.isNew}
                    rowIsDeleted={this.props.isDeleted}
                    rowId={this.props.id}
                    rowIndex={this.props.index}
                    column={col}
                    content={this.props.cells.find(cell => cell.column === col.id)}
                    highlight={highlight}
                    addCellHighlight={this.props.addCellHighlight}
                    addCellHighlightGroup={this.props.addCellHighlightGroup}
                    cellDisplayBorderLeft={this.props.cellDisplayBorderLeft}
                    updateCellValue={this.props.updateCellValue}
                    performButtonAction={this.props.performButtonAction}
                    openDropdown={this.props.openDropdown}
                />
            );
        });
    }
    getClassName = () => {
        let rowClass = "gr-row";

        if (this.props.isNew) {
            rowClass = rowClass + " new";
        }

        if (this.props.isDeleted) {
            rowClass = rowClass + " deleted";
        }

        return rowClass;
    }

    isColumnDiff = (columnsA, columnsB) => {
        if ((!columnsA && columnsB) || (!columnsB && columnsA) || columnsA.length !== columnsB.length) {
            return true;
        }

        for (var i = 0; i < columnsA.length; i++) {
            if (columnsA[i].width !== columnsB[i].width || columnsA[i].id !== columnsB[i].id) {
                return true;
            }
        }

        return false;
    }
    isRowHighlighted = (highlight) => {
        return highlight.some(sel => sel.rowId === this.props.id);
    }

    render() {
        return (
            <div className={this.getClassName()} id={this.props.id}>
                {this.getCells()}
            </div>
        );
    }
}