namespace Interview.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            InterviewSchedule = new HashSet<InterviewSchedule>();
        }

        [Key, ForeignKey("ApplicationUser")]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

       
      //  public int Count { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Role { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InterviewSchedule> InterviewSchedule { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
