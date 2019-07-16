namespace WIS.Billing.EntitiesCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("T_GRID_DEFAULT_CONFIG")]
    public partial class T_GRID_DEFAULT_CONFIG
    {        
        [Column(Order = 0)]
        [StringLength(30)]
        public string CD_APLICACION { get; set; }
     
        [Column(Order = 1)]
        [StringLength(30)]
        public string CD_BLOQUE { get; set; }

        [Column(Order = 2)]
        [StringLength(30)]
        public override string NM_DATAFIELD { get; set; }

        [StringLength(100)]
        public override string DS_COLUMNA { get; set; }

        public override short? NU_ORDEN_VISUAL { get; set; }

        [Required]
        [StringLength(1)]
        public override string FL_VISIBLE { get; set; }

        public int? RESOURCEID { get; set; }

        [Required]
        [StringLength(1)]
        public override string VL_ALINEACION { get; set; }

        [StringLength(30)]
        public string DS_DATA_FORMAT_STRING { get; set; }

        public override decimal? VL_WIDTH { get; set; }

        [Required]
        [StringLength(2)]
        public override string VL_TYPE { get; set; }

        [StringLength(500)]
        public string VL_LINK { get; set; }

        public override short? VL_POSICION_FIJADO { get; set; }
    }
}
