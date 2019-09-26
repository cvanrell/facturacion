import React, { Component } from 'react';
import { CellContent } from './GridCellContent';
import { gridStatus } from '../Enums';

export class Cell extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isHovering: false
        };

        this.cellRef = React.createRef();
        this.errorMsgRef = React.createRef();
    }

    shouldComponentUpdate(nextProps, nextState) {
        return this.props.content.value !== nextProps.content.value
            || this.props.content.old !== nextProps.content.old
            || this.props.content.editable !== nextProps.content.editable
            || this.props.content.status !== nextProps.content.status
            || this.props.content.message !== nextProps.content.message
            || this.props.column.width !== nextProps.column.width
            || this.state !== nextState
            || this.isHighlighted(this.props.highlight) !== this.isHighlighted(nextProps.highlight);
    }

    handleMouseEnter = () => {
        this.setState({
            isHovering: true
        });
    }
    handleMouseLeave = () => {
        this.setState({
            isHovering: false
        });
    }
    handleKeyDown = (evt) => {
        //TODO: Mejorar navegación con flechas
        /*if (evt.which === 37) {
            if (evt.target.previousElementSibling)
                evt.target.previousElementSibling.focus();

            evt.preventDefault();
        }

        if (evt.which === 39) {
            if (evt.target.nextElementSibling)
                evt.target.nextElementSibling.focus();

            evt.preventDefault();
        }

        if (evt.which === 38) {
            const index = [...evt.target.parentElement.children].indexOf(evt.target);

            if (evt.target.parentElement.previousElementSibling && evt.target.parentElement.previousElementSibling.children[index])
                evt.target.parentElement.previousElementSibling.children[index].focus();

            evt.preventDefault();
        }

        if (evt.which === 40) {
            const index = [...evt.target.parentElement.children].indexOf(evt.target);

            if (evt.target.parentElement.nextElementSibling && evt.target.parentElement.nextElementSibling.children[index])
                evt.target.parentElement.nextElementSibling.children[index].focus();

            evt.preventDefault();
        }*/
    }

    getClassName = () => {
        const selected = this.isHighlighted(this.props.highlight) ? " selected" : "";
        const error = this.props.content.status === gridStatus.error ? " error" : "";
        const displayBorder = this.props.cellDisplayBorderLeft ? " display-border-left" : "";
        
        return "gr-cell" + selected + error + displayBorder;
    }
    getErrorClassName = () => {
        const hidden = !this.state.isHovering ? "hidden" : "";

        return "gr-cell-error-message " + hidden;
    }

    getStyle = () => {
        return {
            minWidth: this.props.column.width
        };
    }
    getErrorStyle = () => {
        if (this.errorMsgRef.current && this.state.isHovering) {
            const diff = (this.cellRef.current.offsetWidth - this.errorMsgRef.current.offsetWidth) / 2;

            const left = this.cellRef.current.getBoundingClientRect().left + diff;
            const top = this.cellRef.current.getBoundingClientRect().top - this.errorMsgRef.current.offsetHeight - 5;

            return {
                left: left,
                top: top
            };
        }

        return {
            left: 0,
            top: 0
        };
    }

    renderCellContent = () => {
        return (
            <CellContent
                type={this.props.column.type}
                rowId={this.props.rowId}
                column={this.props.column}
                disabledButtons={this.props.disabledButtons}
                rowIsNew={this.props.rowIsNew}
                rowIsDeleted={this.props.rowIsDeleted}
                content={this.props.content}
                addHighlight={this.addHighlight}
                updateCellValue={this.props.updateCellValue}
                performButtonAction={this.props.performButtonAction}
                openDropdown={this.props.openDropdown}
            />
        );
    }
    addHighlight = (evt) => {
        if (!evt.shiftKey) {
            const clear = !evt.ctrlKey;

            this.props.addCellHighlight({
                rowId: this.props.rowId,
                columnId: this.props.column.id,
                index: this.props.rowIndex
            }, clear);
        }
        else {
            this.props.addCellHighlightGroup({
                rowId: this.props.rowId,
                columnId: this.props.column.id,
                index: this.props.rowIndex
            });
        }
    }
    
    isHighlighted = (highlight) => {
        return highlight && highlight.columns.indexOf(this.props.column.id) >= 0;
    }

    render() {
        if (this.props.content.status === gridStatus.error) {
            return (
                <div
                    className={this.getClassName()}
                    style={this.getStyle()}
                    ref={this.cellRef}
                    onMouseEnter={this.handleMouseEnter}
                    onMouseLeave={this.handleMouseLeave}
                    onKeyDown={this.handleKeyDown}
                    onClick={this.addHighlight}
                    tabIndex={0}
                >
                    {this.renderCellContent()}
                    <div className={this.getErrorClassName()} style={this.getErrorStyle()} ref={this.errorMsgRef} >
                        {this.props.content.message}
                    </div>
                </div>
            );
        }

        return (
            <div
                className={this.getClassName()}
                style={this.getStyle()}
                ref={this.cellRef}
                onKeyDown={this.handleKeyDown}
                onClick={this.addHighlight}
                tabIndex={0}
            >
                {this.renderCellContent()}
            </div>
        );
    }
}