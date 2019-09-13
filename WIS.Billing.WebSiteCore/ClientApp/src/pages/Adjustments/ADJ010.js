import React, { useState } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
//import Button from 'react-bootstrap/Button'
import { Form, Field, FieldSelect, FieldSelectAsync, FieldDate, SubmitButton, Button, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export default function ADJ010(props) {
    //const { t } = useTranslation();
    const [isFormEnabled, setFormEnabled] = useState(false);
    const [isShowForm, setShowForm] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [rows, setRows] = useState({});

    //const secondarySubmitStyle = { width: "300px !important" };
    const fieldSetStyle = { border: "1px solid #ddd", margin: "10px", width: "100%" };

    const initialValues = {
        //name: "Exito",
        //lastname: "",
        //password: "Pass",
        //type: 2
        Year: "",
        Mes: "",
        Month:"",
        IPCValue: "",
        DateIPC: "",
    };

    const validationSchema = {
        Year: Yup.string().required(),
        //Mes: Yup.string().required(),
        Month: "",
        IPCValue: Yup.string().required(),
        DateIPC: Yup.string().nullable(),
    };



    const onAfterInitialize = () => {
        setFormEnabled(true);
    };
    const onAfterSubmit = (context, form, query, nexus) => {
        setShowForm(false);
        nexus.getForm("ADJ010_form_1").reset();
        nexus.getGrid("ADJ010_grid_1").refresh();
    };

    const onBeforeButtonAction = (context, form, query, nexus) => {
        //context.abortServerCall = true;

        ////nexus.redirect("/stock/STO110");

        //nexus.getForm("form_1").reset();

        query.abortServerCall = true;

        if (query.buttonId === "showFormButton") {
            nexus.getForm("ADJ010_form_1").reset();
            setShowForm(true);
            setEditing(true);
        }
        else if (query.buttonId === "hideFormButton") {
            setShowForm(false);
            nexus.getForm("ADJ010_form_1").reset();
        }
    }

    const onAfterButtonAction = (data, nexus) => {
        setShowForm(false);
        nexus.getForm("ADJ010_form_1").reset();
        nexus.getGrid("ADJ010_grid_1").refresh();

    };

    const executeAdjustment = () => {

        console.log("Ejecutar ajuste");
    };

    const handleClick = () => {

        console.log("llama");
        const request = {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                prueba: "prueba ajustes",
                Application: "ADJ010",
                ComponentId: "ADJ010_form_1",
                //Data: "data"
                Data: JSON.stringify({
                    form: {
                        id: "ADJ010_form_1"
                    }
                })
            })
        };

        fetch("api/Form/ExecuteAdjustments", request).then((response) => response.json()).
            then((response) => console.log(response));
        console.log("termina");
    };



    const formShowButtonClassName = isShowForm ? "hidden" : "";
    const formClassName = isShowForm ? "row mb-5" : "row mb-5 hidden";
    const confirmButtonClassName = isEditing ? "btn btn-warning" : "btn btn-warning hidden";

    return (
        <Page {...props}
            title="Ajustes de IPC">

            <Form
                id="ADJ010_form_1"
                initialValues={initialValues}
                validationSchema={validationSchema}
                onAfterSubmit={onAfterSubmit}
                onBeforeButtonAction={onBeforeButtonAction}
            >

                <div className={formShowButtonClassName} style={{ textAlign: "center" }}>
                    <Button id="showFormButton" value={("Agregar nuevo ajuste")} className="btn btn-success" style={{ margin: "15px" }} isLoading={isFormEnabled} />
                </div>

                <div style={{ textAlign: "right" }}>
                    <Button id="adjustment" value={("Realizar ajuste de IPC")} className="btn btn-primary" style={{ margin: "15px" }} isLoading={isFormEnabled} onClick={handleClick} />
                </div>

                <div className={formClassName}>
                    <div className="col">
                        <div className="col-12">
                            <fieldset className="col-12" >

                                <div className="col-12">
                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="Year">{("Año")}</label>
                                            <Field name="Year" />
                                            <StatusMessage for="Year" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="Month">{("Mes")}</label>
                                            <FieldSelect name="Month" />
                                            <StatusMessage for="Month" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="IPCValue">{("Valor IPC")}</label>
                                            <Field name="IPCValue" />
                                            <StatusMessage for="IPCValue" />
                                        </div>
                                    </div>                                    

                                    <div className="col-4">
                                    <div className="form-group">
                                        <label htmlFor="DateIPC">{("Fecha")}</label>
                                        <FieldDate name="DateIPC" />
                                        <StatusMessage for="DateIPC" />
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                        </div>

                        <div className="row">
                            <div className="col">
                                <SubmitButton value={("Agregar Ajuste")} />
                                &nbsp;
                                <Button id="hideFormButton" value={("Cancelar")} className="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
            </Form>

            <div className="row mb-4">
                <div className="col">
                    <Grid id="ADJ010_grid_1" rowsToFetch={30} rowsToDisplay={30} enableSelection />
                </div>
            </div>
        </Page>
    );



}