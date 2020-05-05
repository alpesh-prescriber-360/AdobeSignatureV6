using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdobeSignatureV6
{
    [DataContract]
    public class UserAgreements
    {
        [DataMember(EmitDefaultValue = false)]
        public PageInfo page { get; set; }//Pagination information for navigating through the response

        [DataMember(EmitDefaultValue = false)]
        public List<UserAgreement> userAgreementList { get; set; }
    }

    [DataContract]
    public class PageInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string nextCursor { get; set; }
    }

    [DataContract]
    public class UserAgreement
    {        
        [DataMember(EmitDefaultValue = false)]
        public string displayDate { get; set; }

        [DataMember]
        public List<DisplayParticipantSetInfo> displayParticipantSetInfos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool esign { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string groupId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool hidden { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string latestVersionId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string parentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string type { get; set; }
        
                 
    }

    [DataContract]
    public class DisplayParticipantSetInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<DisplayParticipantInfo> displayUserSetMemberInfos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string displayUserSetName { get; set; }

        
    }

    [DataContract]
    public class DisplayParticipantInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string company { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string fullName { get; set; }

    }

}
