import React, { useState } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
//import Button from 'react-bootstrap/Button'
import { Form, Field, FieldSelect, FieldSelectAsync, FieldDate, SubmitButton, Button, StatusMessage } from '../../components/FormComponents/Form';
import * as Yup from 'yup';

export default function BIL020(props) {
    //const { t } = useTranslation();
    const [isFormEnabled, setFormEnabled] = useState(false);
    const [isShowForm, setShowForm] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [rows, setRows] = useState({});

    //const secondarySubmitStyle = { width: "300px !important" };
    const fieldSetStyle = { border: "1px solid #ddd", margin: "10px", width: "100%" };

    const initialValues = {
        BillNumber: "",
        BillDate: "",
        
    };

    const validationSchema = {
        BillNumber: Yup.string().required(),
        BillDate: Yup.string().required(),        
    };



    const onAfterInitialize = () => {
        setFormEnabled(true);
    };
    const onAfterSubmit = (context, form, query, nexus) => {
        setShowForm(false);
        nexus.getForm("BIL020_form_1").reset();
        nexus.getGrid("BIL020_grid_1").refresh();
    };

    const onBeforeButtonAction = (context, form, query, nexus) => {        

        query.abortServerCall = true;

        if (query.buttonId === "showFormButton") {
            nexus.getForm("BIL020_form_1").reset();
            setShowForm(true);
            setEditing(true);
        }
        else if (query.buttonId === "hideFormButton") {
            setShowForm(false);
            nexus.getForm("BIL020_form_1").reset();
        }
    }

    const onAfterButtonAction = (data, nexus) => {
        setShowForm(false);
        nexus.getForm("BIL020_form_1").reset();
        nexus.getGrid("BIL020_grid_1").refresh();

    };    



    const formShowButtonClassName = isShowForm ? "hidden" : "";
    const formClassName = isShowForm ? "row mb-5" : "row mb-5 hidden";
    const confirmButtonClassName = isEditing ? "btn btn-warning" : "btn btn-warning hidden";

    return (
        <Page {...props}
            title="Facturas">

            <Form
                id="BIL020_form_1"
                initialValues={initialValues}
                validationSchema={validationSchema}
                onAfterSubmit={onAfterSubmit}
                onBeforeButtonAction={onBeforeButtonAction}
            >

                <div className={formShowButtonClassName} style={{ textAlign: "center" }}>
                    <Button id="showFormButton" value={("Agregar nueva factura")} className="btn btn-success" style={{ margin: "15px" }} isLoading={isFormEnabled} />
                </div>

                <div className={formClassName}>
                    <div className="col">
                        <div className="col-12">
                            <fieldset className="col-12" >

                                <div className="col-12">


                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="SupportName">{("Soporte")}</label>
                                            <Field name="SupportName" readOnly />
                                            <StatusMessage for="SupportName" />
                                        </div>
                                    </div>

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="BillNumber">{("Número de factura")}</label>
                                            <Field name="BillNumber" />
                                            <StatusMessage for="BillNumber" />
                                        </div>
                                    </div>
                                                                        

                                    <div className="col-4">
                                        <div className="form-group">
                                            <label htmlFor="BillDate">{("Fecha de la factura")}</label>
                                            <FieldDate name="BillDate" />
                                            <StatusMessage for="BillDate" />
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
                    <Grid id="BIL020_grid_1" rowsToFetch={30} rowsToDisplay={30} enableSelection />
                </div>
            </div>
        </Page>
    );



}