import React, { Component } from 'react';
import { ToolbarDivider } from './GridToolbarDivider';
import { FilterToolbarStatus } from './GridFilterToolbarStatus';

export class FilterToolbar extends Component {
    render() {
        return (
            <div className="gr-filter-toolbar">
                <button key="filterSaveButton" className="gr-toolbar-btn" onClick={this.props.saveFilter} title="Guardar filtro">
                    <i className="fas fa-download" />
                </button>
                <button key="filterLoadButton" className="gr-toolbar-btn" onClick={this.props.loadFilter} title="Cargar filtro">
                    <i className="fas fa-upload" />
                </button>
                <ToolbarDivider />
                <FilterToolbarStatus />
            </div>
        );
    }
}