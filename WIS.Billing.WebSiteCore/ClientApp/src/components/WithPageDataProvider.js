import React, { Component } from 'react';

export default function withPageDataProvider(WrappedComponent) {
    return class WithPageDataProvider extends Component {
        performLoad(parameters) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const data = {
                parameters: parameters
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    application: pathName,
                    data: JSON.stringify(data)
                })
            };

            return fetch("api/Page/Load", request).then((response) => response.json());
        }
        performUnload(parameters) {
            const path = window.location.pathname;

            const pathName = path.substring(path.lastIndexOf('/') + 1);

            const data = {
                parameters: parameters
            };

            const request = {
                method: "POST",
                cache: "no-cache",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    application: pathName,
                    data: JSON.stringify(data)
                })
            };

            return fetch("api/Page/Unload", request).then((response) => response.json());
        }

        render() {
            return (
                <WrappedComponent
                    performLoad={this.performLoad}
                    performUnload={this.performUnload}
                    {...this.props}
                />
            );
        }
    };
}