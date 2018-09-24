namespace stackoverflow
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vote")]
    public partial class Vote
    {
        public int Id { get; set; }

        public byte VoteValue { get; set; }

        public DateTime CreateDate { get; set; }

        public int VoterId { get; set; }

        public int? AnswerId { get; set; }

        public int? QuestionId { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual Question Question { get; set; }

        public virtual User User { get; set; }
    }
}
