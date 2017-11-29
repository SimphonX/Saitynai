using System;
using System.Collections.Generic;

namespace RateAPI.Model
{
    public partial class Comments
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public int Commenter { get; set; }
        public int Game { get; set; }

        public Games GameNavigation { get; set; }
    }
}
