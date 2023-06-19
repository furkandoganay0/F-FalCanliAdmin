using System;

namespace fal_canli_admin.Models
{
    public class FortuneTellerModel : BaseModel
    {
        public string userid { get; set; }
        public string nickname { get; set; }
        public string about { get; set; }
        public string fortuneTellerStatus { get; set; }
        public string channelId { get; set; }
        public bool hasTarot { get; set; }
        public double tarotServiceFee { get; set; }
        public bool hasCoffee { get; set; }
        public double coffeeServiceFee { get; set; }
        public bool hasBirthChart { get; set; }
        public double birthChartServiceFee { get; set; }
        public bool hasAstrology { get; set; }
        public double astrologyServiceFee { get; set; }
        public UserModel userModel { get; set; }
    }

    public class FortuneTellerModelForAddUpdate
    {
        public string userid { get; set; }
        public string nickname { get; set; }
        public string about { get; set; }
        public string fortuneTellerStatus { get; set; }
        public string channelId { get; set; }
        public bool hasTarot { get; set; }
        public double tarotServiceFee { get; set; }
        public bool hasCoffee { get; set; }
        public double coffeeServiceFee { get; set; }
        public bool hasBirthChart { get; set; }
        public double birthChartServiceFee { get; set; }
        public bool hasAstrology { get; set; }
        public double astrologyServiceFee { get; set; }
        public UserModel userModel { get; set; }
    }
}

