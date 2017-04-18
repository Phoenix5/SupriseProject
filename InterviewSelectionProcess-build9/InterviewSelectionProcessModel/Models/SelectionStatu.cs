namespace Interview.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelectionStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SelectionStatu()
        {
            CandidateStatus = new HashSet<CandidateStatu>();
        }

        [Key]
        public int SelectionStatusID { get; set; }

        [Required]
        [StringLength(50)]
        public string SelectionStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateStatu> CandidateStatus { get; set; }
    }
}
