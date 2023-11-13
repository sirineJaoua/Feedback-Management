using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeebackApplication.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Statue Status { get; set; }
        public DateTime Creationdate { get; set; }
        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ICollection<FeedbackDetail> Details { get; set; }
        public virtual ICollection<ProductReview> Reviews { get; set; }




    }
}