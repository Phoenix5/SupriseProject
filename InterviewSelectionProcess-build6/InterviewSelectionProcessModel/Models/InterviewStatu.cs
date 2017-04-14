namespace InterviewSelectionProcess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InterviewStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InterviewStatu()
        {
            CandidateStatus = new HashSet<CandidateStatu>();
        }

        [Key]
        public int InterviewStatusID { get; set; }

        [Required]
        [StringLength(50)]
        public string InterviewStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateStatu> CandidateStatus { get; set; }
    }
}
