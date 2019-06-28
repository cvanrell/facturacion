import React from 'react';

export const ToolbarButton = (props) => {
    return (
        <button className={props.className} onClick={props.onClick} title={props.label}>
            <i className={props.icon} />
        </button>
    );
};