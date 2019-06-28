import React from 'react';

export function LayoutSubmenu(props) {
    const submenuClass = "wis-item-submenu" + (props.open ? " open" : "");

    return (
        <div className={submenuClass} onMouseLeave={props.handleMouseLeave}>
            {props.children}
        </div>
    );
}