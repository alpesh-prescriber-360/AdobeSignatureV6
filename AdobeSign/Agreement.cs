using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdobeSignatureV6
{

    #region Agreement Creation

    [DataContract]
    public class AgreementInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<FileInfo> fileInfos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<ParticipantSetInfo> participantSetsInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string signatureType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string state { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CcInfo ccs { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string createdDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public OfflineDeviceInfo deviceInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Boolean documentVisibilityEnabled { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public EmailOption emailOption { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string expirationTime { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ExternalId externalId { get; set; }

        /// <summary>Integer which specifies the delay in hours before sending the first reminder</summary>
        [DataMember(EmitDefaultValue = false)]
        public int firstReminderDelay { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<FileInfo> formFieldLayerTemplates { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string groupId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Boolean hasFormFieldData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Boolean hasSignerIdentityReport { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Boolean isDocumentRetentionApplied { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string locale { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<MergeFieldInfo> mergeFieldInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string parentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PostSignOption postSignOption { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string reminderFrequency { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public SecurityOption securityOption { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string senderEmail { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public VaultingInfo vaultingInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string workflowId { get; set; }
        
    }

    [DataContract]
    public class FileInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public Document document { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string label { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string libraryDocumentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string transientDocumentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public URLFileInfo urlFileInfo { get; set; }

    }

    [DataContract]
    public class Document
    {
        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string label { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int numPages { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string mimeType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }


    }

    [DataContract]
    public class URLFileInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string mimeType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string url { get; set; }
    }

    [DataContract]
    public class CcInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public Document email { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string label { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> visiblePages { get; set; }

    }

    [DataContract]
    public class OfflineDeviceInfo
    {

        [DataMember(EmitDefaultValue = false)]
        public string applicationDescription { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string deviceDescription { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string deviceTime { get; set; }
    }

    [DataContract]
    public class EmailOption
    {
        [DataMember(EmitDefaultValue = false)]
        public SendOptions sendOptions { get; set; }
    }

    [DataContract]
    public class SendOptions
    {
        [DataMember(EmitDefaultValue = false)]
        public string completionEmails { get; set; } //['ALL' or 'NONE'] // Control notification mails for agreement completion events - COMPLETED, CANCELLED, EXPIRED and REJECTED
        
        [DataMember(EmitDefaultValue = false)]
        public string inFlightEmails { get; set; } //['ALL' or 'NONE']  // Control notification mails for agreement-in-process events - DELEGATED, REPLACED,

        [DataMember(EmitDefaultValue = false)]
        public string initEmails { get; set; } //['ALL' or 'NONE']  // Control notification mails for Agreement initiation events - ACTION_REQUESTED and CREATED
    }

    [DataContract]
    public class ExternalId
    {
        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }
    }

    [DataContract]
    public class MergeFieldInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string defaultValue { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string fieldName { get; set; }
    }

    [DataContract]
    public class PostSignOption
    {
        [DataMember(EmitDefaultValue = false)]
        public int redirectDelay { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string redirectUrl { get; set; }
    }

    [DataContract]
    public class SecurityOption
    {
        [DataMember(EmitDefaultValue = false)]
        public string openPassword { get; set; }
    }

    [DataContract]
    public class VaultingInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public bool enabled { get; set; }
    }

    /* START Response  */

    [DataContract]
    public class AgreementCreationResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }

    }

    #endregion Agreement Creation

    #region Share Agreement

    [DataContract]
    public class ShareCreationInfoList
    {
        [DataMember(EmitDefaultValue = false)]
        public List<ShareCreationInfo> shareCreationInfo { get; set; }
    }

    [DataContract]
    public class ShareCreationInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; }//The email address of the member with whom the agreement will be shared,

        [DataMember(EmitDefaultValue = false)]
        public string message { get; set; } //Optional message to the sharee
    }


    /*  Response  */

    [DataContract]
    public class ShareCreationResponseList
    {
        [DataMember(EmitDefaultValue = false)]
        public List<ShareCreationResponseList> shareCreationResponseList { get; set; }
    }

    [DataContract]
    public class ShareCreationResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; }//The email address of the member with whom the agreement will be shared,

        [DataMember(EmitDefaultValue = false)]
        public string message { get; set; } //Optional message to the sharee
    }

    #endregion Share Agreement

    #region Agreement Reminder

    [DataContract]
    public class ReminderInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<string> recipientParticipantIds { get; set; } // A list of one or more participant IDs that the reminder should be sent to

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; } // ['ACTIVE' or 'CANCELED' or 'COMPLETE'] 

        [DataMember(EmitDefaultValue = false)]
        public int firstReminderDelay { get; set; } //Integer which specifies the delay in hours before sending the first reminder

        [DataMember(EmitDefaultValue = false)]
        public string frequency { get; set; } // ['DAILY_UNTIL_SIGNED' or 'WEEKDAILY_UNTIL_SIGNED' or 'EVERY_OTHER_DAY_UNTIL_SIGNED' or 'EVERY_THIRD_DAY_UNTIL_SIGNED' or 'EVERY_FIFTH_DAY_UNTIL_SIGNED' or 'WEEKLY_UNTIL_SIGNED' or 'ONCE'] // The frequency at which reminder will be sent until the agreement is completed.

        [DataMember(EmitDefaultValue = false)]
        public string lastSentDate { get; set; }// The date when the reminder was last sent

        [DataMember(EmitDefaultValue = false)]
        public string nextSentDate { get; set; }// The date when the reminder is scheduled to be sent next

        [DataMember(EmitDefaultValue = false)]
        public string note { get; set; }// An optional message sent to the recipients, describing why their participation is required,

        [DataMember(EmitDefaultValue = false)]
        public string reminderId { get; set; } // An identifier of the reminder resource created on the server. If provided in POST or PUT, it will be ignored,

        [DataMember(EmitDefaultValue = false)]
        public string startReminderCounterFrom { get; set; } // ['AGREEMENT_AVAILABILITY' or 'REMINDER_CREATION']

        [DataMember(EmitDefaultValue = false)]
        public bool allUnsigned { get; set; } //If true, set a reminder on all participants (non-CCs and non-sharees) that still need to sign the MegaSign agreement
    }

    /* Response */

    [DataContract]
    public class ReminderCreationResult
    {
        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; } //An identifier of the reminder resource created on the server.If provided in POST or PUT, it will be ignored
    }

    #endregion Agreement Reminder

    #region Agreement combined Document Pages Info

    /* Response */

    /// <summary>Retrieves info of all pages of a combined PDF document for the documents associated with an agreement</summary>
    [DataContract]
    public class CombinedDocumentPagesInfo
    {
        /// <summary>List of basic information of all pages of the combined document of an Agreement</summary>
        [DataMember(EmitDefaultValue = false)]
        public List<DocumentPageInfo> documentPagesInfo { get; set; } //List of basic information of all pages of the combined document of an Agreement.
    }

    /// <summary>Document pages information</summary>
    [DataContract]
    public class DocumentPageInfo
    {
        /// <summary>Height of the page,</summary>
        [DataMember(EmitDefaultValue = false)]
        public decimal height { get; set; }

        /// <summary>Index of the page in combined document starting from 1</summary>
        [DataMember(EmitDefaultValue = false)]
        public int index { get; set; }

        /// <summary>Rotation angle of the page in clockwise direction in degree</summary>
        [DataMember(EmitDefaultValue = false)]
        public decimal rotation { get; set; }

        /// <summary>Width of page</summary>
        [DataMember(EmitDefaultValue = false)]
        public decimal width { get; set; }

    }

    #endregion  Agreement combined Document Pages Info

    #region Agreement Combined Document URL

    /* Response */

    [DataContract]
    public class DocumentUrl
    {
        [DataMember(EmitDefaultValue = false)]
        public string url { get; set; }//Height of the page,

    }

    #endregion  Agreement Combined Document URL

    #region Agreement Document URL

    /* Response */

    [DataContract]
    public class AgreementDocuments
    {
        [DataMember(EmitDefaultValue = false)]
        public List<Document> documents { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public SupportingDocument supportingDocuments { get; set; }//Height of the page,

    }

    [DataContract]
    public class SupportingDocument
    {
        [DataMember(EmitDefaultValue = false)]
        public string displayLabel { get; set; } //Display name of the document,

        [DataMember(EmitDefaultValue = false)]
        public string fieldName { get; set; } //The name of the supporting document field,

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; } //Id representing the document,

        [DataMember(EmitDefaultValue = false)]
        public string mimeType { get; set; } //Mime-type of the document,

    }

    #endregion  Agreement Document

    #region Agreement Document Image URLs

    /* Response */

    [DataContract]
    public class DocumentsImageUrlsInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<DocumentImageUrlsInfo> originalDocumentsImageUrlsInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<DocumentImageUrlsInfo> supportingDocumentsImageUrlsInfo { get; set; }
    }

    [DataContract]
    public class DocumentImageUrlsInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string documentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<DocumentImageUrls> documentImageUrlsList { get; set; }
    }

    [DataContract]
    public class AgreementDocumentImageUrlsInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string documentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<DocumentImageUrls> documentImageUrlsList { get; set; }//Height of the page,

    }

    [DataContract]
    public class DocumentImageUrls
    {
        [DataMember(EmitDefaultValue = false)]
        public List<PageImageUrl> imageURLs { get; set; } //A list of image url (one per page).,

        [DataMember(EmitDefaultValue = false)]
        public string imageSize { get; set; } //['FIXED_WIDTH_50px' or 'FIXED_WIDTH_250px' or 'FIXED_WIDTH_675px' or 'ZOOM_50_PERCENT' or 'ZOOM_75_PERCENT' or 'ZOOM_100_PERCENT' or 'ZOOM_125_PERCENT' or 'ZOOM_150_PERCENT' or 'ZOOM_200_PERCENT']: ImageSize corresponding to the imageUrl returned ,

        [DataMember(EmitDefaultValue = false)]
        public bool imagesAvailable { get; set; } //true if images for the associated image size is available, else false.

    }

    [DataContract]
    public class PageImageUrl
    {
        [DataMember(EmitDefaultValue = false)]
        public int pageNumber { get; set; } //Display name of the document,

        [DataMember(EmitDefaultValue = false)]
        public string url { get; set; } //The name of the supporting document field,        

    }

    #endregion  Agreement Document Image URLs

    #region Agreement Events

    [DataContract]
    public class EventList
    {
        [DataMember(EmitDefaultValue = false)]
        public List<Event> events { get; set; }
    }

    [DataContract]
    public class Event
    {
        [DataMember(EmitDefaultValue = false)]
        public string actingUserEmail { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string actingUserIpAddress { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string actingUserName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string date { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string description { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DeviceLocation deviceLocation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string devicePhoneNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DigitalSignatureInfo digitalSignatureInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string initiatingUserEmail { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string initiatingUserName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string participantEmail { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string participantId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string participantRole { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string synchronizationId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string type { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string versionId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string comment { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string vaultEventId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string vaultProviderName { get; set; }

    }

    [DataContract]
    public class DeviceLocation
    {
        [DataMember(EmitDefaultValue = false)]
        public float latitude { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public float longitude { get; set; }

    }

    [DataContract]
    public class DigitalSignatureInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string company { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }
    }

    #endregion Agreement Events

    #region Agreement FormFields

    [DataContract]
    public class AgreementFormFields
    {
        [DataMember(EmitDefaultValue = false)]
        public List<FormField> fields { get; set; } //List of the form fields in an agreement
    }

    #endregion  Agreement FormFields

    #region Agreement FormFieldsMergeInfo

    [DataContract]
    public class FormFieldMergeInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<MergeFieldInfo> fieldMergeInfos { get; set; } //List of the form fields in an agreement
    }

    #endregion  Agreement FormFields

    #region Agreement note

    [DataContract]
    public class Note
    {
        [DataMember(EmitDefaultValue = false)]
        public string note { get; set; } //List of the form fields in an agreement
    }

    #endregion  Agreement Note

    #region Agreement MembersInfo

    [DataContract]
    public class MembersInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<CCParticipantInfo> ccsInfo { get; set; } //Information of CC participants of the agreement.,

        [DataMember(EmitDefaultValue = false)]
        public List<DetailedParticipantSetInfo> nextParticipantSets { get; set; } // Information of next participant sets.,

        [DataMember(EmitDefaultValue = false)]
        public List<DetailedParticipantSetInfo> participantSets { get; set; } //Information about the participant Sets.,

        [DataMember(EmitDefaultValue = false)]
        public List<SenderInfo> senderInfo { get; set; } //Information of the sender of the agreement.,

        [DataMember(EmitDefaultValue = false)]
        public List<ShareParticipantInfo> sharesInfo { get; set; } //Information of the participants with whom the agreement has been shared.

    }

    [DataContract]
    public class CCParticipantInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string company { get; set; } //Company of the CC participant, if available.,

        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; } //Email of the CC participant of the agreement,

        [DataMember(EmitDefaultValue = false)]
        public bool hidden { get; set; } //True if the agreement is hidden for the user that is calling the API. Only returned if self is true.,

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; } //Name of the CC participant, if available.,

        [DataMember(EmitDefaultValue = false)]
        public string participantId { get; set; } //The unique identifier of the CC participant of the agreement.,

        [DataMember(EmitDefaultValue = false)]
        public bool self { get; set; } //True if the CC participant is the same user that is calling the API.

    }

    [DataContract]
    public class DetailedParticipantSetInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<DetailedParticipantInfo> memberInfos { get; set; } //Array of ParticipantInfo objects, containing participant-specific data (e.g. email). All participants in the array belong to the same set,

        [DataMember(EmitDefaultValue = false)]
        public int order { get; set; } //Index indicating sequential signing group (specified for hybrid routing). This cannot be changed as part of the PUT call.,

        [DataMember(EmitDefaultValue = false)]
        public string role { get; set; } //['SIGNER' or 'SENDER' or 'APPROVER' or 'ACCEPTOR' or 'CERTIFIED_RECIPIENT' or 'FORM_FILLER' or 'DELEGATE_TO_SIGNER' or 'DELEGATE_TO_APPROVER' or 'DELEGATE_TO_ACCEPTOR' or 'DELEGATE_TO_CERTIFIED_RECIPIENT' or 'DELEGATE_TO_FORM_FILLER' or 'SHARE']: Role assumed by all participants in the set (signer, approver etc.). This cannot be changed as part of the PUT call.,

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; } //The unique identifier of the participant set. This cannot be changed as part of the PUT call.,

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; } //Name of ParticipantSet (it can be empty, but needs not to be unique in a single agreement). Maximum no of characters in participant set name is restricted to 255. This cannot be changed as part of the PUT call.,

        [DataMember(EmitDefaultValue = false)]
        public string privateMessage { get; set; } //Participant set's private message - all participants in the set will receive the same message. This cannot be changed as part of the PUT call.,

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; } //['CANCELLED' or 'COMPLETED' or 'EXPIRED' or 'NOT_YET_VISIBLE' or 'WAITING_FOR_OTHERS' or 'WAITING_FOR_MY_APPROVAL' or 'WAITING_FOR_AUTHORING' or

    }

    [DataContract]
    public class DetailedParticipantInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; } //Email of the participant. In case of modifying a participant set (PUT) this is a required field. In case of GET, this is the required field and will always be returned unless it is a fax workflow (legacy agreements) that were created using fax as input,

        [DataMember(EmitDefaultValue = false)]
        public ParticipantSecurityOption securityOption { get; set; } //Security options that apply to the participant.,

        [DataMember(EmitDefaultValue = false)]
        public string company { get; set; } //The company of the participant, if available. This cannot be changed as part of the PUT call.,
        
        [DataMember(EmitDefaultValue = false)]
        public bool hidden { get; set; } //True if the agreement is hidden for the user that is calling the API. Only returned if self is true. Ignored (not required) if modifying a participant (PUT).,

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; } //The unique identifier of the participant. This will be returned as part of Get call but is not mandatory to be passed as part of PUT call for agreements/{id}/members/participantSets/{id}.,

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; } //The name of the participant, if available. This cannot be changed as part of the PUT call.,

        [DataMember(EmitDefaultValue = false)]
        public string privateMessage { get; set; } //The private message of the participant, if available. This cannot be changed as part of the PUT call.,

        [DataMember(EmitDefaultValue = false)]
        public bool self { get; set; } //True if this participant is the same user that is calling the API

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; } //['REPLACED' or 'ACTIVE']: The status of the participant

    }

    [DataContract]
    public class SenderInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string company { get; set; } //Company of the sender, if available.,

        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; } //Email of the sender of the agreement.,

        [DataMember(EmitDefaultValue = false)]
        public bool hidden { get; set; } //True if the agreement is hidden for the user that is calling the API. Only returned if self is true.,

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; } //Name of the sender, if available.,

        [DataMember(EmitDefaultValue = false)]
        public string participantId { get; set; } //The unique identifier of the sender of the agreement.,

        [DataMember(EmitDefaultValue = false)]
        public bool self { get; set; } //True if the sender is the same user that is calling the API.,

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; }

    }

    [DataContract]
    public class ShareParticipantInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string company { get; set; } // Company of the sharee participant, if available.,

        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; } //Email of the sharee participant of the agreement.,

        [DataMember(EmitDefaultValue = false)]
        public bool hidden { get; set; } //True if the agreement is hidden for the user that is calling the API. Only returned if self is true.,

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; } //Name of the sharee participant, if available.,

        [DataMember(EmitDefaultValue = false)]
        public string participantId { get; set; } //The unique identifier of the sharee participant of the agreement.,

        [DataMember(EmitDefaultValue = false)]
        public bool self { get; set; } //True if the Share participant is the same user that is calling the API.,

        [DataMember(EmitDefaultValue = false)]
        public string sharerParticipantId { get; set; } //The unique identifier of the participant who shared the agreement.

    }

    #endregion  Agreement MembersInfo

    #region Agreement Visibility

    [DataContract]
    public class VisibilityInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string visibility {get;set;}//['SHOW' or 'HIDE']: Specifies the visibility.The possible values are HIDE or SHOW
    }

    #endregion Agreement Visibility

    #region Agreement Rejection for particular participant

    [DataContract]
    public class AgreementRejectionInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string comment { get; set; } //Comment describing the reason to reject this agreement.
    }

    #endregion Agreement Rejection for particular participant

    #region Update Agreement State / Cancel an agreement

    [DataContract]
    public class AgreementStateInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string state { get; set; } //['AUTHORING' or 'CANCELLED' or 'IN_PROCESS']: The state in which the agreement should land,
        
        [DataMember(EmitDefaultValue = false)]
        public AgreementCancellationInfo agreementCancellationInfo { get; set; } //Cancellation information for the agreement
    }

    [DataContract]
    public class AgreementCancellationInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string comment { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool notifyOthers { get; set; }
    }

    #endregion Update Agreement State  / Cancel an agreement

    #region agreement Signing URLs    

    [DataContract]
    public class SigningUrlResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public List<SigningUrlSetInfo> signingUrlSetInfos { get; set; } //An array of urls for signer sets involved in this agreement.
    }

    [DataContract]
    public class SigningUrlSetInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<SigningUrl> signingUrls { get; set; } //An array of urls for current signer set.,

        [DataMember(EmitDefaultValue = false)]
        public string signingUrlSetName { get; set; } //The name of the current signer set.Returned only, if the API caller is the sender of agreement
    }

    [DataContract]
    public class SigningUrl
    {
        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; } //The email address of the signer associated with this signing url,

        [DataMember(EmitDefaultValue = false)]
        public string esignUrl { get; set; } //The email address of the signer associated with this signing url
    }

    #endregion agreement Signing URLs

    #region My AgreementInfos

    /// <summary></summary>
    [DataContract]
    public class MyAgreementInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<AccountSharerInfo> accountSharers { get; set; } //A list of account sharer in relation to the api caller and this resource.
    }

    [DataContract]
    public class AccountSharerInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string email { get; set; } //The email address of the user

        [DataMember(EmitDefaultValue = false)]
        public string fullName { get; set; } //Full name of this account sharer.,

        [DataMember(EmitDefaultValue = false)]
        public List<string> permissions { get; set; } //['VIEW' or 'SEND' or 'MODIFY']: A list of permissions given for this account sharing.,

        [DataMember(EmitDefaultValue = false)]
        public string userId { get; set; } //A unique identifier of the user resource for REST APIs as issued by Sign

    }

    #endregion My AgreementInfos

    #region Agreement Views

    /// <summary>Common view configuration for all the available views</summary>
    [DataContract]
    public class CommonViewConfiguration
    {
        /// <summary>Auto LogIn Flag. If true, the URL returned will automatically log the user in. If false, the URL returned will require the credentials. By default its value is false</summary>
        [DataMember(EmitDefaultValue = false)]
        public bool autoLoginUser { get; set; }

        /// <summary>Message template locale</summary>
        [DataMember(EmitDefaultValue = false)]
        public string locale { get; set; }

        /// <summary>No Chrome Flag. If true, the embedded page is shown without a navigation header or footer. If false, the standard page header and footer will be present. By default its value is false</summary>
        [DataMember(EmitDefaultValue = false)]
        public bool noChrome { get; set; }
    }

    /// <summary>Controls various file upload options available on the compose page</summary>
    [DataContract]
    public class FileUploadOptions
    {
        /// <summary>Whether library documents link should appear or not. Default value is taken as true</summary>
        [DataMember(EmitDefaultValue = false)]
        public bool libraryDocument { get; set; }

        /// <summary>Whether local file upload button should appear or not. Default value is taken as true</summary>
        [DataMember(EmitDefaultValue = false)]
        public bool localFile { get; set; }

        /// <summary>Whether link to attach documents from web sources like Dropbox should appear or not. Default value is taken as true</summary>
        [DataMember(EmitDefaultValue = false)]
        public bool webConnectors { get; set; }
    }

    /// <summary>Compose page view configuration</summary>
    [DataContract]
    public class ComposeViewConfiguration
    {
        /// <summary>Controls various file upload options available on the compose page</summary>
        [DataMember(EmitDefaultValue = false)]
        public FileUploadOptions fileUploadOptions { get; set; }

        /// <summary>Should the compose page be provided with authoring mode selected?</summary>
        [DataMember(EmitDefaultValue = false)]
        public bool isPreviewSelected { get; set; }
    }

    /// <summary>Name of the required view and its desired configuration.</summary>
    [DataContract]
    public class AgreementViewInfo
    {
        /// <summary>['COMPOSE' or 'MODIFY' or 'PREFILL' or 'AUTHORING' or 'SEND_PROGRESS' or 'POST_CREATE' or 'DOCUMENT' or 'MANAGE' or 'SIGNING' or 'ALL']: Name of the requested agreement view</summary>
        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        /// <summary>Common view configuration for all the available views</summary>
        [DataMember(EmitDefaultValue = false)]
        public CommonViewConfiguration commonViewConfiguration { get; set; }

        /// <summary>Compose page view configuration</summary>
        [DataMember(EmitDefaultValue = false)]
        public ComposeViewConfiguration composeViewConfiguration { get; set; }
    }

    #endregion  Agreement Agreement Views

}


