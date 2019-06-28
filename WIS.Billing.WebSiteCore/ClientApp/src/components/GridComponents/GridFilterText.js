import React, { Component } from 'react';

export class FilterText extends Component {
    constructor(props) {
        super(props);

        this.inputRef = React.createRef();
    }

    componentDidMount() {
        if (this.props.highlightLast && this.inputRef.current && this.props.columnId === this.props.highlightLast.columnId) {
            this.inputRef.current.focus();
        }
    }

    handleKeyUp = (evt) => {
        if (evt.which === 13) {
            evt.preventDefault();

            this.props.applyFilter();
        }
    }
    handleChange = (evt) => {
        this.props.updateFilter(this.props.columnId, evt.target.value);
    }

    render() {
        return (
            <div>
                <input ref={this.inputRef} defaultValue={this.props.value} onChange={this.handleChange} onKeyUp={this.handleKeyUp} />
                <div className="gr-filter-search-icon">
                    <i className="fas fa-search" />
                </div>
            </div>
        );
    }
}