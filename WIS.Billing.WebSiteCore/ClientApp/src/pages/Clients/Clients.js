import React, { Component } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import { Form, Field, SubmitButton, StatusMessage } from '../../components/FormComponents/Form';

export default function Clients(props) {


    return (
        <Page {...this.props}>
            <div className="row mb-4">
                <div className="col-6">
                    <Grid id="Clients_grid_1" rowsToFetch={30} rowsToDisplay={10} enableSelection />
                </div>                
            </div>            
        </Page>
    );

}