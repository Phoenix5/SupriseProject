namespace Interview.Domain.Model
{
    using Model;
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
            CandidateStatuses = new HashSet<CandidateStatus>();
            InterviewSchedules = new HashSet<InterviewSchedule>();
        }

        [Key, ForeignKey("ApplicationUser")]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MyDate(ErrorMessage = "Your age is less then 18")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Range(0, 20)]
        public int Experience { get; set; }

        [ForeignKey("Technology")]
        public int TechnologyID { get; set; }

        [ForeignKey("Post")]
        public int PostID { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string MobileNumber { get; set; }

        public virtual Post Post { get; set; }

        public virtual Technology Technology { get; set; }

        public string ResumePath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateStatus> CandidateStatuses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; }

        public virtual ApplicationUser ApplicationUser { get;set;}
    }
}
