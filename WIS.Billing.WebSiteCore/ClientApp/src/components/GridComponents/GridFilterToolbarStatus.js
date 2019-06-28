import React, { Component } from 'react';

export class FilterToolbarStatus extends Component {
    render() {
        return (
            <div className="gr-filter-status">
                <div style={{ marginRight: 5, color: "gray" }}>Filtro: </div><div style={{ backgroundColor: "#eaeaea", borderRadius:3, paddingLeft:2, paddingRight:2 }}>Default</div>
            </div>
        );
    }
}