import React, { Component } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import Button from 'react-bootstrap/Button'
import { Form, Field, SubmitButton, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export class CLI050 extends Component {




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
                title="Tarifas totales"
                {...this.props}
            >                

                <h3 className="form-title">{("Tarifas de horas")}</h3>
                <div className="row mb-4">
                    <div className="col-12">
                        <Grid id="CLI050_grid_T" rowsToFetch={30} rowsToDisplay={15}
                        //onAfterApplyFilter={onAfterApplyFilter}
                        //onAfterInitialize={onAfterInitialize}
                        //enableExcelExport
                        />
                    </div>
                </div>

                <h3 className="form-title">{("Tarifas de soporte")}</h3>
                <div className="row mb-4">
                    <div className="col-12">
                        <Grid id="CLI050_grid_S" rowsToFetch={30} rowsToDisplay={15}
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