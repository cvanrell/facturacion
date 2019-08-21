import React, { Component } from 'react';
import { HeaderPane } from './GridHeaderPane';
import { BodyPane } from './GridBodyPane';
import { GridContainer } from './GridContainer';
import { GridContentContainer } from './GridContentContainer';
import { LoadingSkeleton } from './GridLoadingSkeleton';
import { GridScrollPane } from './GridScrollPane';
import { Toolbar } from './GridToolbar';
import { ScrollSync } from '../ScrollSyncComponent';
import withScrollContext from './WithScrollContext';
import withGridDataProvider from './WithGridDataProvider';
import { withPageContext } from '../WithPageContext';
import ColumnResizeMarker from './GridColumnResizeMarker';
import { GridDropdown } from './GridDropdown'
import { columnFixed, gridStatus, filterStatus, componentType } from '../Enums';
import PropTypes from 'prop-types';

export class InternalGrid extends Component {
    static propTypes = {
        enableSelection: PropTypes.bool,
        onBeforeInitialize: PropTypes.func,
        onAfterInitialize: PropTypes.func,
        onBeforeFetch: PropTypes.func,
        onAfterFetch: PropTypes.func,
        onBeforeCommit: PropTypes.func,
        onAfterCommit: PropTypes.func,
        onBeforeValidateRow: PropTypes.func,
        onAfterValidateRow: PropTypes.func,
        onBeforeButtonAction: PropTypes.func,
        onAfterButtonAction: PropTypes.func,
        onBeforeMenuItemAction: PropTypes.func,
        onAfterMenuItemAction: PropTypes.func,
        onBeforeApplyFilter: PropTypes.func,
        onAfterApplyFilter: PropTypes.func,
        onBeforeApplySort: PropTypes.func,
        onAfterApplySort: PropTypes.func,
        rowFetchOverscan: PropTypes.number,
        rowOverscan: PropTypes.number,
        rowsToDisplay: PropTypes.number,
        rowsToFetch: PropTypes.number
    }

    static defaultProps = {
        enableSelection: false,
        rowFetchOverscan: 30,
        rowOverscan: 30,
        rowsToDisplay: 20,
        rowsToFetch: 20,
        lookupAmount: 100
    }

    constructor(props) {
        super(props);

        this.state = {
            columns: [],
            rows: [],
            filters: [],
            sorts: [],
            selection: [],
            highlight: [],
            menuItems: [],

            dropdownRowId: null,
            dropdownColumnId: null,
            dropdownShow: false,
            dropdownTop: 0,
            dropdownLeft: 0,
            dropdownItems: [],

            lookup: [],
            lookupMargin: [],

            rowLastNewId: 0,
            rowHeight: 28,
            rowDisplayStart: 0,
            rowDisplayEnd: props.rowsToFetch,
            rowCurrentIndex: 0,

            filterStatus: filterStatus.closed,

            resizeParentOffset: 0,
            resizeWidthBase: 0,
            resizeWidth: 0,
            resizePositionInitial: 0,
            resizeCurrentPosition: 0,

            highlightLast: null,
            
            isSelectionInverted: false,
            isFetching: false,
            isResizing: false,
            isInitializing: true
        };

        this.toolbarHeight = 29;
        this.headerHeight = 30;

        this.shouldPerformDisplayUpdate = false;
        this.offsetToUpdate = 0;
        this.lastOffset = 0;
        this.scrollWatcher = null;

        this.containerRef = React.createRef();
        this.bodyRef = React.createRef();
    }

    componentDidMount() {
        this.mounted = true;

        this.initialize(this.props.id, this.props.rowsToFetch);
        
        this.watchScroll();

        this.props.nexus.registerComponent(this.props.id, componentType.grid, this.getApi());
    }
    shouldComponentUpdate(nextProps) {
        return !nextProps.scrollContext.isScrolling;
    }
    componentWillUnmount() {
        this.unWatchScroll();

        this.props.nexus.unregisterComponent(this.props.id);

        this.mounted = false;
    }

    hasFixedColumnsLeft = () => {
        return this.state.columns.some(col => col.fixed === columnFixed.left);
    }
    hasFixedColumnsRight = () => {
        return this.state.columns.some(col => col.fixed === columnFixed.right);
    }
    
    addRow = () => {
        const rowId = this.state.rowLastNewId + 1;

        const newRow = {
            id: "newRow_" + rowId,
            index: -rowId,
            isNew: true,
            cells: this.state.columns.map(col =>
                ({
                    column: col.id,
                    value: col.defaultValue ? col.defaultValue : "",
                    old: "",
                    editable: col.insertable
                })
            )
        };

        this.setState({
            rows: [newRow, ...this.state.rows],
            rowLastNewId: rowId
        });
    }
    getRows = () => {
        console.log(this.state.rows.filter(row => row.new));
    }
    deleteRow = () => {
        const rowIds = this.state.highlight.map(s => s.rowId);

        let rows = this.state.rows.filter(r => !r.isNew || rowIds.indexOf(r.id) < 0);

        //TODO: < O(N^2) ver si es posible otimizar mas
        for (let i = 0; i < rowIds.length; i++) {
            for (let j = 0; j < rows.length; j++) {
                if (rows[j].id === rowIds[i]) {
                    rows[j].isDeleted = true;
                    break;
                }
            }
        }

        this.setState({
            rows: rows
        });
    }    

    addCellHighlight = (highlight, clearPrevious) => {
        const cellHighlight = clearPrevious ? [] : [...this.state.highlight];

        var rowIndex = cellHighlight.findIndex(d => d.rowId === highlight.rowId);

        if (rowIndex >= 0) {
            var colIndex = cellHighlight[rowIndex].columns.indexOf(highlight.columnId);

            if (colIndex >= 0) {
                this.setState({
                    highlight: [
                        ...cellHighlight.slice(0, rowIndex),
                        {
                            ...cellHighlight[rowIndex],
                            columns: cellHighlight[rowIndex].columns.filter(d => d !== highlight.columnId)
                        },
                        ...cellHighlight.slice(rowIndex + 1)
                    ],
                    highlightLast: highlight
                });
            }
            else {
                this.setState({
                    highlight: [
                        ...cellHighlight.slice(0, rowIndex),
                        {
                            ...cellHighlight[rowIndex],
                            columns: [
                                ...cellHighlight[rowIndex].columns,
                                highlight.columnId
                            ]
                        },
                        ...cellHighlight.slice(rowIndex + 1)
                    ],
                    highlightLast: highlight
                });
            }
        }
        else {
            this.setState({
                highlight: [
                    ...cellHighlight,
                    {
                        rowId: highlight.rowId,
                        index: highlight.index,
                        columns: [highlight.columnId]
                    }
                ],
                highlightLast: highlight
            });
        }
    }
    addCellHighlightGroup = (highlight) => {
        const orderedColumns = [...this.state.columns.filter(c => !c.hidden)].sort((a, b) => this.sortCellHighlight(a, b));

        const lastHighlight = this.state.highlightLast;

        const lastColumnIndex = orderedColumns.findIndex(c => c.id === lastHighlight.columnId);
        const currentColumnIndex = orderedColumns.findIndex(c => c.id === highlight.columnId);

        const minRowIndex = lastHighlight.index >= highlight.index ? highlight.index : lastHighlight.index;
        const maxRowIndex = lastHighlight.index < highlight.index ? highlight.index : lastHighlight.index;

        const minColIndex = lastColumnIndex >= currentColumnIndex ? currentColumnIndex : lastColumnIndex;
        const maxColIndex = lastColumnIndex < currentColumnIndex ? currentColumnIndex : lastColumnIndex;

        const columns = orderedColumns.filter((c, index) => index >= minColIndex && index <= maxColIndex).map(c => c.id);
        const finalHighlight = this.state.rows.filter(r => r.index >= minRowIndex && r.index <= maxRowIndex).map(r => ({ rowId: r.id, index: r.index, columns: columns }));
        
        this.setState({
            highlight: finalHighlight
        });
    }
    sortCellHighlight(a, b) {
        if ((a.fixed === 2 && b.fixed === 1) || (a.fixed === 1 && b.fixed === 3) || (a.fixed === 2 && b.fixed === 3)) {
            return -1;
        }

        if ((a.fixed === 3 && b.fixed === 1) || (a.fixed === 1 && b.fixed === 2) || (a.fixed === 3 && b.fixed === 2)) {
            return 1;
        }

        return 0;
    }
    getCellHighlight = (evt) => {
        evt.preventDefault();

        const highlight = [...this.state.highlight].sort((prev, next) => prev.index - next.index);
        const columns = [...this.state.columns].sort((a, b) => this.sortCellHighlight(a, b)).map(d => d.id);

        const columnList = columns.filter(col => highlight.some(sel => sel.columns.indexOf(col) >= 0));

        let rowIndex;
        let colOutput;

        const output = highlight.reduce((rowOutput, high) => {
            rowIndex = this.state.rows.findIndex(d => d.id === high.rowId);

            colOutput = columnList.reduce((output, col) => {
                return output + (high.columns.indexOf(col) >= 0 ? this.state.rows[rowIndex].cells.find(c => c.column === col).value : "") + "\t";
            }, "");

            return rowOutput + colOutput + "\n";
        }, "");

        navigator.clipboard.writeText(output);
    }
    
    initialize = () => {
        const data = {
            gridId: this.props.id,
            rowsToFetch: this.props.rowsToFetch,
            parameters: []
        };

        const context = {
            abortServerCall: false
        };

        if (this.props.onBeforeInitialize)
            this.props.onBeforeInitialize(context, data);

        if (!this.mounted || context.abortServerCall)
            return false;

        this.props.gridInitialize(data).then(this.initializeProcessResponse);
    }
    initializeProcessResponse = (response) => {
        try {
            const data = JSON.parse(response.Data);

            let index = 0;
            let rows = [];

            const context = {
                abortUpdate: false
            };

            if (this.props.onAfterInitialize)
                this.props.onAfterInitialize(context, data.grid, data.parameters);

            if (!this.mounted || context.abortUpdate)
                return false;

            if (data.grid.rows.length > 0) {
                rows = this.appendRowData(data.grid.rows, true);
                index = data.grid.rows[data.grid.rows.length - 1].index;
            }

            this.setState({
                columns: data.grid.columns,
                rows: rows,
                lookup: this.getUpdatedLookup(rows, true),
                menuItems: data.grid.menuItems,
                rowCurrentIndex: index,
                isInitializing: false
            });
        }
        catch (ex) {
            //TODO: Mostrar mensaje error
            console.log(ex);
        }
    }

    fetchData = (evt, dontSkip, clearPrevious) => {
        if (this.state.isFetching)
            return false;

        this.setState({
            isFetching: true
        });

        try {
            const data = {
                gridId: this.props.id,
                filters: [...this.state.filters],
                sorts: [...this.state.sorts],
                rowsToSkip: dontSkip ? 0 : this.state.rows.length,
                rowsToFetch: this.props.rowsToFetch,
                parameters: []
            };

            const context = {
                abortServerCall: false
            };

            if (this.props.onBeforeFetch)
                this.props.onBeforeFetch(evt, context, data);

            if (!this.mounted || context.abortServerCall)
                return false;

            return this.props.gridFetchRows(data).then(this.fetchDataProcessResponse.bind(this, clearPrevious));
        }
        catch(ex) {
            //TODO: Mostrar mensaje error
            this.setState({
                isFetching: false
            });
        }
    }
    fetchDataProcessResponse = (clearPrevious, response) => {
        try {
            const data = JSON.parse(response.Data);

            let newRows = data.rows;

            const context = {
                abortUpdate: false
            };

            if (this.props.onAfterFetch)
                this.props.onAfterFetch(context, newRows, data.parameters);

            if (!this.mounted)
                return false;

            if (!context.abortUpdate && newRows) {
                if(!clearPrevious)
                    newRows = this.removeDuplicates(newRows);

                newRows = this.appendRowData(newRows, clearPrevious);                

                if (clearPrevious) {
                    this.setState(prevState => ({
                        rows: [...newRows],
                        lookup: this.getUpdatedLookup(newRows),
                        rowCurrentIndex: newRows.length > 0 ? newRows[newRows.length - 1].index : prevState.rowCurrentIndex,
                        isFetching: false
                    }));
                }
                else {
                    this.setState(prevState => ({
                        rows: [...prevState.rows || [], ...newRows],
                        lookup: this.getUpdatedLookup(newRows),
                        rowCurrentIndex: newRows.length > 0 ? newRows[newRows.length - 1].index : prevState.rowCurrentIndex,
                        isFetching: false
                    }));
                }
            }
            else {
                this.setState({
                    isFetching: false
                });
            }
        }
        catch (ex) {
            //TODO: Mostrar mensaje error
            this.setState({
                isFetching: false
            });
        }
    }

    appendRowData = (rows, resetCounter) => {
        let currentIndex = this.state.rowCurrentIndex;

        let result = [...rows];

        if (resetCounter)
            currentIndex = 0;

        for (let i = 0; i < result.length; i++) {
            result[i].index = currentIndex++;
        }

        return result;
    }

    validateRow = (row) => {
        const data = {
            gridId: this.props.id,
            row: row,
            parameters: []
        };

        const context = {
            abortServerCall: false
        };

        if (this.props.onBeforeValidateRow)
            this.props.onBeforeValidateRow(context, data);

        if (!this.mounted || context.abortServerCall)
            return false;

        this.props.gridValidateRow(data).then(this.validateRowProcessResponse);
    }
    validateRowProcessResponse = (response) => {
        const data = JSON.parse(response.Data);

        const context = {
            abortUpdate: false
        };

        if (this.props.onAfterValidateRow)
            this.props.onAfterValidateRow(context, data.row, data.parameters);

        if (!this.mounted || context.abortUpdate)
            return false;

        const rowIndex = this.state.rows.findIndex(r => r.id === data.row.id);

        const updatedRows = [
            ...this.state.rows.slice(0, rowIndex),
            {
                ...data.row,
                index: this.state.rows[rowIndex].index
            },
            ...this.state.rows.slice(rowIndex + 1)
        ];

        this.setState({
            rows: updatedRows
        });
    }

    commit = () => {
        const data = {
            gridId: this.props.id,
            rows: this.state.rows.filter(row => row.isNew || row.isDeleted || row.cells.some(cell => cell.editable && cell.old !== cell.value)),
            filters: this.state.filters,
            sorts: this.state.sorts,
            rowsToFetch: this.props.rowsToFetch,
            parameters: []
        };

        const context = {
            abortServerCall: false
        };

        if (this.props.onBeforeCommit)
            this.props.onBeforeCommit(context, data);

        if (!this.mounted || context.abortServerCall)
            return false;

        this.props.gridCommit(data).then(this.commitProcessResponse);
    }
    commitProcessResponse = (response) => {
        const data = JSON.parse(response.Data);

        let rows = data.rows;

        const context = {
            abortUpdate: false
        };

        if (this.props.onAfterCommit)
            this.props.onAfterCommit(context, rows, data.parameters);

        if (!this.mounted || context.abortUpdate)
            return false;

        if (response.Status === "OK") {
            this.setState({
                rows: rows
            });
        }
        else if (response.Status === "ERROR") {
            //Usa mutaciones por performance ¯\_(ツ)_/¯ 
            const stateRows = [...this.state.rows];

            const indexes = rows.map(r => ({ index: stateRows.findIndex(row => row.id === r.id), row: r }))
                .sort((a, b) => a.index - b.index);

            let updatedRows = [];

            let prevIndex = 0;

            for (var i = 0; i < indexes.length; i++) {
                updatedRows = [...updatedRows, ...stateRows.slice(prevIndex, indexes[i].index), indexes[i].row];
                console.log(updatedRows);
                prevIndex = indexes[i].index + 1;
            }

            updatedRows = [...updatedRows, ...stateRows.slice(prevIndex)];

            this.setState({
                rows: updatedRows,
                lookup: this.getUpdatedLookup(updatedRows),
            });
        }
    }

    performButtonAction = (rowId, columnId, btnId) => {
        try {
            const rowIndex = this.state.rows.findIndex(row => row.id === rowId);

            const data = {
                gridId: this.props.id,
                buttonId: btnId,
                columnId: columnId,
                row: this.state.rows[rowIndex],
                parameters: []
            };

            const context = {
                abortServerCall: false
            };

            if (this.props.onBeforeButtonAction)
                this.props.onBeforeButtonAction(context, data, this.props.nexus);

            if (!this.mounted || context.abortServerCall)
                return false;

            this.props.gridButtonAction(data).then(this.performButtonActionProcessResponse);
        }
        catch (ex) {
            this.props.nexus.toastException(ex);
        }
    }
    performButtonActionProcessResponse = (response) => {
        if (response.Status === "OK") {
            const data = JSON.parse(response.Data);

            if (data.redirect)
                this.props.nexus.redirect(data.redirect);

            if (this.props.onAfterButtonAction)
                this.props.onAfterButtonAction(data);
        }
        else {
            //TODO: Mostrar error
        }
    }

    performMenuItemAction = (btnId) => {
        try {
            const selection = {
                isInverted: this.state.isSelectionInverted,
                keys: this.state.selection
            };

            const data = {
                gridId: this.props.id,
                buttonId: btnId,
                filters: this.state.filters,
                selection: selection,
                parameters: []
            };

            const context = {
                abortServerCall: false
            };

            if (this.props.onBeforeMenuItemAction)
                this.props.onBeforeMenuItemAction(context, data, this.props.nexus);

            if (!this.mounted || context.abortServerCall)
                return false;

            this.props.gridMenuItemAction(data).then(this.performMenuItemActionProcessResponse);
        }
        catch (ex) {
            this.props.nexus.toastException(ex);
        }
    }
    performMenuItemActionProcessResponse = (response) => {
        try {
            if (response.Status === "ERROR")
                throw new Error(response.Message);

            const data = JSON.parse(response.Data);

            const context = {
                abortUpdate: false
            };

            if (this.props.onAfterMenuItemAction)
                this.props.onAfterMenuItemAction(context, data, this.props.nexus);

            if (!this.mounted || context.abortUpdate)
                return false;

            this.props.nexus.toastNotifications(data.notifications);

            if (data.redirect)
                this.props.nexus.redirect(data.redirect);

            this.setState({
                isSelectionInverted: false,
                selection: []
            });
        }
        catch (ex) {
            this.props.nexus.toastException(ex);
        }
    }

    updateFilter = (columnId, value) => {
        const filters = [...this.state.filters];

        const index = filters.findIndex(filter => filter.columnId === columnId);

        if (index >= 0) {
            if (value) {
                this.setState({
                    filters: [
                        ...filters.slice(0, index),
                        {
                            ...filters[index],
                            value: value
                        },
                        ...filters.slice(index + 1)
                    ]
                });
            }
            else {
                this.setState({
                    filters: filters.filter(f => f.columnId !== columnId) || []
                });
            }
        }
        else {
            this.setState({
                filters: [
                    ...filters,
                    {
                        columnId: columnId,
                        value: value
                    }
                ]
            });
        }
    }
    applyFilter = () => {
        if (this.state.isFetching)
            return false;

        this.setState({
            isFetching: true
        });

        const data = {
            gridId: this.props.id,
            filters: this.state.filters,
            sorts: this.state.sorts,
            rowsToSkip: 0,
            rowsToFetch: this.props.rowsToFetch,            
            parameters: []
        };

        const context = {
            abortServerCall: false
        };

        if (this.props.onBeforeApplyFilter)
            this.props.onBeforeApplyFilter(context, data);

        if (!this.mounted && context.abortServerCall)
            return false;

        this.props.gridFetchRows(data).then(this.applyFilterProcessResponse).catch((err) => console.log(err));
    }
    applyFilterProcessResponse = (response) => {
        try {
            const data = JSON.parse(response.Data);

            let newRows = data.rows;

            const context = {
                abortUpdate: false
            };

            if (this.props.onAfterApplyFilter)
                this.props.onAfterApplyFilter(context, newRows, data.parameters);
            
            if (!this.mounted)
                return false;

            if (!context.abortUpdate && newRows !== undefined && newRows !== null) {
                this.updateRowsFilter(newRows);
            }
            else {
                this.setState({
                    isFetching: false
                });
            }
        }
        catch (ex) {
            //TODO: Mostrar mensaje error
            console.log(ex);

            this.setState({
                isFetching: false
            });
        }
    }

    applySort = (columnId) => {
        if (this.state.isFetching)
            return false;

        this.setState({
            isFetching: true
        });

        const sorts = this.getNewSorts(columnId);

        const data = {
            gridId: this.props.id,
            filters: this.state.filters,
            sorts: sorts,
            rowsToSkip: 0,
            rowsToFetch: this.props.rowsToFetch,
            parameters: []
        };

        const context = {
            abortServerCall: false
        };

        if (this.props.onBeforeApplySort)
            this.props.onBeforeApplySort(context, data);

        if (!this.mounted || context.abortServerCall)
            return false;

        this.props.gridFetchRows(data).then(this.applySortProcessResponse.bind(this, sorts));
    }
    applySortProcessResponse = (sorts, response) => {
        try {
            const data = JSON.parse(response.Data);

            let rows = data.rows;

            const context = {
                abortUpdate: false
            };

            if (this.props.onAfterApplySort)
                this.props.onAfterApplySort(context, rows, data.parameters);

            if (!this.mounted)
                return false;

            if (!context.abortUpdate && rows !== undefined && rows !== null) {
                this.updateRowsSort(rows, sorts);
            }
            else {
                this.setState({
                    isFetching: false
                });
            }
        }
        catch (ex) {
            this.setState({
                isFetching: false
            });
        }
    }
    getNewSorts = (columnId) => {
        const sorts = [...this.state.sorts];
        const index = sorts.findIndex(sort => sort.columnId === columnId);
        let newSorts = [];

        if (index >= 0) {
            if (sorts[index].direction === 1) {
                newSorts = [
                    ...sorts.slice(0, index),
                    {
                        ...sorts[index],
                        direction: 2
                    },
                    ...sorts.slice(index + 1)
                ];
            }
            else {
                newSorts = sorts.filter(sort => sort.columnId !== columnId);
            }
        }
        else {
            newSorts = [
                ...sorts,
                {
                    columnId: columnId,
                    direction: 1
                }
            ];
        }

        return newSorts;
    }

    updateGridConfig = () => {
        const data = {
            gridId: this.props.id,
            columns: this.state.columns,
            parameters: []
        };

        const context = {
            abortServerCall: false
        };

        if (this.props.onBeforeUpdateGridConfig)
            this.props.onBeforeUpdateGridConfig(context, data);

        if (!this.mounted || context.abortServerCall)
            return false;

        this.props.gridUpdateConfig(data).then(this.updateGridConfigResponse);
    }
    updateGridConfigResponse = (response) => {
        const data = JSON.parse(response.Data);

        const context = {
            abortUpdate: false
        };

        if (this.props.onAfterUpdateGridConfig)
            this.props.onAfterUpdateGridConfig(context, data.row, data.parameters);
    }

    updateRowsFilter = (rows) => {
        if (rows) {
            rows = this.appendRowData(rows, true);
        }

        const index = rows.length > 0 ? rows[rows.length - 1].index : 0;

        this.setState({
            rows: [...rows],
            lookup: this.getUpdatedLookup(rows, true),
            rowDisplayStart: 0,
            rowDisplayEnd: this.props.rowsToDisplay,
            rowCurrentIndex: index,
            isFetching: false
        });
    }
    updateRowsSort = (rows, sorts) => {
        if (rows) {
            rows = this.appendRowData(rows, true);
        }

        const index = rows.length > 0 ? rows[rows.length - 1].index : 0;

        this.setState({
            rows: [...rows],
            lookup: this.getUpdatedLookup(rows, true),
            rowDisplayStart: 0,
            rowDisplayEnd: this.props.rowsToDisplay,
            rowCurrentIndex: index,
            sorts: sorts,
            isFetching: false
        });
    }

    rollback = () => {
        const rows = this.state.rows.filter(row => !row.isNew);

        const updatedRows = rows.map(row => ({
            ...row,
            isDeleted: false,
            cells: row.cells.map(cell => ({
                ...cell,
                value: cell.editable ? cell.old : cell.value,
                status: gridStatus.ok
            }))
        }));

        this.setState({
            rows: updatedRows
        });        
    }
    refresh = () => {
        this.fetchData(null, true, true);
    }

    updateCellValue = (rowId, columnId, value) => {
        const rows = [...this.state.rows];

        const rowIndex = rows.findIndex(row => row.id === rowId);

        const cells = [...rows[rowIndex].cells];

        const cellIndex = cells.findIndex(cell => cell.column === columnId);

        const updatedRows = [
            ...rows.slice(0, rowIndex),
            {
                ...rows[rowIndex],
                cells: [
                    ...cells.slice(0, cellIndex),
                    {
                        ...cells[cellIndex],
                        value: value,
                        modified: true
                    },
                    ...cells.slice(cellIndex + 1)
                ]
            },
            ...rows.slice(rowIndex + 1)
        ];

        this.setState({
            rows: updatedRows
        }, () => this.validateRow(updatedRows[rowIndex]));
    }

    columnResizeChange = (mousePosition) => {
        if (!this.state.resizeInverted) {
            this.setState(prevState => ({
                resizeWidth: prevState.resizeWidthBase + (mousePosition - prevState.resizePositionInitial)
            }));
        }
        else {
            this.setState(prevState => ({
                resizeWidth: prevState.resizeWidthBase - (mousePosition - prevState.resizePositionInitial)
            }));
        }
    }
    columnResizeBegin = (columnId, parentOffset, initialOffset, mousePositionInitial, inverted) => {
        this.setState({
            isResizing: true,
            resizeColumnId: columnId,
            resizeParentOffset: parentOffset,
            resizeWidthBase: initialOffset,
            resizeWidth: initialOffset,
            resizePositionInitial: mousePositionInitial,
            resizeInverted: inverted
        });
    }
    columnResizeEnd = () => {
        const index = this.state.columns.findIndex(col => col.id === this.state.resizeColumnId);

        this.setState(prevState => ({
            isResizing: false,
            columns: [
                ...prevState.columns.slice(0, index),
                {
                    ...prevState.columns[index],
                    width: prevState.resizeWidth
                },
                ...prevState.columns.slice(index + 1)
            ]
        }), () => this.updateGridConfig());
    }
    updateColumnOrder = (columnId, targetColumn, previousIndex, nextIndex, rightHalf) => {
        if (previousIndex === nextIndex)
            return false;

        const targetIndex = this.state.columns.findIndex(col => col.id === targetColumn);
        let target;

        let columns = this.state.columns.map(column => {
            if (rightHalf) {
                if (previousIndex > nextIndex) {
                    if (column.id === columnId) {
                        target = this.state.columns[targetIndex + 1];

                        return { ...column, order: nextIndex + 1, fixed: target ? target.fixed : column.fixed };
                    }

                    if (column.order < previousIndex && column.order > nextIndex) {
                        return { ...column, order: column.order + 1 };
                    }
                }
                else {
                    if (column.id === columnId) {
                        target = this.state.columns[targetIndex];

                        return { ...column, order: nextIndex, fixed: target ? target.fixed : column.fixed  };
                    }

                    if (column.order > previousIndex && column.order <= nextIndex) {
                        return { ...column, order: column.order - 1 };
                    }
                }
            }
            else {
                if (previousIndex > nextIndex) {
                    if (column.id === columnId) {
                        target = this.state.columns[targetIndex];

                        return { ...column, order: nextIndex, fixed: target ? target.fixed : column.fixed };
                    }

                    if (column.order < previousIndex && column.order >= nextIndex) {
                        return { ...column, order: column.order + 1 };
                    }
                }
                else {
                    if (column.id === columnId) {
                        target = this.state.columns[targetIndex];

                        return { ...column, order: nextIndex - 1, fixed: target ? target.fixed : column.fixed };
                    }

                    if (column.order > previousIndex && column.order < nextIndex) {
                        return { ...column, order: column.order - 1 };
                    }
                }
            }

            return { ...column };
        });

        columns.sort((a, b) => a.order - b.order);
        
        this.setState({
            columns: columns
        });
    }

    removeDuplicates = (rows) => {
        return rows.filter(r => this.state.lookup.indexOf(r.id) === -1);
    }
    getUpdatedLookup = (rows, replace) => {
        const rowIds = rows.map(r => r.id);

        if (!replace && rowIds.length > 0 && this.props.lookupAmount > this.props.rowsToFetch) {
            return [...this.state.lookup.slice(-(this.props.lookupAmount - rowIds.length)), ...rowIds];
        }

        return rowIds;
    }

    getResizeMarkerPosition() {
        return this.state.resizeInverted ? this.state.resizeParentOffset + (this.state.resizeWidthBase - this.state.resizeWidth)
                                         : this.state.resizeParentOffset + this.state.resizeWidth;
    }
    getResizeMarkerHeight(scrollbarHeight) {
        if (this.containerRef.current) {
            return this.containerRef.current.getBoundingClientRect().height - scrollbarHeight;
        }

        return 0;
    }

    updateSelection = (rowId) => {
        if (this.state.selection.indexOf(rowId) >= 0) {
            this.setState(prevState => ({
                selection: prevState.selection.filter(s => s !== rowId)
            }));
        }
        else {
            this.setState(prevState => ({
                selection: [
                    ...prevState.selection,
                    rowId
                ]
            }));
        }
    }
    invertSelection = () => {
        if (this.state.isSelectionInverted && this.state.selection.length > 0) {
            this.setState({
                selection: []
            });
        }
        else {
            this.setState(prevState => ({
                selection: [],
                isSelectionInverted: !prevState.isSelectionInverted
            }));
        }
    }
        
    openFilterBar = () => {
        this.setState({
            filterStatus: filterStatus.open
        });
    }
    loadFilterBar = () => {
        this.setState({
            filterStatus: filterStatus.review
        });
    }
    closeFilterBar = () => {
        this.containerRef.current.focus();

        this.setState({
            filterStatus: filterStatus.closed
        });
    }
    toggleFilterBar = (evt) => {
        evt.preventDefault();

        if (this.state.filterStatus === filterStatus.closed) { //TODO: pasar a enums
            this.openFilterBar();
        }
        else if (this.state.filterStatus === filterStatus.open) {
            this.loadFilterBar();
        }
        else if (this.state.filterStatus === filterStatus.review) {
            this.closeFilterBar();
        }
    }

    openDropdown = (columnId, rowId, left, top) => {
        const column = this.state.columns.find(d => d.id === columnId);

        if (!column)
            return;

        console.log(this.bodyRef);

        const offsetTopBody = this.bodyRef.current.getBoundingClientRect().top;
        const offsetLeftBody = this.bodyRef.current.getBoundingClientRect().left;

        this.setState({
            dropdownShow: true,
            dropdownColumnId: columnId,
            dropdownRowId: rowId,
            dropdownItems: column.items,
            dropdownLeft: left - offsetLeftBody,
            dropdownTop: top - offsetTopBody
        });
    }
    closeDropdown = () => {
        if (!this.mounted)
            return false;

        this.setState({
            dropdownShow: false
        });
    }

    dropdownClick = (evt) => {
        evt.preventDefault();

        this.performButtonAction(this.state.dropdownRowId, this.state.dropdownColumnId, evt.target.id);
    }

    updateScrollPosition = (rawOffset) => {
        window.requestAnimationFrame(() => {
            this.props.scrollContext.onScrollBegin();

            window.requestAnimationFrame(() => {
                if (rawOffset !== this.lastOffset) {
                    this.lastOffset = rawOffset;

                    const offset = Math.floor(rawOffset / this.state.rowHeight);

                    if (!this.shouldUpdateVisibleArea) {
                        if (offset - this.props.rowOverscan < this.state.rowDisplayStart || offset + this.props.rowsToDisplay + this.props.rowOverscan > this.state.rowDisplayEnd) {
                            this.shouldUpdateVisibleArea = true;
                            this.offsetToUpdate = offset;
                        }
                    }
                }
            });
        });
    }
    updateVisibleArea = () => {
        if (!this.state.isFetching && this.offsetToUpdate + this.props.rowsToDisplay + this.props.rowFetchOverscan > this.state.rows.length) {
            this.fetchData().then(() => {
                this.shouldUpdateVisibleArea = false; //Ver si hacer promesa
            });
        }
        else {
            this.setState({
                rowDisplayStart: this.offsetToUpdate - this.props.rowOverscan < 0 ? 0 : this.offsetToUpdate - this.props.rowOverscan,
                rowDisplayEnd: Math.min(this.offsetToUpdate + this.props.rowsToDisplay + this.props.rowOverscan, this.state.rows.length)
            }, () => this.shouldUpdateVisibleArea = false);
        }
    }
    watchScroll = () => {
        this.scrollWatcher = setInterval(() => {
            if (this.shouldUpdateVisibleArea) {
                this.updateVisibleArea();
            }
        }, 50);
    }
    unWatchScroll = () => {
        clearInterval(this.scrollWatcher);
    }

    getColumns = () => {
        return this.state.columns.filter(col => !col.hidden);
    }
    getMaxHeight = () => {
        return this.props.rowsToDisplay * this.state.rowHeight;
    }
    getEstimatedHeight = () => {        
        return this.getMaxHeight() + this.toolbarHeight + this.props.scrollContext.scrollbarHeight + this.headerHeight;
    }
    getTotalHeight = () => {
        return this.state.rows.length * this.state.rowHeight;
    }

    moveHighlight = (direction) => {

    }

    getMeasures = () => {
        return this.state.columns.filter(d => !d.hidden).reduce((measures, col) => {
            if (col.fixed === columnFixed.left) {
                measures.widthLeft = measures.widthLeft + col.width;
            }
            else if (col.fixed === columnFixed.right) {
                measures.widthRight = measures.widthRight + col.width;
            }
            else {
                measures.widthCenter = measures.widthCenter + col.width;
            }

            return measures;
        }, {
            widthLeft: 0,
            widthRight: 0,
            widthCenter: 0
        });
    }

    getApi() {
        return {
            addRow: this.addRow,
            refresh: this.refresh,
            rollback: this.rollback
        };
    }

    isVScrollActive = () => {
        //Por performance
        const pane = document.querySelector("#" + this.id + " .gr-body-pane");

        return pane ? pane.clientHeight < this.getTotalHeight() : false;
    }
    
    render() {
        const columns = this.getColumns();
        const measures = this.getMeasures();
        const totalHeight = this.getTotalHeight();

        return (
            <ScrollSync onScrollBegin={this.props.scrollContext.onScrollBegin}>
                <GridContainer
                    id={this.props.id}
                    isInitializing={this.state.isInitializing}
                    height={this.getEstimatedHeight()}
                    ref={this.bodyRef}
                >
                    <Toolbar
                        menuItems={this.state.menuItems}
                        refresh={this.refresh}
                        commit={this.commit}
                        rollback={this.rollback}
                        deleteRow={this.deleteRow}
                        addRow={this.addRow}
                        performMenuItemAction={this.performMenuItemAction}
                    />
                    <GridContentContainer
                        ref={this.containerRef}
                        isResizing={this.state.isResizing}
                        columnResizeEnd={this.columnResizeEnd}
                        columnResizeChange={this.columnResizeChange}
                        toggleFilterBar={this.toggleFilterBar}
                        getCellHighlight={this.getCellHighlight}
                    >                    
                        <HeaderPane
                            columns={columns}
                            rows={this.state.rows}
                            selection={this.state.selection}
                            sorts={this.state.sorts}
                            filters={this.state.filters}
                            filterStatus={this.state.filterStatus}
                            widthLeft={measures.widthLeft}
                            widthRight={measures.widthRight}
                            highlightLast={this.state.highlightLast}
                            hasFixedColumnsLeft={this.hasFixedColumnsLeft}
                            hasFixedColumnsRight={this.hasFixedColumnsRight}
                            enableSelection={this.props.enableSelection}
                            applySort={this.applySort}
                            columnResizeBegin={this.columnResizeBegin}
                            applyFilter={this.applyFilter}
                            updateFilter={this.updateFilter}
                            updateColumnOrder={this.updateColumnOrder}
                            invertSelection={this.invertSelection}
                            totalHeight={totalHeight}
                            isSelectionInverted={this.state.isSelectionInverted}
                            isResizing={this.state.isResizing}
                            isVScrollActive={this.isVScrollActive}
                        />
                        <BodyPane
                            columns={columns}
                            rows={this.state.rows}
                            selection={this.state.selection}
                            highlight={this.state.highlight}
                            enableSelection={this.props.enableSelection}
                            hasFixedColumnsLeft={this.hasFixedColumnsLeft}
                            hasFixedColumnsRight={this.hasFixedColumnsRight}
                            widthLeft={measures.widthLeft}
                            widthRight={measures.widthRight}
                            addCellHighlight={this.addCellHighlight}
                            addCellHighlightGroup={this.addCellHighlightGroup}
                            rowHeight={this.state.rowHeight}                            
                            rowDisplayStart={this.state.rowDisplayStart}
                            rowDisplayEnd={this.state.rowDisplayEnd}
                            rowTotalRows={this.state.rows.length}
                            maxHeight={this.getMaxHeight()}                            
                            performButtonAction={this.performButtonAction}
                            updateScrollPosition={this.updateScrollPosition}
                            updateCellValue={this.updateCellValue}                            
                            updateSelection={this.updateSelection}
                            openDropdown={this.openDropdown}
                            isSelectionInverted={this.state.isSelectionInverted}
                            isFetching={this.state.isFetching}
                            isResizing={this.state.isResizing}                            
                        />
                        <GridScrollPane
                            widthLeft={measures.widthLeft}
                            widthRight={measures.widthRight}
                            widthCenter={measures.widthCenter}
                            isVScrollActive={this.isVScrollActive}
                        />
                        <ColumnResizeMarker
                            left={this.getResizeMarkerPosition()}
                            height={this.getResizeMarkerHeight(this.props.scrollContext.scrollbarHeight)}
                            isResizing={this.state.isResizing}
                        />                        
                    </GridContentContainer>
                    <LoadingSkeleton
                        height={this.getEstimatedHeight()}
                        isInitializing={this.state.isInitializing}
                    />
                    <GridDropdown
                        columnId={this.state.dropdownColumnId}
                        rowId={this.state.dropdownRowId}
                        show={this.state.dropdownShow}
                        items={this.state.dropdownItems}
                        left={this.state.dropdownLeft}
                        top={this.state.dropdownTop}
                        closeDropdown={this.closeDropdown}
                        
                        onClick={this.dropdownClick}
                    />
                </GridContainer>
            </ScrollSync>
        );
    }
}

export const Grid = withPageContext(withGridDataProvider(withScrollContext(InternalGrid)));