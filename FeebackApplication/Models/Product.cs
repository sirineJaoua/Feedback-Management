using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeebackApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public Boolean Deleted { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductReview> Details { get; set; }
    }
}