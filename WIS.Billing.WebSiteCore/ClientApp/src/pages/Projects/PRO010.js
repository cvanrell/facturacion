import React, { Component } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import Button from 'react-bootstrap/Button'
import { Form, Field, SubmitButton, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export class PRO010 extends Component {

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

                <Form
                    id="PRO010_form_1"
                //initialValues={initialValues}
                //validationSchema={validationSchema}
                >
                    <div className="row col-12">
                        <fieldset className="row" >

                            <div className="row col-12">
                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="Description">{("Descripcion")}</label>
                                        <Field name="Description" />
                                        <StatusMessage for="Description" />
                                    </div>
                                </div>
                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="Address">{("Direccion")}</label>
                                        <Field name="Address" />
                                        <StatusMessage for="Address" />
                                    </div>
                                </div>

                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="RUT">{("RUT")}</label>
                                        <Field name="RUT" />
                                        <StatusMessage for="RUT" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </Form>

                <div className="row mb-4">
                    <div className="col">
                        <Grid id="PRO010_grid_1" rowsToFetch={30} rowsToDisplay={30} enableSelection />
                    </div>
                </div>
            </Page>
        );
    }


}