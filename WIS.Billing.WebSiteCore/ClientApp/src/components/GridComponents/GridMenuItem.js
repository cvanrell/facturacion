import React from 'react';
import { MenuItemButton } from './GridMenuItemButton';
import { MenuItemDivider } from './GridMenuItemDivider';
import { MenuItemHeader } from './GridMenuItemHeader';
import { menuItemType } from '../Enums';

export function MenuItem(props) {
    switch (props.type) {
        case menuItemType.button:
            return <MenuItemButton key={props.index} id={props.id} label={props.label} className={props.className} onClick={props.onClick} />;
        case menuItemType.divider:
            return <MenuItemDivider key={props.index} className={props.className} />;
        case menuItemType.header:
            return <MenuItemHeader key={props.index} label={props.label} className={props.className} />;
        default:
            return null;
    }
}