import React, { Component } from 'react';
import { ActionToolbar } from './GridActionToolbar';
import { FilterToolbar } from './GridFilterToolbar';

export class Toolbar extends Component {
    render() {
        return (
            <div className="gr-toolbar">
                <ActionToolbar
                    menuItems={this.props.menuItems}
                    refresh={this.props.refresh}
                    commit={this.props.commit}
                    rollback={this.props.rollback}
                    deleteRow={this.props.deleteRow}
                    addRow={this.props.addRow}
                    performMenuItemAction={this.props.performMenuItemAction}
                />
                <FilterToolbar />
            </div>
        );
    }
}