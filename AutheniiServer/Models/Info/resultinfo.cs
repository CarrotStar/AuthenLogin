using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenLoginDeploy.Entities
{
    public class resultinfo
    {
        public result Result { get; set; }

    }

    public class result
    {
        public bool Status { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Profileimg { get; set; }

    }
}
