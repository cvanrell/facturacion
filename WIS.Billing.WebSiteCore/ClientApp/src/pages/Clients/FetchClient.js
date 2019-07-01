import React, { Component } from 'react';
import { Link, NavLink } from 'react-router-dom';
import ReactTable from "react-table";
import "react-table/react-table.css";

export class FetchClient extends Component {
    displayName = FetchClient.name

    constructor(props) {
        super(props);
        this.state = { clients: [], loading: true };

        fetch('api/Client/GetClients')
            .then(response => response.json())
            .then(data => {
                this.setState({ clients: data, loading: false });
            });

        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);

    }
       
    // Handle Delete request for an Client  
    handleDelete(id) {
        if (!window.confirm("Do you want to delete Client with Id: " + id))
            return;
        else {
            fetch('api/Client/Delete/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        clients: this.state.clients.filter((rec) => {
                            return rec.id !== id;
                        })
                    });
            });
        }
    }

    handleEdit(id) {
        this.props.history.push("/Client/edit/" + id);
    }

    // Returns the HTML table to the render() method.  
    renderClientTable(data) {
        return (
            <ReactTable
                data={data}
                columns={[
                    {
                        Header: 'Cliente',
                        id: "client",
                        accessor: data => data.description
                    },
                    {
                        Header: 'RUT',
                        id: "rut",
                        accessor: data => data.rut
                    },
                    {
                        Header: 'Domicilio',
                        id: "address",
                        accessor: data => data.address
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
            : this.renderClientTable(this.state.clients);

        return (
            <div>
                <h1>Clientes</h1>
                <p>Listado de clientes WIS</p>
                <p>
                    <Link to="/addClient">Nuevo cliente</Link>
                </p>
                {contents}
            </div>
        );
    }
}