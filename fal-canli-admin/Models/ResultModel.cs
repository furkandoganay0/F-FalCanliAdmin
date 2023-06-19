using System;

namespace fal_canli_admin.Models
{
    public class ResultModel
    {
        public int StatusCode { get; set; }
        public string Timestamp { get; set; }
        public object Result { get; set; }
    }
}
