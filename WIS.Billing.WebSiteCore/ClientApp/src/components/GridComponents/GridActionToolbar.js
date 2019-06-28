import React, { Component } from 'react';
import { ToolbarDivider } from './GridToolbarDivider';
import { ToolbarButton } from './GridToolbarButton';
import { ToolbarMenu } from './GridToolbarMenu';

export class ActionToolbar extends Component {
    render() {
        return (
            <div className="gr-action-toolbar">
                <ToolbarButton key="refreshButton" className="gr-toolbar-btn refresh" onClick={this.props.refresh} label="Actualizar" icon="fas fa-sync-alt" />
                <ToolbarDivider />
                <ToolbarButton key="commitButton" className="gr-toolbar-btn commit" onClick={this.props.commit} label="Confirmar cambios" icon="fas fa-check" />
                <ToolbarButton key="rollbackButton" className="gr-toolbar-btn rollback" onClick={this.props.rollback} label="Deshacer cambios" icon="fas fa-undo-alt" />
                <ToolbarDivider />
                <ToolbarButton key="deleteButton" className="gr-toolbar-btn delete" onClick={this.props.deleteRow} label="Eliminar fila" icon="fas fa-trash-alt" />
                <ToolbarButton key="addButton" className="gr-toolbar-btn add" onClick={this.props.addRow} label="Agregar fila" icon="fas fa-plus" />
                <ToolbarDivider />
                <ToolbarMenu key="selectionMenu" title="Acciones sobre selección" icon="fas fa-bars" items={this.props.menuItems} performMenuItemAction={this.props.performMenuItemAction}/>
            </div>
        );
    }
}