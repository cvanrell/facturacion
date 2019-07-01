import React, { Component } from 'react';
import { Link, NavLink } from 'react-router-dom';
import ReactTable from "react-table";
import "react-table/react-table.css";

export class FetchMaintenance extends Component {
    displayName = FetchMaintenance.name

    constructor(props) {
        super(props);
        this.state = { maintenances: [], loading: true };

        fetch('api/Maintenance/GetMaintenances')
            .then(response => response.json())
            .then(data => {
                this.setState({ maintenances: data, loading: false });
            });

        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);

    }
       
    // Handle Delete request for an Maintenance  
    handleDelete(id) {
        if (!window.confirm("Do you want to delete Maintenance with Id: " + id))
            return;
        else {
            fetch('api/Maintenance/Delete/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        maintenances: this.state.maintenances.filter((rec) => {
                            return rec.id !== id;
                        })
                    });
            });
        }
    }

    handleEdit(id) {
        this.props.history.push("/Maintenance/edit/" + id);
    }

    // Returns the HTML table to the render() method.  
    renderMaintenanceTable(data) {
        return (
            <ReactTable
                data={data}
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
            : this.renderMaintenanceTable(this.state.maintenances);

        return (
            <div>
                <h1>Mantenimientos</h1>
                <p>Listado de mantenimientos</p>
                <p>
                    <Link to="/addMaintenance">Nuevo mantenimiento</Link>
                </p>
                {contents}
            </div>
        );
    }
}