using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.BusinessLogicCore.Enums
{
    public class Estado
    {
        public const string PENDIENTE_APROBACION = "Pendiente de aprobacion interna";
        public const string PENDIENTE_FACTURACION = "Pendiente de facturación";
        public const string CANCELADA = "Cancelada";
        public const string RECHAZADA = "Rechazada";
        public const string FACTURADA = "Facturada";
        public const string PAGADA = "Pagada";

    }
}
