namespace InterviewSelectionProcess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InterviewSchedule")]
    public partial class InterviewSchedule
    {
        [Key]
        public int InterviewID { get; set; }

        public int EmployeeID { get; set; }

        public int? HRID { get; set; }

        [StringLength(500)]
        public string Note { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InterviewDate { get; set; }

        public int CandidateID { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
