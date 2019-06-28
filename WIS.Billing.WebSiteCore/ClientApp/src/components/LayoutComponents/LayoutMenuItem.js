import React from 'react';
import { LayoutMenuItemAction } from './LayoutMenuItemAction';
import { LayoutMenuItemLabel } from './LayoutMenuItemLabel';

export function LayoutMenuItem(props) {
    if (props.type === "action")
        return (
            <LayoutMenuItemAction
                id={props.id}
                label={props.label}
                url={props.url}
                module={props.module}
                searchValue={props.searchValue}
                menuOpen={props.menuOpen}
            />
        );

    if (props.type === "label")
        return (
            <LayoutMenuItemLabel
                id={props.id}
                label={props.label}
                items={props.items}
                searchValue={props.searchValue}
                menuOpen={props.menuOpen}
            />
        );

    return null;
}