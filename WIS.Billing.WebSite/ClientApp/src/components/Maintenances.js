import React, { Component } from 'react';
//import { render } from "react-dom";
import ReactTable from "react-table";
import "react-table/react-table.css";

export class Maintenances extends Component {
    displayName = Maintenances.name

    constructor(props) {
        super(props);
        this.state = { maintenances: [], loading: true };

        fetch('api/SampleData/GetMaintenances')
            .then(response => response.json())
            .then(data => {
                this.setState({ maintenances: data, loading: false });
            });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : (
                <ReactTable
                    data={this.state.maintenances}
                    columns={[
                        {
                            Header: 'Cliente',
                            id: "client",
                            accessor: data => data.client.description
                        },
                        {
                            Header: 'Monto',
                            id: "amount",
                            accessor: data => data.amount
                        },
                        {
                            Header: 'Moneda',
                            id: "currencydescription",
                            accessor: data => data.currencyDescription
                        },
                        {
                            Header: 'Periodicidad',
                            id: "periodicitydescription",
                            accessor: data => data.periodicityDescription
                        },
                        {
                            Header: 'Acciones',
                            id: "actions",
                            accessor: 'id',
                            Cell: ({ value }) => (<a onClick={() => { console.log('clicked value', value) }}>Button</a>)
                        }
                    ]}
                    defaultPageSize={10}
                    className="-striped -highlight"
                />
            );

        return (
            <div>
                {contents}
            </div>
        );
    }
}