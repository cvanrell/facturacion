namespace WIS.Billing.DataAccessCore.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("T_GRID_DEFAULT_CONFIG")]
    public partial class T_GRID_DEFAULT_CONFIG
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string CD_APLICACION { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string CD_BLOQUE { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public  string NM_DATAFIELD { get; set; }

        [StringLength(100)]
        public  string DS_COLUMNA { get; set; }

        public  short? NU_ORDEN_VISUAL { get; set; }

        [Required]
        [StringLength(1)]
        public string FL_VISIBLE { get; set; }

        public int? RESOURCEID { get; set; }

        [Required]
        [StringLength(1)]
        public string VL_ALINEACION { get; set; }

        [StringLength(30)]
        public string DS_DATA_FORMAT_STRING { get; set; }

        public decimal? VL_WIDTH { get; set; }

        [Required]
        [StringLength(2)]
        public string VL_TYPE { get; set; }

        [StringLength(500)]
        public string VL_LINK { get; set; }

        public short? VL_POSICION_FIJADO { get; set; }
    }
}
