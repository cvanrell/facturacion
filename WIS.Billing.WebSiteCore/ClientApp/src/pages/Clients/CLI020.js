﻿import React, { Component } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import Button from 'react-bootstrap/Button'
import { Form, Field, SubmitButton, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export class CLI020 extends Component {


    

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
                title="Edicion de clientes"//{t("DOC020_Sec0_pageTitle_Titulo")}
                {...this.props}
            >
                <Form
                    id="CLI020_form_1"
                    //initialValues={initialValues}
                    //validationSchema={validationSchema}
                    >
                    <div className="row col-12">
                        <fieldset className="row" >
                            
                            <div className="row col-12">
                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="Description">{("Descripcion")}</label>
                                        <Field name="Description" readOnly />
                                        <StatusMessage for="Description" />
                                    </div>
                                </div>
                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="Address">{("Direccion")}</label>
                                        <Field name="Address" readOnly />
                                        <StatusMessage for="Address" />
                                    </div>
                                </div>

                                <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="RUT">{("RUT")}</label>
                                        <Field name="RUT" readOnly />
                                        <StatusMessage for="RUT" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </Form>
                
            </Page>
        );
    }


}