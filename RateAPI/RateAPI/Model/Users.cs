using System;
using System.Collections.Generic;

namespace RateAPI.Model
{
    public partial class Users
    {
        public Users()
        {
            Followings = new HashSet<Followings>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string UserPass { get; set; }

        public ICollection<Followings> Followings { get; set; }
    }
}
