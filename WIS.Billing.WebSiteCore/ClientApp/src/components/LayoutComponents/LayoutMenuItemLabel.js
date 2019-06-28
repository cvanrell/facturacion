import React, { useState } from 'react';
import { LayoutMenuItem } from './LayoutMenuItem';
import { LayoutSubmenu } from './LayoutSubmenu';

export function LayoutMenuItemLabel(props) {
    const [submenuOpen, setSubmenuOpen] = useState(false);

    function handleClick() {
        if (props.menuOpen) {
            toggleSubmenu();
        }
    }
    function handleMouseEnter() {

    }
    function handleMouseLeave() {

    }

    function toggleSubmenu() {
        setSubmenuOpen(!submenuOpen);
    }

    function isSearchResult() {
        if (props.searchValue.length === 0)
            return false;

        const searchValue = props.searchValue.toLowerCase();

        if (props.label.toLowerCase().indexOf(searchValue) > -1)
            return true;

        return props.items.some(i => i.label.toLowerCase().indexOf(searchValue) > -1);
    }
    function isSearchEmpty() {
        return props.searchValue.length === 0;
    }

    const submenu = props.items ? props.items.map(i => <LayoutMenuItem {...i} />) : null;

    const itemClass = "wis-menu-item" + (!isSearchEmpty() && !isSearchResult() ? " hidden" : "");

    return (
        <div id={props.id} className={itemClass}>
            <a className="wis-item-label" onClick={handleClick} onMouseEnter={handleMouseEnter}>
                <span>{props.label}</span>
                <i className="fa fa-chevron-right pull-right submenu-arrow" />
            </a>
            <LayoutSubmenu open={isSearchResult() || submenuOpen}>
                {submenu}
            </LayoutSubmenu>
        </div>
    );
}