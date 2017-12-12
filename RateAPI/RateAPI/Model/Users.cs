using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RateAPI.Model
{
    public partial class Users
    {

        [Key]
        public string Username { get; set; }
        public string UserPass { get; set; }
        
    }
}
