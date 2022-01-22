using System;
using System.Collections.Generic;

#nullable disable

namespace AuthenLoginDeploy.Entities
{
    public partial class TblHistoryUser
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public string Profileimg { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
