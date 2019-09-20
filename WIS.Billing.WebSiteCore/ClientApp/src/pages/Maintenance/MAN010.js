import React, { useState } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
//import Button from 'react-bootstrap/Button'
import { Form, Field, FieldSelect, FieldSelectAsync, FieldDate, SubmitButton, Button, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export default function MAN010(props) {    
    const [isFormEnabled, setFormEnabled] = useState(false);
    const [isShowForm, setShowForm] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [rows, setRows] = useState({});
    
    const fieldSetStyle = { border: "1px solid #ddd", margin: "10px", width: "100%" };

    const initialValues = {        
        Client: "",        
        Description: "",
        SupportRate: ""
    };

    const validationSchema = {
        Client: Yup.string().required(),        
        Description: Yup.string().required(),
        SupportRate: Yup.string().required()
    };



    const onAfterInitialize = () => {
        setFormEnabled(true);
    };
    const onAfterSubmit = (context, form, query, nexus) => {
        setShowForm(false);
        nexus.getForm("MAN010_form_1").reset();
        nexus.getGrid("MAN010_grid_1").refresh();
    };

    const onBeforeButtonAction = (context, form, query, nexus) => {        

        query.abortServerCall = true;

        if (query.buttonId === "showFormButton") {
            nexus.getForm("MAN010_form_1").reset();
            setShowForm(true);
            setEditing(true);
        }
        else if (query.buttonId === "hideFormButton") {
            setShowForm(false);
            nexus.getForm("MAN010_form_1").reset();
        }
    }

    const onAfterButtonAction = (data, nexus) => {
        setShowForm(false);
        nexus.getForm("MAN010_form_1").reset();
        nexus.getGrid("MAN010_grid_1").refresh();

    };

    
    const onSelectChange = (contexto, form, query, nexus) => {
        if (query.FieldId === "Cliente") {

        }
    };



    const formShowButtonClassName = isShowForm ? "hidden" : "";
    const formClassName = isShowForm ? "row mb-5" : "row mb-5 hidden";
    const confirmButtonClassName = isEditing ? "btn btn-warning" : "btn btn-warning hidden";

    return (
        <Page {...props}
            title="Mantenimiento">

            <Form
                id="MAN010_form_1"
                initialValues={initialValues}
                validationSchema={validationSchema}
                onAfterSubmit={onAfterSubmit}
                onBeforeButtonAction={onBeforeButtonAction}
                //onAfterValidateField = {onSelectChange}
            >

                <div className={formShowButtonClassName} style={{ textAlign: "center" }}> 
                    <Button id="showFormButton" value={("Agregar nuevo mantenimiento")} className="btn btn-success" style={{ margin: "15px" }} isLoading={isFormEnabled} />
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
                                            <label htmlFor="SupportRate">{("Tarifa de soporte")}</label>
                                            <FieldSelect name="SupportRate" />
                                            <StatusMessage for="SupportRate" />
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                        </div>

                        <div className="row">
                            <div className="col">
                                <SubmitButton value={("Agregar mantenimiento")} />
                                &nbsp;
                                <Button id="hideFormButton" value={("Cancelar")} className="btn btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
            </Form>

            <div className="row mb-4">
                <div className="col">
                    <Grid id="MAN010_grid" rowsToFetch={30} rowsToDisplay={30} enableSelection />
                </div>
            </div>
        </Page>
    );



}