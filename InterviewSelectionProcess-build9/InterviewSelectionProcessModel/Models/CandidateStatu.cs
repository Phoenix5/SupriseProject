namespace Interview.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CandidateStatu
    {
        [Key]
        public int CandidateStatusID { get; set; }

        public int CandidateID { get; set; }

        public int InterviewStatus { get; set; }

        public int SelectionStatus { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual InterviewStatu InterviewStatu { get; set; }

        public virtual SelectionStatu SelectionStatu { get; set; }
    }
}
