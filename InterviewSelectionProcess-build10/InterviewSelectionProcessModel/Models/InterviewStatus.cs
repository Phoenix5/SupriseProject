namespace Interview.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InterviewStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InterviewStatus()
        {
            CandidateStatus = new HashSet<CandidateStatus>();
        }

        [Key]
        public int InterviewStatusID { get; set; }

        [Required]
        [StringLength(50)]
        public string InterviewStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateStatus> CandidateStatus { get; set; }
    }
}
