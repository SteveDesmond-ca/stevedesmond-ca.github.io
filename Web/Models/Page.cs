using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public sealed class Page
    {
        [Required]
        public int ID { get; set; }

        [Required, MaxLength(10)]
        public string Category { get; set; }

        [Required, MaxLength(50)]
        public string URL { get; set; }

        [NotMapped]
        public string AbsoluteURL => "/" + Category.ToLower() + "/" + URL;

        [NotMapped]
        public string FullURL => Settings.Domain + AbsoluteURL;

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        [Required, Range(typeof(DateTime), "1/1/2015", "1/1/2020")]
        public DateTime Timestamp { get; set; }

        [Required, Column("Live")]
        public bool Aggregate { get; set; }

        [Required]
        public bool Partial { get; set; }

        [Required]
        public bool FullWidth { get; set; }

        [Required]
        public bool Crawl { get; set; }

        public string TwitterStatus { get; set; }
    }
}