namespace WIS.Billing.EntitiesCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    
    public partial class T_GRID_FILTER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_GRID_FILTER()
        {
            T_GRID_FILTER_DET = new HashSet<T_GRID_FILTER_DET>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CD_FILTRO { get; set; }

        [Required]
        [StringLength(20)]
        public string NM_FILTRO { get; set; }

        [StringLength(200)]
        public string DS_FILTRO { get; set; }

        [Required]
        [StringLength(40)]
        public string CD_BLOQUE { get; set; }

        [Required]
        [StringLength(40)]
        public string CD_APLICACION { get; set; }

        public DateTime? DT_ADDROW { get; set; }

        public int USERID { get; set; }

        [StringLength(1)]
        public string FL_GLOBAL { get; set; }

        [StringLength(1)]
        public string FL_INICIAL { get; set; }

        [StringLength(2000)]
        public string VL_FILTRO_AVANZADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_GRID_FILTER_DET> T_GRID_FILTER_DET { get; set; }
    }
}
