﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        public string Title { get; set; }

        [StringLength(1024, MinimumLength = 1, ErrorMessage = "The {0} must between {2} and {1} character.")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 4)]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Post Content")]
        public string PostContent { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        public bool Published { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        public DateTime PostedOn { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        public DateTime Modified { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "View Count")]
        public int ViewCount { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Rate Count")]
        public int RateCount { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Total Rate")]
        public int TotalRate { get; set; }

        [NotMapped]
        public decimal Rate { get => TotalRate / RateCount; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
