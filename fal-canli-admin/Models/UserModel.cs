using System;

namespace fal_canli_admin.Models
{
    public class UserModel:BaseModel
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public bool isAdmin { get; set; }
        public bool isDeported { get; set; }
        public string zodiacSign { get; set; }
    }

    public class UserModelForAddUpdate
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public bool isAdmin { get; set; }
        public bool isDeported { get; set; }
    }

    public class UserModelWithoutPasswordForAddUpdate
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public bool isAdmin { get; set; }
        public bool isDeported { get; set; }
    }
}

