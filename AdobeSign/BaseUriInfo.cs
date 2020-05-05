using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdobeSignatureV6
{
    [DataContract]
    public class BaseUriInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string api_access_point { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string web_access_point { get; set; }
    }
}
