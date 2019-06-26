import React, { Component } from 'react';
import { FetchFee } from './FetchFee';

export class AddProject extends Component {
    constructor(props) {
        super(props);

        this.state = {
            title: "", loading: true, clientList: [], currencyList: [], projectData: {
                client: {}, fees: []
            }
        };

        fetch('api/Client/GetClients')
            .then(response => response.json())
            .then(data => {
                this.setState({ clientList: data });
            });

        fetch('api/Utils/GetCurrencyList')
            .then(response => response.json())
            .then(data => {
                this.setState({ currencyList: data });
            });

        var projectId = this.props.match.params["projectId"];
        
        // This will set state for Edit project  
        if (typeof projectId !== 'undefined') {
            fetch('api/Project/Details/' + projectId)
                .then(response => response.json())
                .then(data => {
                    this.setState({ title: "Editar proyecto", loading: false, projectData: data });
                })
                .catch(function (error) { console.log(error); });
        }
        // This will set state for Add project
        else {
            this.state = {
                title: "Crear proyecto", loading: false, clientList: [], currencyList: [], projectData: {
                    client: {}, fees: []
                }
            };
        }

        // This binding is necessary to make "this" work in the callback  
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    // This will handle the submit form event.  
    handleSave(event) {
        event.preventDefault();
        const data = new FormData(event.target);

        // PUT request for Edit project.  
        if (this.state.projectData.id) {
            fetch('api/Project/Edit', {
                method: 'PUT',
                body: data
            })//.then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchproject");
                });
        }

        // POST request for Add project.  
        else {
            fetch('api/Project/Create', {
                method: 'POST',
                body: data
            })//.then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchproject");
                });
        }
    }

    // This will handle Cancel button click event.  
    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchproject");
    }

    // Returns the HTML Form to the render() method.  
    renderCreateForm(clientList, currencyList, periodicityList) {
         return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="Id" value={this.state.projectData.id} />
                </div>
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Description">Descripción</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="description" defaultValue={this.state.projectData.description} required />
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Client">Cliente</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="client.id" defaultValue={this.state.projectData.client.id} required>
                            <option value="">-- Seleccione el cliente --</option>
                            {clientList.map(client =>
                                <option key={client.id} value={client.id}>{client.description}</option>
                            )}
                        </select>
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Amount" >Monto</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="Amount" defaultValue={this.state.projectData.amount} required />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Currency">Moneda</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="Currency" defaultValue={this.state.projectData.currency} required>
                            <option value="">-- Seleccione la moneda --</option>
                            {currencyList.map(currency =>
                                <option key={currency.currency} value={currency.currency}>{currency.currencyDescription}</option>
                            )}
                        </select>
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Installments" >Cuotas</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="Installments" defaultValue={this.state.projectData.installments} required />
                    </div>
                </div>
                <div className="form-group">
                    <FetchFee loading={this.state.loading} fees={this.state.projectData.fees} />
                </div>
                <div className="form-group">
                    <button type="submit" className="btn btn-default">Guardar</button>
                    <button className="btn" onClick={this.handleCancel}>Cancelar</button>
                </div >
            </form >
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm(this.state.clientList, this.state.currencyList, this.state.periodicityList);

        return (
            <div>
                <h3>{this.state.title}</h3>
                <hr />
                {contents}
            </div>
        );
    }
}  