using System;
using System.Collections.Generic;

namespace RateAPI.Model
{
    public partial class Followings
    {
        public int Id { get; set; }
        public int Follower { get; set; }
        public int Game { get; set; }

        public Users FollowerNavigation { get; set; }
        public Games GameNavigation { get; set; }
    }
}
