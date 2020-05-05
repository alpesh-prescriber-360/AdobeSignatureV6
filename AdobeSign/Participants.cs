using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdobeSignatureV6
{
    

    [DataContract]
    public class ParticipantSetInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<ParticipantInfo> memberInfos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int order { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string role { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string label { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string privateMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> visiblePages { get; set; }

        
    }


    [DataContract]
    public class ParticipantInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ParticipantSecurityOption securityOption { get; set; }


    }

    [DataContract]
    public class ParticipantSecurityOption
    {
        [DataMember(EmitDefaultValue = false)]
        public string authenticationMethod { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string password { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PhoneInfo phoneInfo { get; set; }


    }

    [DataContract]
    public class PhoneInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string countryCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CountryIsoCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string phone { get; set; }
    }

}
