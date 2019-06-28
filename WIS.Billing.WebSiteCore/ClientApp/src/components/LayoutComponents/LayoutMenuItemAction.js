import React from 'react';
import { Link } from 'react-router-dom';

export function LayoutMenuItemAction(props) {
    return (
        <div id={props.id} className="wis-menu-item">
            <Link to={props.url} className="wis-item-action">
                <span>{props.label}</span>
            </Link>
        </div>
    );
}