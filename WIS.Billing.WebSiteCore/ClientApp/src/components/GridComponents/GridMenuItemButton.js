import React from 'react';

export const MenuItemButton = (props) => {
    return (
        <button id={props.id} className="dropdown-item" onClick={props.onClick}>{props.label}</button>
    );
};