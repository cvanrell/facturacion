import React, { Component } from 'react';
import { textAlign } from '../Enums';
import DatePicker from 'react-datepicker';
import { getDateTimeString } from '../DateTimeUtil';

export class CellDateTime extends Component {
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

    handleDblClick = () => {
        this.setState({
            isEditing: true
        }, () => {
            if (this.dateRef.current)
                this.dateRef.current.setFocus();
        });
    }
    handleChange = (date) => {
        this.props.updateCellValue(this.props.rowId, this.props.column.id, date.toISOString());
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
        const value = date ? getDateTimeString(date) : "";

        if (!this.props.content.editable) {
            return (
                <div
                    style={style}
                    className="gr-cell-content-text"
                >
                    {value}
                </div>
            );
        }
        else {
            if (this.state.isEditing) {
                return (
                    //Si se quiere aplicar al seleccionar, usar onSelect con handleBlur
                    <DatePicker
                        ref={this.dateRef}
                        selected={date}
                        showTimeInput
                        className="gr-cell-content-input"
                        style={style}
                        timeInputLabel="Hora: "
                        dateFormat="dd/MM/yyyy HH:mm:ss"
                        timeFormat="HH:mm"
                        onChange={this.handleChange}
                        shouldCloseOnSelect={false}
                        onClickOutside={this.handleBlur}
                    />
                );
            }
            else {
                return (
                    <div
                        style={style}
                        className="gr-cell-content-text"
                        onDoubleClick={this.handleDblClick}
                    >
                        {value}
                    </div>
                );
            }
        }
    }
}