import React, { Component } from 'react';

export class AddMaintenance extends Component {
    constructor(props) {
        super(props);

        this.state = {
            title: "", loading: true, clientList: [], currencyList: [], periodicityList: [], maintenanceData: {
                client: {}
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

        fetch('api/Utils/GetPeriodicityList')
            .then(response => response.json())
            .then(data => {
                this.setState({ periodicityList: data });
            }); 

        var maintenanceId = this.props.match.params["maintenanceId"];

        // This will set state for Edit maintenance  
        if (typeof maintenanceId !== 'undefined') {
            fetch('api/Maintenance/Details/' + maintenanceId)
                .then(response => response.json())
                .then(data => {
                    this.setState({ title: "Editar mantenimiento", loading: false, maintenanceData: data });
                })
                .catch(function (error) { console.log(error); });
        }
        // This will set state for Add maintenance
        else {
            this.state = {
                title: "Crear mantenimiento", loading: false, clientList: [], currencyList: [], periodicityList: [], maintenanceData: {
                    client: {}
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
        
        // PUT request for Edit maintenance.  
        if (this.state.maintenanceData.id) {
            fetch('api/Maintenance/Edit', {
                method: 'PUT',
                body: data
            })//.then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchmaintenance");
                });
        }

        // POST request for Add maintenance.  
        else {
            fetch('api/Maintenance/Create', {
                method: 'POST',
                body: data
            })//.then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchmaintenance");
                });
        }
    }

    // This will handle Cancel button click event.  
    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchmaintenance");
    }

    // Returns the HTML Form to the render() method.  
    renderCreateForm(clientList, currencyList, periodicityList) {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="id" value={this.state.maintenanceData.id} />
                </div>
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Description">Descripción</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="description" defaultValue={this.state.maintenanceData.description} required />
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Client">Cliente</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="client.id" defaultValue={this.state.maintenanceData.client.id} required>
                            <option value="">-- Seleccione el cliente --</option>
                            {clientList.map(client =>
                                <option key={client.id} value={client.id}>{client.description}</option>
                            )}
                        </select>
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Amount" >Monto</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="Amount" defaultValue={this.state.maintenanceData.amount} required />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Currency">Moneda</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="currency" defaultValue={this.state.maintenanceData.currency} required>
                            <option value="">-- Seleccione la moneda --</option>
                            {currencyList.map(currency =>
                                <option key={currency.currency} value={currency.currency}>{currency.currencyDescription}</option>
                            )}
                        </select>
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Periodicity">Periodicidad</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="periodicity" defaultValue={this.state.maintenanceData.periodicity} required>
                            <option value="">-- Seleccione la periodicidad --</option>
                            {periodicityList.map(periodicity =>
                                <option key={periodicity.periodicity} value={periodicity.periodicity}>{periodicity.periodicityDescription}</option>
                            )}
                        </select>
                    </div>
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