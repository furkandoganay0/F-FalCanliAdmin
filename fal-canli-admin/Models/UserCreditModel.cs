using System;

namespace fal_canli_admin.Models
{
    public class UserCreditModel : BaseModel
    {
        public double amount { get; set; }
        public string userid { get; set; }
        public string type { get; set; }
        public string fortuneTellerId { get; set; }
        public UserModel userModel { get; set; }
        public FortuneTellerModel fortuneTellerModel { get; set; }
    }

    public class UserCreditModelForAddUpdate
    {
        public double amount { get; set; }
        public string userid { get; set; }
        public string type { get; set; }
        public string fortuneTellerId { get; set; }
        public UserModel userModel { get; set; }
        public FortuneTellerModel fortuneTellerModel { get; set; }
    }

    public class UserCreditModelWithoutFortuneTellerForAddUpdate
    {
        public double amount { get; set; }
        public string userid { get; set; }
        public string type { get; set; }
        public UserModel userModel { get; set; }
        public FortuneTellerModel fortuneTellerModel { get; set; }
    }
}