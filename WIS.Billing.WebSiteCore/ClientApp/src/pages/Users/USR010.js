import React, { useState } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
//import Button from 'react-bootstrap/Button'
import { Form, Field, FieldSelect, FieldSelectAsync, FieldDate, SubmitButton, Button, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export default function USR010(props) {
    //const { t } = useTranslation();
    const [isFormEnabled, setFormEnabled] = useState(false);
    const [isShowForm, setShowForm] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [rows, setRows] = useState({});

    //const secondarySubmitStyle = { width: "300px !important" };
    const fieldSetStyle = { border: "1px solid #ddd", margin: "10px", width: "100%" };

    const initialValues = {        
        Name: "",
        LastName: "",
        UserName: "",
        Password: ""
    };

    const validationSchema = {
        Name: Yup.string().required(),
        UserName: Yup.string().required(),
        
        Password: Yup.string().required(),
    };



    const onAfterInitialize = () => {
        setFormEnabled(true);
    };
    const onAfterSubmit = (context, form, query, nexus) => {
        setShowForm(false);
        nexus.getForm("USR010_form_1").reset();
        nexus.getGrid("USR010_grid_1").refresh();
    };

    const onBeforeButtonAction = (context, form, query, nexus) => {
        //context.abortServerCall = true;

        ////nexus.redirect("/stock/STO110");

        //nexus.getForm("form_1").reset();

        query.abortServerCall = true;

        if (query.buttonId === "showFormButton") {
            nexus.getForm("USR010_form_1").reset();
            setShowForm(true);
            setEditing(true);
        }
        else if (query.buttonId === "hideFormButton") {
            setShowForm(false);
            nexus.getForm("USR010_form_1").reset();
        }
    }

    const onAfterButtonAction = (data, nexus) => {
        setShowForm(false);
        nexus.getForm("USR010_form_1").reset();
        nexus.getGrid("USR010_grid_1").refresh();

    };



    const formShowButtonClassName = isShowForm ? "hidden" : "";
    const formClassName = isShowForm ? "row mb-5" : "row mb-5 hidden";
    const confirmButtonClassName = isEditing ? "btn btn-warning" : "btn btn-warning hidden";

    return (
        <Page {...props}
            title="Ajustes de IPC">

            <Form
                id="USR010_form_1"
                initialValues={initialValues}
                validationSchema={validationSchema}
                onAfterSubmit={onAfterSubmit}
                onBeforeButtonAction={onBeforeButtonAction}
            >

                <div className={formShowButtonClassName} style={{ textAlign: "center" }}>
                    <Button id="showFormButton" value={("Agregar nuevo ajuste")} className="btn btn-success" style={{ margin: "15px" }} isLoading={isFormEnabled} />
                </div>

                <div className={formClassName}>
                    <div className="col">
                        <div className="col-12">
                            <fieldset className="col-12" >

                                <div className="col-12">
                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="Name">{("Nombre")}</label>
                                            <Field name="Name" />
                                            <StatusMessage for="Name" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="LastName">{("LastName")}</label>
                                            <FieldSelect name="LastName" />
                                            <StatusMessage for="LastName" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="UserName">{("Nombre de usuario")}</label>
                                            <Field name="UserName" />
                                            <StatusMessage for="UserName" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="Password">{("Contraseña")}</label>
                                            <Field name="Password" />
                                            <StatusMessage for="Password" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="Email">{("Email")}</label>
                                            <Field name="Email" />
                                            <StatusMessage for="Email" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="TpUser">{("Tipo de usuario")}</label>
                                            <FieldSelect name="TpUser" />
                                            <StatusMessage for="TpUser" />
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
                    <Grid id="USR010_grid_1" rowsToFetch={30} rowsToDisplay={30} enableSelection />
                </div>
            </div>
        </Page>
    );



}