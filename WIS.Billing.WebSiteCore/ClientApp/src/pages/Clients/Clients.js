import React, { Component } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import { Form, Field, SubmitButton, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export default function Clients(props) {

    initialValues = {
        name: "Exito",
        lastname: "",
        password: "Pass",
        type: 2
    };

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

    return (
        <Page {...this.props}>
            <div className="row mb-4">
                <div className="col">
                    <Form
                        id="form_1"
                        initialValues={this.initialValues}
                        validationSchema={this.validationSchema}
                        onBeforeButtonAction={this.onBeforeButtonAction}
                    >
                        <div className="form-group">
                            <Field name="description" />
                            <StatusMessage for="description" />
                        </div>
                        <div className="form-group">
                            <Field name="address" type="input" readOnly />
                            <StatusMessage for="address" />
                        </div>
                        <div className="form-group">
                            <Field name="rut" type="input" />
                            <StatusMessage for="rut" />
                        </div>
                        
                        <SubmitButton value="Submit" />
                        <Button id="btnSubmit" value="Submit" className="btn btn-success" style={{ marginLeft: "10px" }} />
                    </Form>
                </div>
            </div>
            <div className="row mb-4">
                <div className="col">
                    <Grid id="Clients_grid_1" rowsToFetch={30} rowsToDisplay={30} enableSelection />
                </div>
            </div>
        </Page>
    );

}