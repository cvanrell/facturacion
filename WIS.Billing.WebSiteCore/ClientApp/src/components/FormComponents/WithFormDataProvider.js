import React, { Component } from 'react';

export default function withFormDataProvider(WrappedComponent) {
    return class WithFormDataProvider extends Component {
        initialize(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                form: data.form,
                query: data.query
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.form.id,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Form/Initialize", request).then(response => response.json());
        }
        validateField(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                form: data.form,
                query: data.query
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.form.id,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Form/ValidateField", request).then((response) => response.json());
        }        
        performButtonAction(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                form: data.form,
                query: data.query
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.form.id,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Form/ButtonAction", request).then((response) => response.json());
        }
        submit(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                form: data.form,
                query: data.query
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.form.id,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Form/Submit", request).then((response) => response.json());
        }

        selectSearch(data) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const requestData = {
                form: data.form,
                query: data.query
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    componentId: data.form.id,
                    application: pathName,
                    data: JSON.stringify(requestData)
                })
            };

            return fetch("api/Form/SelectSearch", request).then((response) => response.json());
        }

        render() {
            return (
                <WrappedComponent
                    formInitialize={this.initialize}
                    formValidateField={this.validateField}                    
                    formPerformButtonAction={this.performButtonAction}
                    formSubmit={this.submit}
                    formSelectSearch={this.selectSearch}
                    {...this.props}
                />
            );
        }
    };
}