namespace Interview.Domain.Model
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

        [ForeignKey("Employee")]
        public string EmployeeID { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string HRNote { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InterviewDate { get; set; }

        [ForeignKey("Candidate")]
        public string CandidateID { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
