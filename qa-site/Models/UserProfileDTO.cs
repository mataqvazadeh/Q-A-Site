using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QASite.Models
{
    public class UserProfileDTO
    {
        public UserProfileDTO()
        {
            Answers = new HashSet<Answer>();
            Comments = new HashSet<Comment>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime? LastLogin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}