using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MachineTestApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
