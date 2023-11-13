using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeebackApplication.Models
{
    public class NewModel
    {
        public int Id { get; set; }
        public List<Field> Fields { get; set; }
        public List<Product> ProductList { get; set; }
    }
}