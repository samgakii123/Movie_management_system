namespace DeltaX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Movy
    {
        [Key]
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int ReleaseYear { get; set; }
        public List<string> ActorId { get; set; }
        [Required]
        [StringLength(50)]
        public string Plot { get; set; }

        [StringLength(500)]
        public string Poster { get; set; }

        public int ProducerId { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedDt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? UpdatedDt { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
