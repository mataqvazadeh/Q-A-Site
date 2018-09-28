namespace QASite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Answer()
        {
            Comments = new HashSet<Comment>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Body { get; set; }

        public int AuthorId { get; set; }

        public int QuestionId { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsAccepted { get; set; }

        public virtual Question Question { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
