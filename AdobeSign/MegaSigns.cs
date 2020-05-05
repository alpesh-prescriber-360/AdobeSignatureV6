using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdobeSignatureV6
{

    #region MagaSign Creation

    [DataContract]
    public class MegaSignInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public ChildAgreementsInfo childAgreementsInfo { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public List<FileInfo> fileInfos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        public SignatureTypeEnum signatureType { get; set; }

        [DataMember(Name = "signatureType")]
        public string signatureTypeString
        {
            get { return signatureType.ToString(); }
            set
            {
                SignatureTypeEnum g;
                this.signatureType = Enum.TryParse(value, true, out g) ? g : SignatureTypeEnum.ESIGN;
            }
        }

        [DataMember(EmitDefaultValue = false)]
        public string state { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<MagaSignCcInfo> ccs { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string createdDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string expirationTime { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ExternalId externalId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int firstReminderDelay { get; set; } //Integer which specifies the delay in hours before sending the first reminder

        [DataMember(EmitDefaultValue = false)]
        public string groupId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool isDocumentRetentionApplied { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string locale { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int numChildren { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PostSignOption postSignOption { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string reminderFrequency { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public MagaSignSecurityOption securityOption { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string senderEmail { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; }        

        [DataMember(EmitDefaultValue = false)]
        public List<VaultingInfo> vaultingInfo { get; set; }
    }

    [DataContract]
    public class MagaSignSecurityOption
    {
        [DataMember(EmitDefaultValue = false)]
        public string externalAuthenticationMethod { get; set; }  //['NONE' or 'WEB_IDENTITY' or 'KBA' or 'PASSWORD']

        [DataMember(EmitDefaultValue = false)]
        public string internalAuthenticationMethod { get; set; }  //['NONE' or 'WEB_IDENTITY' or 'KBA' or 'PASSWORD']

        [DataMember(EmitDefaultValue = false)]
        public string externalPassword { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string internalPassword { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string openPassword { get; set; }

    }

    [DataContract]
    public class MagaSignCcInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; }
    }

    [DataContract]
    public class ChildAgreementsInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public MegaSignChildAgreementsFileInfo fileInfo { get; set; }
    }

    [DataContract]
    public class MegaSignChildAgreementsFileInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string fileType { get; set; }  //['CSV']

        [DataMember(EmitDefaultValue = false)]
        public string transientDocumentId { get; set; }  

        [DataMember(EmitDefaultValue = false)]
        public string childAgreementsInfoFileId { get; set; }  

    }


    /* Response */

    [DataContract]
    public class MegaSignCreationResponse 
    {
        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }
    }

    #endregion MegaSign Creation

    #region MegaSigns List

    [DataContract]
    public class MegaSigns
    {
        [DataMember(EmitDefaultValue = false)]
        public List<MegaSign> megaSignList { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PageInfo page { get; set; }

    }

    [DataContract]
    public class MegaSign
    {
        [DataMember(EmitDefaultValue = false)]
        public string displayDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Boolean esign { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string groupId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool hidden { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; }
    }

    #endregion MegaSigns List

    #region MegaSign Child Agreements

    [DataContract]
    public class MegaSignChildAgreements
    {
        [DataMember(EmitDefaultValue = false)]
        public List<MegaSignChildAgreement> megaSignChildAgreementList { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PageInfo page { get; set; }
    }

    [DataContract]
    public class MegaSignChildAgreement
    {        
        [DataMember(EmitDefaultValue = false)]
        public string displayDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Boolean esign { get; set; }

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

    #endregion #region MegaSign Child Agreements

    #region MegaSign MembersInfo

    [DataContract]
    public class MegaSignMembersInfo {
        [DataMember(EmitDefaultValue = false)]
        public List<CCParticipantInfo> ccsInfo{ get; set; }//Information of CC participants of the megaSign.,

        [DataMember(EmitDefaultValue = false)]
        public List<DetailedParticipantSetInfo> participantSets { get; set; } // Information about the participant Sets.,

        [DataMember(EmitDefaultValue = false)]
        public List<SenderInfo> senderInfo { get; set; } //Information of the sender of the megaSign.,

        [DataMember(EmitDefaultValue = false)]
        public List<ShareParticipantInfo> sharesInfo { get; set; } //Information of the participants with whom the megaSign has been shared.
    }

    #endregion MegaSign MembersInfo

    #region MegaSign Document Iamge Urls

    [DataContract]
    public class BasicDocumentsImageUrlsInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<DocumentsImageUrlsInfo> documentsImageUrlsInfo { get; set; }

    }

    # endregion MegaSign Document Iamge Urls


    #region MagaSign ReminderInfo

    [DataContract]
    public class MegaSignRemindersResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public List<ReminderInfo> reminderInfoList { get; set; } //A list of one or more reminders created on the MegaSign parent specified by the unique identifier megasignId by the user invoking the API.
    }

    #endregion MagaSign ReminderInfo

    #region MagaSign State Updation

    [DataContract]
    public class MegaSignStateInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string state { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AgreementCancellationInfo megaSignCancellationInfo { get; set; } //A list of one or more reminders created on the MegaSign parent specified by the unique identifier megasignId by the user invoking the API.
    }

    #endregion MagaSign State Updation

}
