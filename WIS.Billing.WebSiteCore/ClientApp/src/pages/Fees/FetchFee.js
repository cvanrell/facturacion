import React, { Component } from 'react';
import { Link, NavLink } from 'react-router-dom';
import ReactTable from "react-table";
import "react-table/react-table.css";

export class FetchFee extends Component {
    displayName = FetchFee.name

    constructor(props) {
        super(props);
        this.state = { fees: this.props.fees, loading: this.props.loading };

        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);

    }
       
    // Handle Delete request for an Fee  
    handleDelete(id) {
        if (!window.confirm("Do you want to delete Fee with Id: " + id))
            return;
        else {
            fetch('api/Fee/Delete/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        fees: this.state.fees.filter((rec) => {
                            return rec.id !== id;
                        })
                    });
            });
        }
    }

    handleEdit(id) {
        this.props.history.push("/Fee/edit/" + id);
    }

    // Returns the HTML table to the render() method.  
    renderFeeTable(data) {
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
                        Header: 'Valor cuota',
                        id: "fee",
                        accessor: data => data.amount
                    },
                    {
                        Header: 'IVA',
                        id: "iva",
                        accessor: data => data.iva
                    },
                    {
                        Header: 'Total',
                        id: "total",
                        accessor: data => data.total
                    },
                    {
                        Header: 'Mes',
                        id: "month",
                        accessor: data => data.month
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
            : this.renderFeeTable(this.state.fees);

        return (
            <div>
                <h3>Cuotas del proyecto</h3>
                <hr/>
                <p>
                    <Link to="/addFee">Nueva cuota</Link>
                </p>
                {contents}
            </div>
        );
    }
}