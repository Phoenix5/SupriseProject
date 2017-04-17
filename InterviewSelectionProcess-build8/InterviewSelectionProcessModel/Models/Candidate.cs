namespace Interview.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Candidate")]
    public partial class Candidate
    {


        public class MyDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime d = Convert.ToDateTime(value);
                return d <= DateTime.Now.AddYears(-18);

            }
        }




        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Candidate()
        {
            CandidateStatus = new HashSet<CandidateStatu>();
            InterviewSchedules = new HashSet<InterviewSchedule>();
        }

        public int CandidateID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MyDate(ErrorMessage = "Invalid date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Range(0, 20)]
        public int Experience { get; set; }

        public int TechnologiesWorkedOnID { get; set; }

        public int PostID { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public virtual Post Post { get; set; }

        public virtual Technology Technology { get; set; }

        public string ResumePath { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateStatu> CandidateStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; }
    }
}
