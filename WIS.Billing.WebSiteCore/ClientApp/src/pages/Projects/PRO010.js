import React, { Component } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import Button from 'react-bootstrap/Button'
import { Form, Field, FieldSelect, SubmitButton, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export class PRO010 extends Component {

    //initialValues = {
    //    name: "Exito",
    //    lastname: "",
    //    password: "Pass",
    //    type: 2
    //};

    validationSchema = {
        //Client: Yup.string().required(),
        //Currency: Yup.string().required(),
        //Description: Yup.string().required(),
    };

    onBeforeButtonAction = (context, form, query, nexus) => {
        context.abortServerCall = true;

        //nexus.redirect("/stock/STO110");

        nexus.getForm("form_1").reset();
    }

    //    <div className="col-4">
    //    <div className="form-group">
    //        <label htmlFor="IVA">{("IVA")}</label>
    //        <Field name="IVA" />
    //        <StatusMessage for="IVA" />
    //    </div>
    //</div>
    //    <div className="col-4">
    //        <div className="form-group">
    //            <label htmlFor="Total">{("Total")}</label>
    //            <Field name="Total" />
    //            <StatusMessage for="Total" />
    //        </div>
    //    </div>
    //    <div className="col-4">
    //        <div className="form-group">
    //            <label htmlFor="Amount">{("Monto")}</label>
    //            <Field name="Amount" />
    //            <StatusMessage for="Amount" />
    //        </div>
    //    </div>

    //    <div className="col-4">
    //        <div className="form-group">
    //            <label htmlFor="InitialDate">{("Fecha de inicio")}</label>
    //            <Field name="InitialDate" />
    //            <StatusMessage for="InitialDate" />
    //        </div>
    //    </div>                                


    render() {
        return (
            <Page {...this.props}>

                <Form
                    id="PRO010_form_1"
                    //initialValues={initialValues}
                    //validationSchema={validationSchema}
                >
                    <div className="col-12">
                        <fieldset className="col-12" >

                            <div className="col-12">
                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="Description">{("Descripcion")}</label>
                                        <Field name="Description" />
                                        <StatusMessage for="Description" />
                                    </div>
                                </div>

                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="Client">{("Cliente")}</label>
                                        <FieldSelect name="Client" />
                                        <StatusMessage for="Client" />
                                    </div>
                                </div>

                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="Amount">{("Monto")}</label>
                                        <Field name="Amount" />
                                        <StatusMessage for="Amount" />
                                    </div>
                                </div>
                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="Currency">{("Moneda")}</label>
                                        <FieldSelect name="Currency" />
                                        <StatusMessage for="Currency" />
                                    </div>
                                </div>


                            </div>
                        </fieldset>
                    </div>

                    <div className="row">
                        <div className="col">
                            <SubmitButton value={("Agregar proyecto")} />
                        </div>
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