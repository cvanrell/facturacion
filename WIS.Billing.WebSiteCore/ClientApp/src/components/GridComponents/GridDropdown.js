import React, { useRef } from 'react';
import { MenuItem } from './GridMenuItem';

export function GridDropdown(props) {
    const timeout = useRef(false);

    const style = {
        display: props.show ? "block" : "none",
        left: props.left + 1,
        top: props.top - 2
    };

    const handleItemClick = (evt) => {
        evt.preventDefault();

        props.performButtonAction(props.rowId, props.columnId, evt.target.value);
    };
    const handleMenuLeave = () => {
        if (props.show) {
            timeout.current = setTimeout(() => {
                props.closeDropdown();
            }, 300);
        }
    };
    const handleMouseEnter = () => {
        if (timeout.current) {
            clearTimeout(timeout.current);
        }
    };

    const menuItems = props.items.map((d, index) => (
        <MenuItem
            type={d.itemType}
            key={index}
            id={d.id}
            index={index}
            label={d.label}
            className={d.cssClass}
            onClick={handleItemClick}
        />
    ));

    return (
        <div
            className="dropdown-menu"
            style={style}
            onMouseLeave={handleMenuLeave}
            onMouseEnter={handleMouseEnter}
        >
            {menuItems}
        </div>
    );
}