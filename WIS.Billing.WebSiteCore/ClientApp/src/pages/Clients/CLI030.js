import React, { Component } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import Button from 'react-bootstrap/Button'
import { Form, Field, SubmitButton, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export class CLI030 extends Component {




    //fieldSetStyle = { border: "1px solid #ddd", margin: "10px", width: "100%" };


    validationSchema = {
    };

    //onAfterApplyFilter = (context, form, query, nexus) => {
    //    console.log("----- onAfterApplyFilter ")
    //    //nexus.getForm("DOC020_form_1").reset();
    //};
    //onAfterInitialize = (context, form, query, nexus) => {
    //    console.log("----- onAfterApplyFilter ")
    //    //nexus.getForm("DOC020_form_1").reset();
    //};



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

    render() {
        return (

            <Page
                icon="fas fa-file"
                title="Historico tarifa"
                {...this.props}
            >
                <Form
                    id="CLI030_form_1"
                //initialValues={initialValues}
                //validationSchema={validationSchema}
                >
                    <div className="row col-12">
                        <fieldset className="row" >

                            <div className="row col-12">
                                <div className="col-12">
                                    <div className="form-group">
                                        <label htmlFor="Description">{("Descripción")}</label>
                                        <Field name="Description" readOnly />
                                        <StatusMessage for="Description" />
                                    </div>
                                </div>                                
                            </div>
                        </fieldset>
                    </div>


                </Form>

                <h3 className="form-title">{("Tarifas de horas")}</h3>
                <div className="row mb-4">
                    <div className="col-12">
                        <Grid id="CLI030_grid_T" rowsToFetch={30} rowsToDisplay={15}
                        //onAfterApplyFilter={onAfterApplyFilter}
                        //onAfterInitialize={onAfterInitialize}
                        //enableExcelExport
                        />
                    </div>
                </div>                
            </Page>
        );
    }


}