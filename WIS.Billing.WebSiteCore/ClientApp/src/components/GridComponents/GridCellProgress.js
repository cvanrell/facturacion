import React, { Component } from 'react';
import InputProgress from './GridInputProgress';

export class CellProgress extends Component {
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
            setTimeout(() => this.inputRef.current.focus(), 100); //Bug de firefox
    }

    handleDoubleClick = () => {
        if (this.props.content.editable) {
            this.setState({
                isEditing: true
            });
        }
    }
    handleKeyUp = (e) => {
        if (e.which === 13 && this.state.isEditing && this.inputRef) {
            e.preventDefault();

            this.inputRef.current.blur();
        }
    }
    handleBlur = (event) => {
        this.props.updateCellValue(this.props.rowId, this.props.column.id, event.target.value);

        this.setState({
            isEditing: false
        });
    }

    getStyle(value) {
        return {
            width: value + "%"
        };
    }
    getClassName() {
        const status = +this.props.content.value < 30 ? "low"
            : +this.props.content.value < 80 ? "mid"
            : "high";

        return "gr-cell-progress " + status;
    }

    render() {
        if (!this.state.isEditing || !this.props.content.editable) {
            return (
                <div onDoubleClick={this.handleDoubleClick} className={this.getClassName()}>
                    <div style={this.getStyle(this.props.content.value)} />
                </div>
            );
        }

        return (
            <InputProgress ref={this.inputRef} value={this.props.content.value} onBlur={this.handleBlur} onKeyUp={this.handleKeyUp} />
        );
    }
}