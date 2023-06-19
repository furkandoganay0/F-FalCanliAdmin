using System;

namespace fal_canli_admin.Models
{
    public class BaseModel
    {
        public string _id { get; set; }
        public string _createdate { get; set; }
        public string _createuser { get; set; }
        public bool _isdeleted { get; set; }
        public int _statusid { get; set; }
        public string _updatedate { get; set; }
        public string _updateuser { get; set; }
    }
}
