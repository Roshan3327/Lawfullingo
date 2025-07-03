using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Lawfullingo
{
    public class Users
    {
        public int id { get; set; }
        public string user_name { get; set; } = null;
        public string user_email { get; set; }
        public string password { get; set; }
        public int mobile { get; set; }
        public string profile_image { get; set; } = null;
        public string gender { get; set; }
        public DateTime user_dob { get; set; }
        public bool status { get; set; }
        public string education { get; set; }
        public bool isVerified { get; set; }
        public DateTime deleted_at { get; set; }
        public DateTime created_at { get; set; }

        public ICollection<Purchase> purchases { get; set; } = new List<Purchase>();


    }
}
