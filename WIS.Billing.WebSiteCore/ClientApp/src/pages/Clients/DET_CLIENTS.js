import React from 'react';
import { Grid } from '../../components/GridComponents/Grid';
import { Page } from '../../components/Page';
import { Form, Field, StatusMessage } from '../../components/FormComponents/Form';
import { useTranslation } from 'react-i18next';

export default function DET_CLIENTS(props) {
    const { t } = useTranslation();

    const fieldSetStyle = { border: "1px solid #ddd", margin: "10px", width: "100%" };

    const initialValues = {
        //QT_DISPONIBLE: "",
        //QT_RESERVADA: "",
        //QT_MERCADERIA: "",
    };
    const validationSchema = {
    };

    const onAfterApplyFilter = (context, form, query, nexus) => {
        console.log("----- onAfterApplyFilter ")
        //nexus.getForm("DOC020_form_1").reset();
    };
    const onAfterInitialize = (context, form, query, nexus) => {
        console.log("----- onAfterApplyFilter ")
        ne//xus.getForm("DOC020_form_1").reset();
    };

    return (

        <Page
            icon="fas fa-file"
            title="Edicion de clientes"//{t("DOC020_Sec0_pageTitle_Titulo")}
            {...props}
        >
            <Form
                id="DET_CLIENTS_form_1"
                initialValues={initialValues}
                validationSchema={validationSchema}>

                <div className="row col-12">
                    <fieldset className="row" style={fieldSetStyle}>
                        <legend>{t("DOC020_frm1_lbl_Totales")}</legend>
                        <div className="row col-12">
                            <div className="col-4">
                                <div className="form-group">
                                    <label htmlFor="Description">{t("Descripcion")}</label>
                                    <Field name="Description" readOnly />
                                    <StatusMessage for="Description" />
                                </div>
                            </div>
                            <div className="col-4">
                                <div className="form-group">
                                    <label htmlFor="Address">{t("Direccion")}</label>
                                    <Field name="Address" readOnly />
                                    <StatusMessage for="Address" />
                                </div>
                            </div>
                            
                            <div className="col-4">
                                <div className="form-group">
                                    <label htmlFor="RUT">{t("RUT")}</label>
                                    <Field name="RUT" readOnly />
                                    <StatusMessage for="RUT" />
                                </div>
                            </div>
                        </div>                        
                    </fieldset>
                </div>
            </Form>
            <div className="row mb-4">
                <div className="col-12">
                    <Grid id="DET_CLIENTS_grid_T" rowsToFetch={30} rowsToDisplay={15}
                        onAfterApplyFilter={onAfterApplyFilter}
                        onAfterInitialize={onAfterInitialize}
                        //enableExcelExport
                    />
                </div>
            </div>

            <div className="row mb-4">
                <div className="col-12">
                    <Grid id="DET_CLIENTS_grid_S" rowsToFetch={30} rowsToDisplay={15}
                        onAfterApplyFilter={onAfterApplyFilter}
                        onAfterInitialize={onAfterInitialize}
                    //enableExcelExport
                    />
                </div>
            </div>
        </Page>
    );
}