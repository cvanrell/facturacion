using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;


namespace WIS.Billing.EntitiesCore
{
    public abstract class GridColumnConfigPartial
    {
        public virtual string DS_COLUMNA { get; set; }
        public virtual string NM_DATAFIELD { get; set; }
        public virtual short? VL_POSICION_FIJADO { get; set; }
        public virtual string VL_TYPE { get; set; }
        public virtual string VL_ALINEACION { get; set; }
        public virtual short? NU_ORDEN_VISUAL { get; set; }
        public virtual decimal? VL_WIDTH { get; set; }
        public virtual string FL_VISIBLE { get; set; }
        
        public FixPosition GetFixPosition()
        {
            return (FixPosition)(this.VL_POSICION_FIJADO ?? 1);
        }
        public ColumnType GetColumnType()
        {
            if (this.VL_TYPE == null)
                throw new Exception("Tipo de columna no definido");

            switch (this.VL_TYPE)
            {
                case "ST":
                    return ColumnType.Text;
                case "CK":
                    return ColumnType.Checkbox;
                case "DT":
                    return ColumnType.DateTime;
                case "DO":
                    return ColumnType.Date;
                case "BA":
                    return ColumnType.Button;
                case "BL":
                    return ColumnType.ItemList;
                case "PG":
                    return ColumnType.Progress;
            }

            return ColumnType.Text;
        }
        public GridTextAlign GetTextAlign()
        {
            if (this.VL_ALINEACION == null)
                return GridTextAlign.Left;

            switch (this.VL_ALINEACION)
            {
                case "D":
                    return GridTextAlign.Right;
                case "I":
                    return GridTextAlign.Left;
                case "C":
                    return GridTextAlign.Center;
            }

            return GridTextAlign.Left;
        }
        public short GetOrder()
        {
            return this.NU_ORDEN_VISUAL ?? 0;
        }
        public decimal GetWidth()
        {
            return this.VL_WIDTH ?? 100;
        }

        public void SetFixPosition(FixPosition position)
        {
            this.VL_POSICION_FIJADO = (short)position;
        }
        public void SetColumnType(ColumnType type)
        {
            string dbType = "ST";

            switch (type)
            {
                case ColumnType.Text:
                    dbType = "ST";
                    break;
                case ColumnType.DateTime:
                    dbType = "DT";
                    break;
                case ColumnType.Date:
                    dbType = "DO";
                    break;
                case ColumnType.Checkbox:
                    dbType = "CK";
                    break;
                case ColumnType.Button:
                    dbType = "BA";
                    break;
                case ColumnType.ItemList:
                    dbType = "BL";
                    break;
                case ColumnType.Progress:
                    dbType = "PG";
                    break;
            }

            this.VL_TYPE = dbType;
        }
        public void SetTextAlign(GridTextAlign textAlign)
        {
            string dbAlign = null;

            switch (textAlign)
            {
                case GridTextAlign.Right:
                    dbAlign = "D";
                    break;
                case GridTextAlign.Left:
                    dbAlign = "I";
                    break;
                case GridTextAlign.Center:
                    dbAlign = "C";
                    break;
            }

            this.VL_ALINEACION = dbAlign;
        }
        public void SetVisible(bool visible)
        {
            this.FL_VISIBLE = visible ? "S" : "N";
        }

        public bool IsVisible()
        {
            return FL_VISIBLE == "S";
        }
    }
}
