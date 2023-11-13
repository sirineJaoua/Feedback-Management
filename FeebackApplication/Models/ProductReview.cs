using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeebackApplication.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        [ForeignKey("Feedback")]
        public int FeedbackId { get; set; }
        public string Value { get; set; }
        public virtual Feedback Feedback { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}