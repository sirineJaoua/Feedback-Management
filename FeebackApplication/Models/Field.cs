using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeebackApplication.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
        public string ScoreRange { get; set; }
        public Product Product { get; set; }
        public virtual ICollection<FeedbackDetail> Details { get; set; }
    }
}