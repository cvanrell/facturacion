import React, { Component } from 'react';
import InputText from './GridInputText';
import { textAlign } from '../Enums';

export class CellText extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isEditing: false
        };

        this.inputRef = React.createRef();
    }

    shouldComponentUpdate(nextProps, nextState) {
        return this.props.content.value !== nextProps.content.value
            || this.props.content.old !== nextProps.content.old
            || this.state.isEditing !== nextState.isEditing;
    }
    componentDidUpdate() {
        if (this.state.isEditing)
            this.inputRef.current.focus();
    }

    handleDoubleClick = (evt) => {
        if (this.props.content.editable) {
            this.setState({
                isEditing: true
            });
        }
    }
    handleKeyUp = (evt) => {
        if (evt.which === 13 && this.state.isEditing && this.inputRef) {
            evt.preventDefault();

            this.inputRef.current.blur();
        }
    }
    handleBlur = (evt) => {
        this.props.updateCellValue(this.props.rowId, this.props.column.id, evt.target.value);

        this.setState({
            isEditing: false
        });
    }

    getStyle = () => {
        let align;

        switch (this.props.column.textAlign) {
            case textAlign.center:
                align = "center";
                break;
            case textAlign.right:
                align = "right";
                break;
            default:
                align = "left";
        }

        return {
            textAlign: align 
        };
    }

    render() {
        var style = this.getStyle();

        if (!this.state.isEditing || !this.props.content.editable) {
            return (
                <div onDoubleClick={this.handleDoubleClick} style={style} className="gr-cell-content-text">
                    {this.props.content.value}
                </div>
            );
        }

        return (
            <InputText
                ref={this.inputRef}
                value={this.props.content.value}
                onBlur={this.handleBlur}
                onKeyUp={this.handleKeyUp}
                style={style}
                className="gr-cell-content-input"
            />
        );
    }
}