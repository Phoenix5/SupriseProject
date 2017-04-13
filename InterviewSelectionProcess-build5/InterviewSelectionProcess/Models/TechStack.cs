namespace InterviewSelectionProcess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TechStack")]
    public partial class TechStack
    {
        [Key]
        public int TeckStackID { get; set; }

        [Required]
        [StringLength(50)]
        public string CandidateID { get; set; }

        public int TechID { get; set; }
    }
}
