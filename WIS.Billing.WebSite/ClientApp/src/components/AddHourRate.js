import React, { Component } from 'react';

export class AddHourRate extends Component {
    constructor(props) {
        super(props);

        this.state = {
            title: "", loading: true, hourRateData: {}, currencyList: [], periodicityList:[]
        };

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


        var hourRateId = this.props.match.params["hourRateId"];

        // This will set state for Edit hourRate  
        if (typeof hourRateId !== 'undefined') {
            fetch('api/HourRate/Details/' + hourRateId)
                .then(response => response.json())
                .then(data => {
                    this.setState({ title: "Edit", loading: false, hourRateData: data });
                })
                .catch(function (error) { console.log(error); });
        }
        // This will set state for Add hourRate
        else {
            this.state = {
                title: "Create", loading: false, hourRateData: {}
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
        
        // PUT request for Edit hourRate.  
        if (this.state.hourRateData.id) {
            fetch('api/HourRate/Edit', {
                method: 'PUT',
                body: data
            })//.then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchHourRate");
                });
        }

        // POST request for Add hourRate.  
        else {
            fetch('api/HourRate/Create', {
                method: 'POST',
                body: data
            })//.then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchHourRate");
                });
        }
    }

    // This will handle Cancel button click event.  
    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchHourRate");
    }

    // Returns the HTML Form to the render() method.  
    renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="id" value={this.state.hourRateData.id} />
                </div>
                <div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Rate">Tarifa</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="rate" defaultValue={this.state.hourRateData.rate} required />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Currency">Moneda</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="currency" defaultValue={this.state.hourRateData.currency} required>
                            <option value="">-- Seleccione la moneda --</option>
                            {currencyList.map(currency =>
                                <option key={currency.currency} value={currency.currency}>{currency.currencyDescription}</option>
                            )}
                        </select>
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="AdjustmentPeriodicity">Periodicidad del ajuste</label>
                    <div className="col-md-4">
                        <select className="form-control" data-val="true" name="adjustmentPeriodicity" defaultValue={this.state.hourRateData.periodicity} required>
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
                <h1>{this.state.title}</h1>
                <h3>HourRate</h3>
                <hr />
                {contents}
            </div>
        );
    }
}  