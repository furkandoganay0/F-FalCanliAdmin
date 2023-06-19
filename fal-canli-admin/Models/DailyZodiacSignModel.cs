using System;

namespace fal_canli_admin.Models
{
    public class DailyZodiacSignModel : BaseModel
    {
        public string type { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string explanation { get; set; }
    }

    public class DailyZodiacSignModelForAddUpdate
    {
        public string type { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string explanation { get; set; }
    }
}