namespace Interview.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CandidateStatus
    {
        [Key]
        public int CandidateStatusID { get; set; }

        [ForeignKey("Candidate")]
        public string CandidateID { get; set; }

        [ForeignKey("InterviewStatus")]
        public int InterviewStatusID { get; set; }

        [ForeignKey("SelectionStatus")]
        public int SelectionStatusID { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual InterviewStatus InterviewStatus { get; set; }

        public virtual SelectionStatus SelectionStatus { get; set; }
    }
}
