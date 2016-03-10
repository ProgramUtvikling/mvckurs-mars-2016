﻿using ImdbWeb.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImdbWeb.Areas.Admin.Models.MovieModels
{
    public class EditModel
    {
        //[Display(Name = "EAN kode")]
        //public string MovieId { get; set; }

        [Display(Name = "Tittel")]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "Originaltittel")]
        [MaxLength(100)]
        public string OriginalTitle { get; set; }

        [Display(Name = "Beskrivelse")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Produksjonsår")]
        [MaxLength(4)]
        public string ProductionYear { get; set; }

        [Display(Name = "Timer")]
        [Required]
        [Range(0, int.MaxValue / 60 - 1)]
        public int RunningLengthHours { get; set; }
        [Display(Name = "Minutter")]
        [Required]
        [Range(0, 59)]
        public int RunningLengthMinutes { get; set; }

        [Display(Name = "Sjanger")]
        [Required]
        public int GenreId { get; set; }
    }
}