
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeebackApplication.Models
{
    
    public class FeedbackDetail
    {
        public int Id { get; set; }
        [ForeignKey("Feedback")]
        public int FeedbackId { get; set; }
        public virtual Feedback Feedback { get; set; }

        public string Value { get; set; }


        [ForeignKey("Field")]
        public int FieldId { get; set; }
        public virtual Field Field { get; set; }

        

        
    }
}