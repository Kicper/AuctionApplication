using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }

        public Grade RatingText { get; set; }

        public enum Grade
        {
            [Display(Name = "Very low")]
            VeryLow = -3,
            [Display(Name = "Low")]
            Low = -2,
            [Display(Name = "Slightly low")]
            SlightlyLow = -1,
            [Display(Name = "Medium")]
            Medium = 0,
            [Display(Name = "Slightly high")]
            SlightlyHigh = 1,
            [Display(Name = "High")]
            High = 2,
            [Display(Name = "Very high")]
            VeryHigh = 3
        }
    }
}
