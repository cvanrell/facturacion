import React, { useState } from 'react';
import { LayoutMenuItem } from './LayoutMenuItem';
import { LayoutSubmenu } from './LayoutSubmenu';

export function LayoutMenuSection(props) {
    const [submenuOpen, setSubmenuOpen] = useState(false);

    function handleClick() {
        if (props.menuOpen) {
            toggleSubmenu();
        }
    }
    function handleMouseLeave() {

    }
    function toggleSubmenu() {
        setSubmenuOpen(!submenuOpen);
    }

    function isSearchResult() {
        //TODO: Terminar
        if (props.searchValue.length === 0)
            return false;

        const searchValue = props.searchValue.toLowerCase();

        if (props.label.toLowerCase().indexOf(searchValue) > -1)
            return true;

        return props.searchValue.length > 0 && props.items.some(i => i.label.toLowerCase().indexOf(searchValue) > -1);
    }
    function isSearchEmpty() {
        return props.searchValue.length === 0;
    }

    const submenu = !props.items ? null : props.items.map(i => (
        <LayoutMenuItem
            key={i.id}
            searchValue={props.searchValue}
            menuOpen={props.menuOpen}
            {...i}
        />
    ));

    const sectionClass = "wis-menu-section" + ((!isSearchEmpty && !isSearchResult()) ? " hidden" : "");

    console.log(sectionClass);

    return (
        <div id={props.id} className={sectionClass}>
            <a className="wis-item-label" onClick={handleClick}>
                <i className={props.icon} />
                <span>{props.label}</span>
            </a>
            <LayoutSubmenu open={submenuOpen}>
                {submenu}
            </LayoutSubmenu>
        </div>
    );
}