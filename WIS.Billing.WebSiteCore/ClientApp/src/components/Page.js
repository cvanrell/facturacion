﻿import React, { Component } from 'react';
import withPageDataProvider from './WithPageDataProvider';
import { PageContextProvider } from './WithPageContext';
import { componentType, notificationType } from './Enums';
import { withTranslation } from 'react-i18next';
import { toast } from 'react-toastify';

class InternalPage extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isLoadComplete: false
        };

        this.components = [];
    }

    componentDidMount() {
        this.onLoad();
    }
    componentWillUnmount() {
        this.onUnload();
    }

    onLoad() {
        if (!this.props.load) {
            this.setState({ isLoadComplete: true });

            return false;
        }

        let parameters = [];

        parameters = this.beforeLoad(parameters);

        this.props.performLoad(parameters)
            .then(response => {
                try {
                    let data = JSON.parse(response.Data);

                    this.afterLoad(data);

                    this.setState({ isLoadComplete: true });
                }
                catch (ex) {
                    this.toastException(ex);
                }
            });
    }
    onUnload() {
        if (!this.props.unload) {
            this.setState({ isLoadComplete: true });

            return false;
        }

        let parameters = [];

        parameters = this.beforeUnload(parameters);

        this.props.performUnload(parameters)
            .then(response => {
                try {
                    let data = JSON.parse(response.Data);

                    this.afterUnload(data);

                    this.setState({ isLoadComplete: true });
                }
                catch (ex) {
                    this.toastException(ex);
                }
            });
    }

    registerComponent = (componentId, type, api) => {
        if (this.components.some(c => c.id === componentId))
            throw "Duplicate component ID found: " + componentId + ". Cannot register component";

        this.components = [
            ...this.components,
            {
                id: componentId,
                type: type,
                api: api
            }
        ];
    }
    unregisterComponent = (componentId) => {
        const index = this.components.findIndex(c => c.id === componentId);

        this.components = [
            ...this.components.slice(0, index),
            ...this.components.slice(index + 1)
        ];
    }
    getComponents = () => {
        return this.components;
    }
    getGrid = (gridId) => {
        const res = this.getComponent(gridId);

        if (res.type !== componentType.grid)
            throw "Component " + gridId + " is not a grid";

        return res.api;
    }
    getForm = (formId) => {
        const res = this.getComponent(formId);

        if (res.type !== componentType.form)
            throw "Component " + formId + " is not a form";

        return res.api;
    }
    getComponent = (componentId) => {
        const res = this.components.find(d => d.id === componentId);

        if (!res)
            throw "Component " + componentId + " not found";

        return res;
    }

    beforeLoad(...attrs) {
        if (this.props.onBeforeLoad)
            this.props.onBeforeLoad(attrs);
    }
    afterLoad(...attrs) {
        if (this.props.onAfterLoad)
            this.props.onAfterLoad(attrs);
    }
    beforeUnload(...attrs) {
        if (this.props.onBeforeUnload)
            this.props.onBeforeUnload(attrs);
    }
    afterUnload(...attrs) {
        if (this.props.onAfterUnload)
            this.props.onAfterUnload(attrs);
    }

    redirect = (url) => {
        this.props.history.push(url);
    }

    toast = (type, message, options) => {
        const translatedMessage = this.props.t(message);

        switch (type) {
            case notificationType.error:
                toast.error(translatedMessage, options);
                break;
            case notificationType.info:
                toast.info(translatedMessage, options);
                break;
            case notificationType.warning:
                toast.warn(translatedMessage, options);
                break;
            case notificationType.success:
                toast.success(translatedMessage, options);
                break;
            default:
                toast(translatedMessage, options);
        }
    }
    toastException = (exception) => {
        const translatedMessage = this.props.t(exception.message);

        this.toast(notificationType.error, exception.name + ": " + translatedMessage);
    }
    toastNotifications = (notifications) => {
        if (!notifications)
            return;

        notifications.forEach(d => {
            return this.toast(d.type, this.props.t(d.message));
        }, this);
    }

    getNexus() {
        return {
            registerComponent: this.registerComponent,
            unregisterComponent: this.unregisterComponent,
            getComponents: this.getComponents,
            getComponent: this.getComponent,
            getGrid: this.getGrid,
            getForm: this.getForm,
            redirect: this.redirect,
            toast: this.toast,
            toastException: this.toastException,
            toastNotifications: this.toastNotifications
        };
    }

    render() {
        if (this.state.isLoadComplete) {
            const { t } = this.props;

            return (
                <div className="base-page">
                    <div className="row">
                        <div className="col-md-12">
                            <h2>
                                <span className={this.props.icon} style={{ fontSize: "28px" }} /> {t(this.props.title)}
                            </h2>
                            <hr />
                        </div>
                    </div>
                    <PageContextProvider value={this.getNexus()}>
                        {this.props.children}
                    </PageContextProvider>
                </div>
            );
        }

        return null;

    }
}

export const Page = withTranslation()(withPageDataProvider(InternalPage));