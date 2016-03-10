using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImdbWeb.Areas.Admin.Models.MovieModels
{
    public class IndexModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        [UIHint("Duration")]
        public int RunningLength { get; set; }
    }
}