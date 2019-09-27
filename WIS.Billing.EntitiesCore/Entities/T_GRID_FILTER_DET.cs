namespace WIS.Billing.EntitiesCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    
    public partial class T_GRID_FILTER_DET
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CD_FILTRO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string CD_COLUMNA { get; set; }

        [StringLength(2000)]
        public string VL_FILTRO { get; set; }

        public short? VL_ORDEN { get; set; }

        public int? NU_ORDEN_EJECUCION { get; set; }

        public virtual T_GRID_FILTER T_GRID_FILTER { get; set; }
    }
}
