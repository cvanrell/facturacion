import React, { useState } from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
//import Button from 'react-bootstrap/Button'
import * as Yup from 'yup';

export default function BIL010(props) {
    const [isFormEnabled, setFormEnabled] = useState(false);
    const [isShowForm, setShowForm] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [rows, setRows] = useState({});

    const fieldSetStyle = { border: "1px solid #ddd", margin: "10px", width: "100%" };

    const initialValues = {
        
    };

    const validationSchema = {
        
    };



    const onAfterInitialize = () => {
        setFormEnabled(true);
    };
    const onAfterSubmit = (context, form, query, nexus) => {
        setShowForm(false);
        nexus.getForm("BIL010_form_1").reset();
        nexus.getGrid("BIL010_grid_1").refresh();
    };

    const onBeforeButtonAction = (context, form, query, nexus) => {

        query.abortServerCall = true;

        if (query.buttonId === "showFormButton") {
            nexus.getForm("BIL010_form_1").reset();
            setShowForm(true);
            setEditing(true);
        }
        else if (query.buttonId === "hideFormButton") {
            setShowForm(false);
            nexus.getForm("BIL010_form_1").reset();
        }
    }

    const onAfterButtonAction = (data, nexus) => {
        setShowForm(false);
        nexus.getForm("BIL010_form_1").reset();
        nexus.getGrid("BIL010_grid_1").refresh();

    };
    
    



    const formShowButtonClassName = isShowForm ? "hidden" : "";
    const formClassName = isShowForm ? "row mb-5" : "row mb-5 hidden";
    const confirmButtonClassName = isEditing ? "btn btn-warning" : "btn btn-warning hidden";

    return (
        <Page {...props}
            title="Mantenimientos a facturar">
            
            <div className="row mb-4">
                <div className="col">
                    <Grid id="BIL010_grid_1" rowsToFetch={30} rowsToDisplay={30} enableSelection />
                </div>
            </div>
        </Page>
    );



}