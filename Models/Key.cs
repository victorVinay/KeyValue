using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeyValue.Models
{
    public class Key
    {
        [Key]
        public string key { get; set; }
        public string value { get; set; }
    }
}
