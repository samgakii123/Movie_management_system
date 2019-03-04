using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaX.Models
{
    public class MovieList
    {
        public int MovieId { get; set; }
        public int ProducerId { get; set; }
        public List<int> ActorIdList { get; set; }
        public List<string> ActorNameList { get; set; }
        public string ProducerName { get; set; }
        public string MovieName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool Active { get; set; }
        public string ActorId { get; set; }
        public string ActorName { get; set; }
        public int ReleaseYear { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }
    }
}