using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RateAPI.Model
{
    public partial class Games
    {
        public Games()
        {
            Comments = new HashSet<Comments>();
            Followings = new HashSet<Followings>();
        }

        public int Id { get; set; }

        public ICollection<Comments> Comments { get; set; }
        public ICollection<Followings> Followings { get; set; }
    }
    public class GamesID
    {
        public int Id { get; set; }
    }
}
