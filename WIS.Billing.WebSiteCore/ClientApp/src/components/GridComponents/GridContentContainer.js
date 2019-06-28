import React, { Component } from 'react';

class InternalGridContentContainer extends Component {
    handleMouseUp = () => {
        if (this.props.isResizing)
            this.props.columnResizeEnd();
    }
    handleMouseMove = (evt) => {
        if (this.props.isResizing)
            this.props.columnResizeChange(evt.clientX);
        else
            return false;
        //TODO: Something
    }
    handleMouseLeave = () => {
        if (this.props.isResizing)
            this.props.columnResizeEnd();
    }
    handleKeyDown = (evt) => {
        if (evt.ctrlKey && evt.which === 70) {
            this.props.toggleFilterBar(evt);
        }
        else if (evt.ctrlKey && evt.which === 67) {
            this.props.getCellHighlight(evt);
        }
    }

    render() {
        return (
            <div
                className="gr-content-container"                
                ref={this.props.forwardedRef}
                onKeyDown={this.handleKeyDown}
                onMouseUp={this.handleMouseUp}
                onMouseMove={this.handleMouseMove}
                onMouseLeave={this.handleMouseLeave}
                tabIndex="0"
            >
                {this.props.children}
            </div>
        );
    };
}

export const GridContentContainer = React.forwardRef((props, forwardedRef) => {
    return (
        <InternalGridContentContainer forwardedRef={forwardedRef} {...props}>
            {props.children}
        </InternalGridContentContainer>
    );
});