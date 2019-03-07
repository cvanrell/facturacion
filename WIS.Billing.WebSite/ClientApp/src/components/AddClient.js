import React, { Component } from 'react';
import { FetchHourRate } from './FetchHourRate';

export class AddClient extends Component {
    constructor(props) {
        super(props);

        this.state = {
            title: "", loading: true, clientData: {}
        };

        var clientId = this.props.match.params["clientId"];

        // This will set state for Edit client  
        if (typeof clientId !== 'undefined') {
            fetch('api/Client/Details/' + clientId)
                .then(response => response.json())
                .then(data => {
                    this.setState({ title: "Editar cliente", loading: false, clientData: data });
                })
                .catch(function (error) { console.log(error); });
        }
        // This will set state for Add client
        else {
            this.state = {
                title: "Crear cliente", loading: false, clientData: {}
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
        
        // PUT request for Edit client.  
        if (this.state.clientData.id) {
            fetch('api/Client/Edit', {
                method: 'PUT',
                body: data
            })//.then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchclient");
                });
        }

        // POST request for Add client.  
        else {
            fetch('api/Client/Create', {
                method: 'POST',
                body: data
            })//.then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchclient");
                });
        }
    }

    // This will handle Cancel button click event.  
    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchclient");
    }

    // Returns the HTML Form to the render() method.  
    renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="id" value={this.state.clientData.id} />
                </div>
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Description">Descripción</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="description" defaultValue={this.state.clientData.description} required />
                    </div>
                </div>
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="RUT">RUT</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="rut" defaultValue={this.state.clientData.rut} required />
                    </div>
                </div>
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Address">Domicilio</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="address" defaultValue={this.state.clientData.address} required />
                    </div>
                </div>
                <div className="form-group">
                    <FetchHourRate loading={this.state.loading} hourRates={this.state.clientData.hourRates} />
                </div>
                <div className="form-group">
                    <button type="submit" className="btn btn-default">Guardar</button>
                    <button className="btn" onClick={this.handleCancel}>Cancelar</button>
                </div>
            </form >
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm();

        return (
            <div>
                <h3>{this.state.title}</h3>
                <hr />
                {contents}
            </div>
        );
    }
}  