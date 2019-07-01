using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.ControlDocumental;
using WIS.BusinessLogicCore.ControlDocumental.Enums;
using WIS.Billing.DataAccessCore.Database;

namespace WIS.BusinessLogicCore.DataModel.Mappers
{
    public class DocumentoMapper : Mapper
    {
        private readonly DocumentoFactory _factory;

        public DocumentoMapper()
        {
            this._factory = new DocumentoFactory();
        }

        //TODO: Ver si es correcto mantener este metodo
        [Obsolete]
        public IDocumento MapToDocumento(T_DOCUMENTO documentoEntity)
        {
            if (documentoEntity == null)
                return null;

            TipoDocumento tipoDocumento = this.GetTipo(documentoEntity.TP_DOCUMENTO);

            IDocumento documento = this._factory.Create(tipoDocumento);

            this.SetPropertiesDocumento(tipoDocumento, documento, documentoEntity);

            return documento;
        }
        public IDocumentoIngreso MapToIngreso(T_DOCUMENTO documentoEntity)
        {
            if (documentoEntity == null)
                return null;

            TipoDocumento tipoDocumento = this.GetTipo(documentoEntity.TP_DOCUMENTO);

            IDocumentoIngreso documento = this._factory.CreateIngreso(tipoDocumento);

            this.SetPropertiesDocumento(tipoDocumento, documento, documentoEntity);

            documento.Estado = this.GetEstadoIngreso(documentoEntity.ID_ESTADO);

            return documento;
        }
        public IDocumentoEgreso MapToEgreso(T_DOCUMENTO documentoEntity)
        {
            if (documentoEntity == null)
                return null;

            TipoDocumento tipoDocumento = this.GetTipo(documentoEntity.TP_DOCUMENTO);

            IDocumentoEgreso documento = this._factory.CreateEgreso(tipoDocumento);

            this.SetPropertiesDocumento(tipoDocumento, documento, documentoEntity);

            documento.Estado = this.GetEstadoEgreso(documentoEntity.ID_ESTADO);

            return documento;
        }
        public IDocumentoActa MapToActa(T_DOCUMENTO documentoEntity)
        {
            TipoDocumento tipoDocumento = this.GetTipo(documentoEntity.TP_DOCUMENTO);

            IDocumentoActa documento = this._factory.CreateActa(tipoDocumento);

            this.SetPropertiesDocumento(tipoDocumento, documento, documentoEntity);

            documento.Estado = this.GetEstadoActa(documentoEntity.ID_ESTADO);

            return documento;
        }

        [Obsolete]
        public T_DOCUMENTO MapFromDocumento(IDocumento documento)
        {
            return this.CreateEntityDocumento(documento);
        }
        public T_DOCUMENTO MapFromIngreso(IDocumentoIngreso documento)
        {
            T_DOCUMENTO documentoEntity = this.CreateEntityDocumento(documento);

            documentoEntity.ID_ESTADO = this.GetEstadoIngresoId(documento.Estado);

            return documentoEntity;
        }
        public T_DOCUMENTO MapFromEgreso(IDocumentoEgreso documento)
        {
            T_DOCUMENTO documentoEntity = this.CreateEntityDocumento(documento);

            documentoEntity.ID_ESTADO = this.GetEstadoEgresoId(documento.Estado);

            return documentoEntity;
        }
        public T_DOCUMENTO MapFromActa(IDocumentoActa documento)
        {
            T_DOCUMENTO documentoEntity = this.CreateEntityDocumento(documento);

            documentoEntity.ID_ESTADO = this.GetEstadoActaId(documento.Estado);

            return documentoEntity;
        }


        public DocumentoLinea MapToDocumentoLinea(T_DET_DOCUMENTO detalleDocumento)
        {
            var linea = new DocumentoLinea
            {
                CantidadDesafectada = detalleDocumento.QT_DESAFECTADA,
                CantidadDescargada = detalleDocumento.QT_DESCARGADA,
                CantidadIngresada = detalleDocumento.QT_INGRESADA,
                CantidadReservada = detalleDocumento.QT_RESERVADA,
                CIF = detalleDocumento.VL_CIF,
                DescripcionProducto = detalleDocumento.DS_PRODUTO_INGRESO,
                Disponible = this.MapStringToBoolean(detalleDocumento.ID_DISPONIBLE),
                Empresa = detalleDocumento.CD_EMPRESA,
                Faixa = detalleDocumento.CD_FAIXA,
                FechaAlta = detalleDocumento.DT_ADDROW,
                FechaDisponible = detalleDocumento.DT_DISPONIBLE,
                FechaModificacion = detalleDocumento.DT_UPDROW,
                Identificador = detalleDocumento.NU_IDENTIFICADOR,
                Producto = detalleDocumento.CD_PRODUTO,
                Situacion = detalleDocumento.CD_SITUACAO,
                ValorMercaderia = detalleDocumento.VL_MERCADERIA
            };

            return linea;
        }
        public T_DET_DOCUMENTO MapFromDocumentoLinea(string nroDocumento, TipoDocumento tipoDocumento, DocumentoLinea linea)
        {
            var detalleDocumento = new T_DET_DOCUMENTO
            {
                CD_EMPRESA = linea.Empresa,
                CD_FAIXA = linea.Faixa,
                CD_PRODUTO = linea.Producto,
                CD_SITUACAO = linea.Situacion,
                DS_PRODUTO_INGRESO = linea.DescripcionProducto,
                DT_ADDROW = linea.FechaAlta,
                DT_DISPONIBLE = linea.FechaDisponible,
                DT_UPDROW = linea.FechaModificacion,
                ID_DISPONIBLE = this.MapBoolToString(linea.Disponible),
                NU_DOCUMENTO = nroDocumento,
                NU_IDENTIFICADOR = linea.Identificador,
                QT_DESAFECTADA = linea.CantidadDesafectada,
                QT_DESCARGADA = linea.CantidadDescargada,
                QT_INGRESADA = linea.CantidadIngresada,
                QT_RESERVADA = linea.CantidadReservada,
                TP_DOCUMENTO = this.GetTipo(tipoDocumento),
                VL_CIF = linea.CIF,
                VL_MERCADERIA = linea.ValorMercaderia
            };

            return detalleDocumento;
        }

        private T_DOCUMENTO CreateEntityDocumento(IDocumento documento)
        {
            var documentoEntity = new T_DOCUMENTO
            {
                NU_AGENDA = documento.Agenda,
                CD_CAMION = documento.Camion,
                CD_CLIENTE = documento.Cliente,
                CD_DEPOSITO = documento.Deposito,
                CD_DESPACHANTE = documento.Despachante,
                CD_EMPRESA = documento.Empresa,
                CD_FORNECEDOR = documento.Proveedor,
                CD_FUNCIONARIO = documento.Usuario,
                CD_MONEDA = documento.Moneda,
                CD_SITUACAO = documento.Situacion,
                CD_TRANSPORTISTA = documento.Transportista,
                CD_UNIDAD_MEDIDA_BULTO = documento.UnidadMedida,
                CD_VIA = documento.Via,
                DS_ANEXO1 = documento.Anexo1,
                DS_ANEXO2 = documento.Anexo2,
                DS_DOCUMENTO = documento.Descripcion,
                DT_ADDROW = documento.FechaAlta,
                DT_DECLARADO = documento.FechaDeclarado,
                DT_DTI = documento.FechaDTI,
                DT_DUA = documento.DocumentoAduana.Fecha,
                DT_ENVIADO = documento.FechaEnviado,
                DT_FACTURACION = documento.FechaFacturado,
                DT_FINALIZADO = documento.FechaFinalizado,
                DT_MOVILIZA_CONTENEDOR = documento.FechaMovilizacionContenedor,
                DT_PROGRAMADO = documento.FechaProgramado,
                DT_UPDROW = documento.FechaModificacion,
                ID_AGENDAR_AUTOMATICAMENTE = this.MapBoolToString(documento.AgendarAutomaticamente),
                ID_FICTICIO = this.MapBoolToString(documento.Ficticio),
                ID_GENERAR_AGENDA = this.MapBoolToString(documento.GeneraAgenda),
                ID_MANUAL = documento.IdManual,
                NU_CONOCIMIENTO = documento.Conocimiento,
                NU_CORRELATIVO = documento.NumeroCorrelativo,
                NU_CORRELATIVO_2 = documento.NumeroCorrelativo2,
                NU_DOCUMENTO = documento.Numero,
                NU_DOC_TRANSPORTE = documento.DocumentoTransporte,
                NU_DTI = documento.NumeroDTI,
                NU_DUA = documento.DocumentoAduana.Numero,
                NU_EXPORT = documento.NumeroExportacion,
                NU_FACTURA = documento.Factura,
                NU_IMPORT = documento.NumeroImportacion,
                QT_BULTO = documento.CantidadBulto,
                QT_CONTENEDOR20 = documento.CantidadContenedor20,
                QT_CONTENEDOR40 = documento.CantidadContenedor40,
                QT_PESO = documento.Peso,
                QT_VOLUMEN = documento.Volumen,
                TP_ALMACENAJE_Y_SEGURO = documento.TipoAlmacenajeYSeguro,
                TP_DOCUMENTO = this.GetTipo(documento.Tipo),
                TP_DUA = documento.DocumentoAduana.Tipo,
                VL_ARBITRAJE = documento.ValorArbitraje,
                VL_FLETE = documento.ValorFlete,
                VL_OTROS_GASTOS = documento.ValorOtrosGastos,
                VL_SEGURO = documento.ValorSeguro,
                VL_VALIDADO = this.MapBoolToString(documento.Validado)
            };

            foreach (var linea in documento.Lineas)
            {
                documentoEntity.T_DET_DOCUMENTO.Add(this.MapFromDocumentoLinea(documento.Numero, documento.Tipo, linea));
            }

            return documentoEntity;
        }
        private void SetPropertiesDocumento(TipoDocumento tipo, IDocumento documento, T_DOCUMENTO documentoEntity)
        {
            documento.Agenda = documentoEntity.NU_AGENDA;
            documento.AgendarAutomaticamente = this.MapStringToBoolean(documentoEntity.TP_DOCUMENTO);
            documento.Anexo1 = documentoEntity.DS_ANEXO1;
            documento.Anexo2 = documentoEntity.DS_ANEXO2;
            documento.Camion = documentoEntity.CD_CAMION;
            documento.CantidadBulto = documentoEntity.QT_BULTO;
            documento.CantidadContenedor20 = documentoEntity.QT_CONTENEDOR20;
            documento.CantidadContenedor40 = documentoEntity.QT_CONTENEDOR40;
            documento.Cliente = documentoEntity.CD_CLIENTE;
            documento.Configuracion = new DocumentoConfiguracion();
            documento.Conocimiento = documentoEntity.NU_CONOCIMIENTO;
            documento.Deposito = documentoEntity.CD_DEPOSITO;
            documento.Descripcion = documentoEntity.DS_DOCUMENTO;
            documento.Despachante = documentoEntity.CD_DESPACHANTE;
            documento.DocumentoAduana = new DocumentoAduana
            {
                Numero = documentoEntity.NU_DUA,
                Tipo = documentoEntity.TP_DUA,
                Fecha = documentoEntity.DT_DUA
            };
            documento.DocumentoTransporte = documentoEntity.NU_DOC_TRANSPORTE;
            documento.Empresa = documentoEntity.CD_EMPRESA;
            documento.Factura = documentoEntity.NU_FACTURA;
            documento.FechaAlta = documentoEntity.DT_ADDROW;
            documento.FechaDeclarado = documentoEntity.DT_DECLARADO;
            documento.FechaDTI = documentoEntity.DT_DTI;
            documento.FechaEnviado = documentoEntity.DT_ENVIADO;
            documento.FechaFacturado = documentoEntity.DT_FACTURACION;
            documento.FechaFinalizado = documentoEntity.DT_FINALIZADO;
            documento.FechaModificacion = documentoEntity.DT_UPDROW;
            documento.FechaMovilizacionContenedor = documentoEntity.DT_MOVILIZA_CONTENEDOR;
            documento.FechaProgramado = documentoEntity.DT_PROGRAMADO;
            documento.Ficticio = this.MapStringToBoolean(documentoEntity.ID_FICTICIO);
            documento.GeneraAgenda = this.MapStringToBoolean(documentoEntity.ID_GENERAR_AGENDA);
            documento.IdManual = documentoEntity.ID_MANUAL;
            documento.Moneda = documentoEntity.CD_MONEDA;
            documento.Numero = documentoEntity.NU_DOCUMENTO;
            documento.NumeroCorrelativo = documentoEntity.NU_CORRELATIVO;
            documento.NumeroCorrelativo2 = documentoEntity.NU_CORRELATIVO_2;
            documento.NumeroDTI = documentoEntity.NU_DTI;
            documento.NumeroExportacion = documentoEntity.NU_EXPORT;
            documento.NumeroImportacion = documentoEntity.NU_IMPORT;
            documento.Peso = documentoEntity.QT_PESO;
            documento.Proveedor = documentoEntity.CD_FORNECEDOR;
            documento.Situacion = documentoEntity.CD_SITUACAO;
            documento.Tipo = tipo;
            documento.TipoAlmacenajeYSeguro = documentoEntity.TP_ALMACENAJE_Y_SEGURO;
            documento.Transportista = documentoEntity.CD_TRANSPORTISTA;
            documento.UnidadMedida = documentoEntity.CD_UNIDAD_MEDIDA_BULTO;
            documento.Usuario = documentoEntity.CD_FUNCIONARIO;
            documento.Validado = this.MapStringToBoolean(documentoEntity.VL_VALIDADO);
            documento.ValorArbitraje = documentoEntity.VL_ARBITRAJE;
            documento.ValorFlete = documentoEntity.VL_FLETE;
            documento.ValorOtrosGastos = documentoEntity.VL_OTROS_GASTOS;
            documento.ValorSeguro = documentoEntity.VL_SEGURO;
            documento.Via = documentoEntity.CD_VIA;
            documento.Volumen = documentoEntity.QT_VOLUMEN;

            foreach (var linea in documentoEntity.T_DET_DOCUMENTO)
            {
                documento.Lineas.Add(this.MapToDocumentoLinea(linea));
            }
        }

        public TipoDocumento GetTipo(string tipo)
        {
            //TODO: cambiar a constantes o valores

            switch (tipo)
            {
                case "A":   return TipoDocumento.Acta;
                case "I":   return TipoDocumento.Ingreso;
                case "IN":  return TipoDocumento.IngresoNacional;
                case "IF":  return TipoDocumento.IngresoProduccion;
                case "E":   return TipoDocumento.Egreso;
                case "EF":  return TipoDocumento.EgresoProduccion;
            }

            return TipoDocumento.Unknown;
        }
        public EstadoDocumentoIngreso GetEstadoIngreso(string estado)
        {
            switch (estado)
            {
                case "EDI": return EstadoDocumentoIngreso.Edicion;
                case "ACE": return EstadoDocumentoIngreso.AceptadoAduana;
                case "ENV": return EstadoDocumentoIngreso.EnviadoAduana;
                case "FIN": return EstadoDocumentoIngreso.Finalizado;
                case "CAN": return EstadoDocumentoIngreso.Cancelado;
                case "VIN": return EstadoDocumentoIngreso.VerificacionIniciada;
                case "WAP": return EstadoDocumentoIngreso.PendienteCierreDeAgenda;
            }

            return EstadoDocumentoIngreso.Unknown;
        }
        public EstadoDocumentoEgreso GetEstadoEgreso(string estado)
        {
            switch (estado)
            {
                case "INI": return EstadoDocumentoEgreso.Inicializado;
                case "WLG": return EstadoDocumentoEgreso.LienasGeneradas;
                case "ENV": return EstadoDocumentoEgreso.EnviadoAduana;
                case "ACE": return EstadoDocumentoEgreso.AceptadoAduana;
                case "VIN": return EstadoDocumentoEgreso.VerificacionIniciada;
                case "FIN": return EstadoDocumentoEgreso.Finalizado;
                case "CAN": return EstadoDocumentoEgreso.Cancelado;
            }

            return EstadoDocumentoEgreso.Unknown;
        }
        public EstadosDocumentosActa GetEstadoActa(string estado)
        {
            switch (estado)
            {
                case "INI": return EstadosDocumentosActa.Inicializado;
                case "WLG": return EstadosDocumentosActa.LienasGeneradas;
                case "ENV": return EstadosDocumentosActa.EnviadoAduana;
                case "ACE": return EstadosDocumentosActa.AceptadoAduana;
                case "VIN": return EstadosDocumentosActa.VerificacionIniciada;
                case "FIN": return EstadosDocumentosActa.Finalizado;
                case "CAN": return EstadosDocumentosActa.Cancelado;
            }

            return EstadosDocumentosActa.Unknown;
        }

        public string GetTipo(TipoDocumento tipo)
        {
            switch (tipo)
            {
                case TipoDocumento.Acta: return "A";
                case TipoDocumento.Ingreso: return "I";
                case TipoDocumento.IngresoNacional: return "IN";
                case TipoDocumento.IngresoProduccion: return "IF";
                case TipoDocumento.Egreso: return "E";
                case TipoDocumento.EgresoProduccion: return "EF";
            }

            return null;
        }
        public string GetEstadoIngresoId(EstadoDocumentoIngreso estado)
        {
            switch (estado)
            {
                case EstadoDocumentoIngreso.Edicion: return "EDI";
                case EstadoDocumentoIngreso.AceptadoAduana: return "ACE";
                case EstadoDocumentoIngreso.EnviadoAduana: return "ENV";
                case EstadoDocumentoIngreso.Finalizado: return "FIN";
                case EstadoDocumentoIngreso.Cancelado: return "CAN";
                case EstadoDocumentoIngreso.VerificacionIniciada: return "VIN";
                case EstadoDocumentoIngreso.PendienteCierreDeAgenda: return "WAP";
            }

            return null;
        }
        public string GetEstadoEgresoId(EstadoDocumentoEgreso estado)
        {
            switch (estado)
            {
                case EstadoDocumentoEgreso.Inicializado: return "INI";
                case EstadoDocumentoEgreso.LienasGeneradas: return "WLG";
                case EstadoDocumentoEgreso.EnviadoAduana: return "ENV";
                case EstadoDocumentoEgreso.AceptadoAduana: return "ACE";
                case EstadoDocumentoEgreso.VerificacionIniciada: return "VIN";
                case EstadoDocumentoEgreso.Finalizado: return "FIN";
                case EstadoDocumentoEgreso.Cancelado: return "CAN";
            }

            return null;
        }
        public string GetEstadoActaId(EstadosDocumentosActa estado)
        {
            switch (estado)
            {
                case EstadosDocumentosActa.Inicializado: return "INI";
                case EstadosDocumentosActa.LienasGeneradas: return "WLG";
                case EstadosDocumentosActa.EnviadoAduana: return "ENV";
                case EstadosDocumentosActa.AceptadoAduana: return "ACE";
                case EstadosDocumentosActa.VerificacionIniciada: return "VIN";
                case EstadosDocumentosActa.Finalizado: return "FIN";
                case EstadosDocumentosActa.Cancelado: return "CAN";
            }

            return null;
        }

    }
}
