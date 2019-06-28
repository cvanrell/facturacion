import React, { useRef } from 'react';

export function CellItemList(props) {
    const buttonRef = useRef(null);

    function handleClick(evt) {
        props.openDropdown(props.column.id, props.rowId, buttonRef.current.getBoundingClientRect().right, buttonRef.current.getBoundingClientRect().top);
    }

    return (
        <div className="gr-cell-itemlist dropdown">
            <button
                ref={buttonRef}
                className="gr-btn"
                onClick={handleClick}
                title={props.label}
            >
                <i className="fas fa-bars" />
            </button>
        </div>
    );
}