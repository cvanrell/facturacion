import React, { Component } from 'react';
import { textAlign } from '../Enums';
import DatePicker from 'react-datepicker';
import { getDateString } from '../DateTimeUtil';

export class CellDate extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isEditing: false
        };

        this.dateRef = React.createRef();
    }

    shouldComponentUpdate(nextProps, nextState) {
        return this.state.isEditing !== nextState.isEditing
            || this.props.content.value !== nextProps.content.value
            || this.props.content.old !== nextProps.content.old
            || this.props.content.metadata !== nextProps.content.metadata;
    }

    handleChange = (date) => {
        this.props.updateCellValue(this.props.rowId, this.props.column.id, date.toISOString());
    }
    handleDblClick = () => {
        this.setState({
            isEditing: true
        }, () => {
            if (this.dateRef.current)
                this.dateRef.current.setFocus();
        });
    }
    handleBlur = () => {
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
        const style = this.getStyle();

        const date = this.props.content.value ? new Date(this.props.content.value) : null;
        const value = date ? getDateString(date) : "";

        if (!this.props.content.editable) {
            return (
                <div style={style} className="gr-cell-content-text">
                    {value}
                </div>
            );
        }
        else {
            if (this.state.isEditing) {
                return (
                    <DatePicker
                        ref={this.dateRef}
                        selected={date}                        
                        className="gr-cell-content-input"
                        style={style}
                        dateFormat="dd/MM/yyyy"
                        onChange={this.handleChange}
                        onBlur={this.handleBlur}
                    />
                );
            }
            else {
                return (
                    <div style={style} className="gr-cell-content-text" onDoubleClick={this.handleDblClick} >
                        {value}
                    </div>
                );
            }
        }
    }
}