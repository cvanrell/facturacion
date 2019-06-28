import React from 'react';
import { cellType } from '../Enums';
import { CellText } from './GridCellText';
import { CellCheckbox } from './GridCellCheckbox';
import { CellProgress } from './GridCellProgress';
import { CellDateTime } from './GridCellDateTime';
import { CellDate } from './GridCellDate';
import { CellButton } from './GridCellButton';
import { CellItemList } from './GridCellItemList';

export function CellContent(props) {
    switch (props.type) {
        case cellType.text:
            return <CellText rowIsNew={props.rowIsNew} rowIsDeleted={props.rowIsDeleted} rowId={props.rowId} column={props.column} content={props.content} updateCellValue={props.updateCellValue} />;
        case cellType.checkbox:
            return <CellCheckbox rowIsNew={props.rowIsNew} rowIsDeleted={props.rowIsDeleted} rowId={props.rowId} column={props.column} content={props.content} updateCellValue={props.updateCellValue} />;
        case cellType.progress:
            return <CellProgress rowIsNew={props.rowIsNew} rowIsDeleted={props.rowIsDeleted} rowId={props.rowId} column={props.column} content={props.content} updateCellValue={props.updateCellValue} />;
        case cellType.dateTime:
            return <CellDateTime rowIsNew={props.rowIsNew} rowIsDeleted={props.rowIsDeleted} rowId={props.rowId} column={props.column} content={props.content} updateCellValue={props.updateCellValue} />;
        case cellType.date:
            return <CellDate rowIsNew={props.rowIsNew} rowIsDeleted={props.rowIsDeleted} rowId={props.rowId} column={props.column} content={props.content} updateCellValue={props.updateCellValue} />;
        case cellType.button:
            return <CellButton rowIsNew={props.rowIsNew} rowIsDeleted={props.rowIsDeleted} rowId={props.rowId} column={props.column} content={props.content} performButtonAction={props.performButtonAction} />;
        case cellType.itemList:
            return <CellItemList rowIsNew={props.rowIsNew} rowIsDeleted={props.rowIsDeleted} rowId={props.rowId} column={props.column} content={props.content} openDropdown={props.openDropdown} />;
        default:
            return props.content.value;
    }
}