namespace QASite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Body { get; set; }

        public DateTime CreateDate { get; set; }

        public int AuthorId { get; set; }

        public int? AnswerId { get; set; }

        public int? QuestionId { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual Question Question { get; set; }

        public virtual User User { get; set; }
    }
}
