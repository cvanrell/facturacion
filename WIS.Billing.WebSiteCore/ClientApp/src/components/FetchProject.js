import React, { Component } from 'react';
import { Link, NavLink } from 'react-router-dom';
import ReactTable from "react-table";
import "react-table/react-table.css";

export class FetchProject extends Component {
    displayName = FetchProject.name

    constructor(props) {
        super(props);
        this.state = { projects: [], loading: true };

        fetch('api/Project/Index')
            .then(response => response.json())
            .then(data => {
                this.setState({ projects: data, loading: false });
            });

        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);

    }
       
    // Handle Delete request for an Project  
    handleDelete(id) {
        if (!window.confirm("Do you want to delete Project with Id: " + id))
            return;
        else {
            fetch('api/Project/Delete/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        projects: this.state.projects.filter((rec) => {
                            return rec.id !== id;
                        })
                    });
            });
        }
    }

    handleEdit(id) {
        this.props.history.push("/Project/edit/" + id);
    }

    // Returns the HTML table to the render() method.  
    renderProjectTable(data) {
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
                        Header: 'Cuota',
                        id: "installments",
                        accessor: data => data.installments
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
            : this.renderProjectTable(this.state.projects);

        return (
            <div>
                <h1>Proyectos</h1>
                <p>Listado de proyectos WIS</p>
                <p>
                    <Link to="/addProject">Nuevo proyecto</Link>
                </p>
                {contents}
            </div>
        );
    }
}