namespace DeltaX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MoviesActor
    {
        [Key]
        public int MoviesActorsId { get; set; }

        public int ActorId { get; set; }

        public int MovieId { get; set; }

        public bool Active { get; set; }

        public DateTime? CreatedDt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedDt { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
