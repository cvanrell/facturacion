import React from 'react';

export function LayoutHeader() {
    return (
        <header>
            <div className="wis-header-container">
                <div className="wis-header-title">
                    <span className="customer-brand">
                        <img src="/Content/Img/logo.png" alt="Logo" />
                    </span>
                </div>
                <div className="wis-header-user">
                    <span className="wis-header-username">Administrador</span>
                    <a className="wis-header-logout" href="/SEG/Logout">
                        <i className="fa fa-remove" />
                    </a>
                </div>
            </div>
        </header>
    );
}