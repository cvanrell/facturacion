import React, { Component } from 'react';

export default function withGridDataProvider(WrappedComponent) {
    return class WithGridDataProvider extends Component {
        initialize(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                gridId: data.gridId,
                rowsToFetch: data.rowsToFetch,
                parameters: data.parameters
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.gridId,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Grid/Initialize", request).then((response) => response.json());
        }
        fetchRows(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                gridId: data.gridId,
                filters: data.filters,
                sorts: data.sorts,
                rowsToSkip: data.rowsToSkip,
                rowsToFetch: data.rowsToFetch,
                parameters: data.parameters
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.gridId,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Grid/FetchRows", request).then((response) => response.json());
        }
        validateRow(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {          
                gridId: data.gridId,
                row: data.row,
                parameters: data.parameters
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.gridId,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Grid/ValidateRow", request).then((response) => response.json());
        }
        commit(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {                
                gridId: data.gridId,
                rows: data.rows,
                query: {
                    filters: data.filters,
                    sorts: data.sorts,
                    rowsToFetch: data.rowsToFetch,
                    rowsToSkip: 0,
                    parameters: data.parameters
                }                
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.gridId,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Grid/Commit", request).then((response) => response.json());
        }
        buttonAction(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                gridId: data.gridId,
                buttonId: data.buttonId,
                row: data.row,
                column: data.columnId,
                parameters: data.parameters
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.gridId,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Grid/ButtonAction", request).then((response) => response.json());
        }
        menuItemAction(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                gridId: data.gridId,
                buttonId: data.buttonId,
                filters: data.filters,
                selection: data.selection,                
                parameters: data.parameters
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.gridId,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Grid/MenuItemAction", request).then((response) => response.json());
        }
        updateConfig(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                gridId: data.gridId,
                columns: data.columns,
                parameters: data.parameters
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.gridId,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Grid/UpdateConfig", request).then((response) => response.json());
        }

        render() {
            return (
                <WrappedComponent
                    gridInitialize={this.initialize}
                    gridFetchRows={this.fetchRows}
                    gridValidateRow={this.validateRow}
                    gridCommit={this.commit}
                    gridButtonAction={this.buttonAction}
                    gridMenuItemAction={this.menuItemAction}
                    gridUpdateConfig={this.updateConfig}
                    {...this.props}
                />
            );
        }
    };
}