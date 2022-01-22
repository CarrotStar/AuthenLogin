using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenLoginDeploy.Entities
{
    public class TblUsersInfo
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public string Profileimg { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? EditCount { get; set; }
    }
}
