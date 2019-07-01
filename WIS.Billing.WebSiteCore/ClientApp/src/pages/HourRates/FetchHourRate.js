import React, { Component } from 'react';
import { Link, NavLink } from 'react-router-dom';
import ReactTable from "react-table";
import "react-table/react-table.css";

export class FetchHourRate extends Component {
    displayName = FetchHourRate.name

    constructor(props) {
        super(props);
        this.state = { hourRates: this.props.hourRates, loading: this.props.loading };

        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);

    }
       
    // Handle Delete request for an HourRate  
    handleDelete(id) {
        if (!window.confirm("Do you want to delete HourRate with Id: " + id))
            return;
        else {
            fetch('api/HourRate/Delete/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        hourRates: this.state.hourRates.filter((rec) => {
                            return rec.id !== id;
                        })
                    });
            });
        }
    }

    handleEdit(id) {
        this.props.history.push("/HourRate/edit/" + id);
    }

    // Returns the HTML table to the render() method.  
    renderHourRateTable(data) {
        return (
            <ReactTable
                data={data}
                columns={[
                    {
                        Header: 'Descripción',
                        id: "description",
                        accessor: data => data.description
                    },
                    {
                        Header: 'Valor hora',
                        id: "hourRate",
                        accessor: data => data.rate
                    },
                    {
                        Header: 'Moneda',
                        id: "currencyDescription",
                        accessor: data => data.currencyDescription
                    },
                    {
                        Header: 'Acciones',
                        id: "actions",
                        accessor: data => data.id,
                        Cell: ({ value }) => <a onClick={() => this.handleEdit(value)}>Editar</a>
                    }
                ]}
                defaultPageSize={10}
                className="-striped -highlight"
            />
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderHourRateTable(this.state.hourRates);

        return (
            <div>
                <h3>Tarifas de horas</h3>
                <p>
                    <Link to="/addHourRate">Nueva tarifa</Link>
                </p>
                {contents}
            </div>
        );
    }
}