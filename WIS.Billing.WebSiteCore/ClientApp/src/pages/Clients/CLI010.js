import React, { Component } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import Button from 'react-bootstrap/Button'
import { Form, Field, SubmitButton, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export class CLI010 extends Component {

    //initialValues = {
    //    name: "Exito",
    //    lastname: "",
    //    password: "Pass",
    //    type: 2
    //};

    validationSchema = {
        name: Yup.string()
            .min(2)
            .max(8)
            .required(),
        password: Yup.string()
            .min(6)
            .required(),
        type: Yup.number()
            .min(1)
            .max(3)
    };

    onBeforeButtonAction = (context, form, query, nexus) => {
        context.abortServerCall = true;

        //nexus.redirect("/stock/STO110");

        nexus.getForm("form_1").reset();
    }

    render() {
        return (
            <Page {...this.props}>
                
                <div className="row mb-4">
                    <div className="col">
                        <Grid id="CLI010_grid_1" rowsToFetch={30} rowsToDisplay={30} enableSelection />
                    </div>
                </div>
            </Page>
        );
    }
    

}