import React from 'react';

export function LayoutMenuHeader(props) {    
    function handleClick(evt) {
        props.redirect("/");
    }
    function handleSearch(evt) {
        props.updateSearch(evt.target.value);
    }

    if (!props.menuOpen) {
        return (
            <div className="wis-nav-header">
                <div className="wis-nav-logo-mini">
                    <a onClick={handleClick} >
                        <span>WIS</span>
                    </a>
                </div>
                <div className="wis-nav-open-button" onClick={props.openMenu}>
                    <span className="fa fa-cog" />
                </div>
            </div>
        );
    }
    else {
        return (
            <div className="wis-nav-header">
                <div className="wis-nav-logo">
                    <a onClick={handleClick} >
                        <span>WIS 4.0</span>
                    </a>
                </div>
                <div className="wis-nav-search">
                    <div className="wis-nav-search-field">
                        <input className="wis-nav-search-input" placeholder="Buscar" onChange={handleSearch} />
                    </div>
                </div>
                <div className="wis-nav-close-button" onClick={props.closeMenu}>
                    <span className="fa fa-chevron-left" />
                </div>
            </div>
        );
    }
}