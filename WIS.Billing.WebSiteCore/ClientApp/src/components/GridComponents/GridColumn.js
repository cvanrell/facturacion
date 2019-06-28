import React, { Component, useImperativeHandle, useRef } from 'react';
import { ColumnSortButton } from './GridColumnSortButton';
import { DragSource, DropTarget } from 'react-dnd';

class InternalColumn extends Component {
    handleMouseDown = (evt) => {
        if (this.props.forwardedRef.current) {
            const elementOffset = this.props.resizeLeft ? this.props.forwardedRef.current.offsetLeft : this.props.forwardedRef.current.offsetLeft - this.props.getScrollLeft();
            const elementWidth = this.props.forwardedRef.current.offsetWidth;

            this.props.columnResizeBegin(this.props.columnId, elementOffset, elementWidth, evt.clientX, this.props.resizeLeft);

            evt.preventDefault();
            evt.stopPropagation();
        }
    }

    getSortInfo = () => {
        const index = this.props.sorts.findIndex(s => s.columnId === this.props.columnId);
        const sort = this.props.sorts[index];

        return {
            direction: sort ? sort.direction : 0,
            order: (index >= 0 ? index + 1 : null)
        };
    }

    getColumnStyle = () => {
        return {
            opacity: this.props.isDragging ? 0.5 : 1,
            minWidth: this.props.width
        };
    }
    getClassName = () => {
        const displayBorder = this.props.displayBorderLeft && this.props.isFirst ? " display-border-left" : "";

        return "gr-header-label" + displayBorder;
    }

    render() {
        const sortInfo = this.getSortInfo();

        if (this.props.resizeLeft) {
            return (
                <div className="gr-header-label fixed-right" style={this.getColumnStyle()} ref={this.props.forwardedRef}>                    
                    <div className="gr-col-resize" onMouseDown={this.handleMouseDown}>
                        <div />
                    </div>
                    <ColumnSortButton
                        columnId={this.props.columnId}
                        direction={sortInfo.direction}
                        order={sortInfo.order}
                        applySort={this.props.applySort}
                    />
                    <div className="gr-col-name">
                        <strong>{this.props.name}</strong>
                    </div>
                    <div className="gr-col-banishing" />
                </div>
            );
        }

        return (
            <div className={this.getClassName()} style={this.getColumnStyle()} ref={this.props.forwardedRef}>
                <ColumnSortButton
                    columnId={this.props.columnId}
                    direction={sortInfo.direction}
                    order={sortInfo.order}                    
                    applySort={this.props.applySort}
                />
                <div className="gr-col-name">
                    <strong>{this.props.name}</strong>                
                </div>
                <div className="gr-col-resize" onMouseDown={this.handleMouseDown}>
                    <div />
                </div>
                <div className="gr-col-banishing" />
            </div>
        );
    }
}

const ForwardedColumn = React.forwardRef(({ connectDragSource, connectDropTarget, ...props }, ref) => {
    const elementRef = useRef(null);

    connectDragSource(elementRef);
    connectDropTarget(elementRef);

    useImperativeHandle(ref, () => ({
        getNode: () => elementRef.current
    }));

    return (
        <InternalColumn forwardedRef={elementRef} {...props} />
    );
});

const columnSource = {
    beginDrag: props => ({ id: props.columnId, index: props.order }),
    canDrag: (props, monitor) => !props.isResizing
};

function collect(connect, monitor) {
    return {
        connectDragSource: connect.dragSource(),
        isDragging: monitor.isDragging()
    };
}

export const Column = DropTarget(
    "column",
    {
        drop(props, monitor, component) {
            if (!component) {
                return null;
            }

            // node = HTML Div element from imperative API
            const node = component.decoratedRef.current.getNode();

            if (!node) {
                return null;
            }

            const item = monitor.getItem();

            const dragIndex = item.index;
            const hoverIndex = props.order;

            // Don't replace items with themselves
            if (dragIndex === hoverIndex) {
                return;
            }

            // Determine rectangle on screen
            const hoverBoundingRect = node.getBoundingClientRect();

            // Get vertical middle
            const hoverMiddleX =
                (hoverBoundingRect.right - hoverBoundingRect.left) / 2;

            // Determine mouse position
            const clientOffset = monitor.getClientOffset();

            // Get pixels to the top
            const hoverClientX = clientOffset.x - hoverBoundingRect.left;

            // Time to actually perform the action
            props.updateColumnOrder(item.id, props.columnId, dragIndex, hoverIndex, hoverClientX > hoverMiddleX);

            // Note: we're mutating the monitor item here!
            // Generally it's better to avoid mutations,
            // but it's good here for the sake of performance
            // to avoid expensive index searches.
            monitor.getItem().index = hoverIndex;
        }
    },
    (connect) => ({ connectDropTarget: connect.dropTarget() })
)(DragSource("column", columnSource, collect)(ForwardedColumn));