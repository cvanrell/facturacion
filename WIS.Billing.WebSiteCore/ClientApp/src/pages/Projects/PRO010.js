import React, { useState } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
//import Button from 'react-bootstrap/Button'
import { Form, Field, FieldSelect, FieldSelectAsync, FieldDate, SubmitButton, Button, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export default function PRO010(props) {
    //const { t } = useTranslation();
    const [isFormEnabled, setFormEnabled] = useState(false);
    const [isShowForm, setShowForm] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [rows, setRows] = useState({});

    //const secondarySubmitStyle = { width: "300px !important" };
    const fieldSetStyle = { border: "1px solid #ddd", margin: "10px", width: "100%" };

    const initialValues = {        
        Client: "",
        Currency: "",
        Description: "",
        InitialDate: "",
        Amount: "",
        Total: ""
    };

    const validationSchema = {
        Client: Yup.string().required(),
        Currency: Yup.string().required(),
        Description: Yup.string().required(),
        InitialDate: Yup.string().nullable(),
        Amount: Yup.number().required(),
        Total: Yup.number().required()
    };



    const onAfterInitialize = () => {
        setFormEnabled(true);
    };
    const onAfterSubmit = (context, form, query, nexus) => {
        setShowForm(false);
        nexus.getForm("PRO010_form_1").reset();
        nexus.getGrid("PRO010_grid_1").refresh();
    };

    const onBeforeButtonAction = (context, form, query, nexus) => {
        //context.abortServerCall = true;

        ////nexus.redirect("/stock/STO110");

        //nexus.getForm("form_1").reset();

        query.abortServerCall = true;

        if (query.buttonId === "showFormButton") {
            nexus.getForm("PRO010_form_1").reset();
            setShowForm(true);
            setEditing(true);
        }
        else if (query.buttonId === "hideFormButton") {
            setShowForm(false);
            nexus.getForm("PRO010_form_1").reset();
        }
    }

    const onAfterButtonAction = (data, nexus) => {
        setShowForm(false);
        nexus.getForm("PRO010_form_1").reset();
        nexus.getGrid("PRO010_grid_1").refresh();

    };

    const handleClick = () => {
        const request = {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                prueba: "prueba ajustes"
            })
        };

        fetch("api/Form/ExecuteAdjustment", request).then((response) => response.json()).
            then((response) => console.log(response));
    };



    const formShowButtonClassName = isShowForm ? "hidden" : "";
    const formClassName = isShowForm ? "row mb-5" : "row mb-5 hidden";
    const confirmButtonClassName = isEditing ? "btn btn-warning" : "btn btn-warning hidden";

    return (        
        <Page {...props}
        title="Proyectos">

            <Form
                id="PRO010_form_1"
                initialValues={initialValues}
                validationSchema={validationSchema}
                onAfterSubmit={onAfterSubmit}
                onBeforeButtonAction={onBeforeButtonAction}
            >

                <div className={formShowButtonClassName} style={{ textAlign: "center" }}>
                    <Button id="showFormButton" value={("Agregar nuevo proyecto")} className="btn btn-success" style={{ margin: "15px" }} isLoading={isFormEnabled} />
                </div>

                <div className={formClassName}>
                    <div className="col">
                        <div className="col-12">
                            <fieldset className="col-12" >

                                <div className="col-12">
                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="Description">{("Descripción")}</label>
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
                                                                                                           

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="InitialDate">{("Fecha de Inicio")}</label>
                                            <FieldDate name="InitialDate" />
                                            <StatusMessage for="InitialDate" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="Total">{("Total")}</label>
                                            <Field name="Total" />
                                            <StatusMessage for="Total" />
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                        </div>

                        <div className="row">
                            <div className="col">
                                <SubmitButton value={("Agregar proyecto")} />
                                &nbsp;
                                <Button id="hideFormButton" value={("Cancelar")} className="btn btn-danger" />
                            </div>
                        </div>
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