using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImdbWeb.Models.RatingModels
{
    public class VoteResultModel
    {
        public string MovieId { get; set; }
        public int YourVote { get; set; }
        public double AverageVote { get; set; }
        public int VoteCount { get; set; }
    }
}