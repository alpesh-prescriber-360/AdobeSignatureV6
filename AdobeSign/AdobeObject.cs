using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace AdobeSignatureV6
{
    public class AdobeObject
    {
        private RestAPI API;
        public AdobeObject(RestAPI api)
        {
            API = api;
        }

        /// <summary>
        /// Gets the base uri to access other APIs. In case other APIs are accessed from a different end point, it will be considered an invalid request
        /// </summary>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.BaseUriInfo> GetBaseURI()
        {
            string json = await API.GetRestJson("/base_uris");
            return API.DeserializeJSon<AdobeSignatureV6.BaseUriInfo>(json);
        }


        #region Agreements

        /// <summary>
        /// This is a primary endpoint which is used to create a new agreement. An agreement can be created using transientDocument, libraryDocument or a URL. You can create an agreement in one of the 3 mentioned states: a) DRAFT - to incrementally build the agreement before sending out, b) AUTHORING - to add/edit form fields in the agreement, c) IN_PROCESS - to immediately send the agreement. You can use the PUT /agreements/{agreementId}/state endpoint to transition an agreement between the above mentioned states. An allowed transition would follow the following sequence: DRAFT -> AUTHORING -> IN_PROCESS -> CANCELLED.
        /// </summary>
        /// <param name="agreementInfo">Information about the agreement that you want to create.</param>
        /// <returns> Respone Class: AdobeSignatureV6.AgreementCreationResponse </returns>
        public async Task<AdobeSignatureV6.AgreementCreationResponse> CreateAgreement(AdobeSignatureV6.AgreementInfo agreementInfo)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.AgreementInfo>(agreementInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string json = await API.PostRest("/agreements", byteContent);
            return API.DeserializeJSon<AdobeSignatureV6.AgreementCreationResponse>(json);

        }

        /// <summary>
        /// Updates the agreement in draft state, or update the expirationTime on an existing agreement that is already out for signature
        /// </summary>
        /// <param name="agreeementID">The agreement identifier</param>
        /// <param name="agreementInfo">Information necessary to update a modifiable agreement that is presently out for signature</param>
        /// <returns>Empty string if success else gives an error message</returns>
        public async Task<string> UpdateAgreement(string agreeementID, AdobeSignatureV6.AgreementInfo agreementInfo)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.AgreementInfo>(agreementInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}", agreeementID);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }


        /// <summary>
        /// This endpoint can be used by originator/sender of an agreement to transition between the states of agreement. An allowed transition would follow the following sequence: DRAFT -> AUTHORING -> IN_PROCESS -> CANCELLED.
        /// </summary>
        /// <param name="agreeementID">The agreement identifier</param>
        /// <param name="agreementStateInfo">The state in which the agreement should land and Cancellation information for the agreement</param>
        /// <returns>Empty string if success else gives an error message</returns>
        public async Task<string> CancelAgreement(string agreeementID, AdobeSignatureV6.AgreementStateInfo agreementStateInfo)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.AgreementStateInfo>(agreementStateInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/state", agreeementID);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Updates the visibility of an agreement
        /// </summary>
        /// <param name="agreementId">The agreement identifier</param>
        /// <param name="visibilityInfo">Information to update visibility of agreement</param>
        /// <returns>Returns eempty string if call is successful else if returns an error message</returns>
        public async Task<string> UpdateAgreementVisibility(string agreementId, AdobeSignatureV6.VisibilityInfo visibilityInfo)
        {
            if (string.IsNullOrWhiteSpace(agreementId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.VisibilityInfo>(visibilityInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/me/visibility", agreementId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Set the merge info for an agreement
        /// </summary>
        /// <param name="agreementId">The agreement identifier</param>
        /// <param name="formFieldMergeInfo">A mapping indicating the default values to set for form fields</param>
        /// <returns>Returns eempty string if call is successful else if returns an error message</returns>
        public async Task<string> UpdateAgreementMergeFieldInfo(string agreementId, AdobeSignatureV6.FormFieldMergeInfo formFieldMergeInfo)
        {
            if (string.IsNullOrWhiteSpace(agreementId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.FormFieldMergeInfo>(formFieldMergeInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/formFields/mergeInfo", agreementId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Updates the participant set of an agreement identified by agreementId in the path
        /// </summary>
        /// <param name="agreementId">The agreement identifier</param>
        /// <param name="participantSetId">The participant set identifier</param>
        /// <param name="detailedParticipantSetInfo">The new participant set info</param>
        /// <returns>Returns eempty string if call is successful else if returns an error message</returns>
        public async Task<string> UpdateAgreementParticipantSetsinfo(string agreementId, string participantSetId, AdobeSignatureV6.DetailedParticipantSetInfo detailedParticipantSetInfo)
        {
            if (string.IsNullOrWhiteSpace(agreementId) && string.IsNullOrWhiteSpace(participantSetId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.DetailedParticipantSetInfo>(detailedParticipantSetInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/members/participantSets/{1}", agreementId,participantSetId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Rejects the agreement for a participant
        /// </summary>
        /// <param name="agreementId">The agreement identifier</param>
        /// <param name="participantSetId">The participant set identifier</param>
        /// <param name="participantId">The participant identifier</param>
        /// <param name="agreementRejectionInfo">Participant rejection information required for rejecting the agreement</param>
        /// <returns></returns>
        public async Task<string> RejectAgreementForParticipant(string agreementId, string participantSetId, string participantId, AdobeSignatureV6.AgreementRejectionInfo agreementRejectionInfo)
        {
            if (string.IsNullOrWhiteSpace(agreementId) && string.IsNullOrWhiteSpace(participantSetId) && string.IsNullOrWhiteSpace(participantId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.AgreementRejectionInfo>(agreementRejectionInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/members/participantSets/{1}/participants/{2}/reject", agreementId, participantSetId,participantId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Updates the security options for a particular participant
        /// </summary>
        /// <param name="agreementId">The agreement identifier</param>
        /// <param name="participantSetId">The participant set identifier</param>
        /// <param name="participantId">The participant identifier</param>
        /// <param name="participantSecurityOption">Security options that apply to the participant</param>
        /// <returns></returns>
        public async Task<string> UpdateAgreementForParticipantSetSecurityOptions(string agreementId, string participantSetId, string participantId, AdobeSignatureV6.ParticipantSecurityOption participantSecurityOption)
        {
            if (string.IsNullOrWhiteSpace(agreementId) && string.IsNullOrWhiteSpace(participantSetId) && string.IsNullOrWhiteSpace(participantId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.ParticipantSecurityOption>(participantSecurityOption);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/members/participantSets/{1}/participants/{2}/securityOptions", agreementId, participantSetId, participantId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// You can only update an ACTIVE reminder, and can only update the status to 'CANCELED', update reminderParticipantIds, or update note
        /// </summary>
        /// <param name="agreementId">The agreement identifier</param>
        /// <param name="reminderId">The reminder identifier</param>
        /// <param name="reminderInfo">The information about a reminder associated with a recipient of an agreement</param>
        /// <returns></returns>
        public async Task<string> UpdateAgreementReminder(string agreementId, string reminderId, AdobeSignatureV6.ReminderInfo reminderInfo)
        {
            if (string.IsNullOrWhiteSpace(agreementId) && string.IsNullOrWhiteSpace(reminderId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.ReminderInfo>(reminderInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/reminders/{1}", agreementId, reminderId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Updates the latest note associated with an agreement
        /// </summary>
        /// <param name="agreementId">The agreement identifier</param>
        /// <param name="note">The note to be associated with the agreement</param>
        /// <returns></returns>
        public async Task<string> UpdateAgreementNote(string agreementId, AdobeSignatureV6.Note note)
        {
            if (string.IsNullOrWhiteSpace(agreementId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.Note>(note);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/me/note", agreementId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// 
        /// </summary>
        /// /// <param name="agreeementID">The agreement identifier</param>
        /// <param name="shareCreationInfoList">List of agreement share creation information objects</param>
        /// <returns>Response Class: ShareCreationResponseList</returns>
        public async Task<AdobeSignatureV6.ShareCreationResponseList> ShareAgreement(string agreeementID, AdobeSignatureV6.ShareCreationInfoList shareCreationInfoList)
        {            
            if (string.IsNullOrWhiteSpace(agreeementID))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.ShareCreationInfoList>(shareCreationInfoList);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/members/share", agreeementID);
            string json = await API.PostRest(endpoint, byteContent);
            return API.DeserializeJSon<AdobeSignatureV6.ShareCreationResponseList>(json);

        }

        /// <summary>
        /// Retrieves agreements for the user
        /// </summary>
        /// <returns>An array of UserAgreement items</returns>
        public async Task<AdobeSignatureV6.UserAgreements> GetAgreementList()
        {
            string json = await API.GetRestJson("/agreements");
            return API.DeserializeJSon<AdobeSignatureV6.UserAgreements>(json);
        }

        /// <summary>
        /// Retrieves the current status of an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns></returns>
        public async Task<AgreementInfo> GetAgreementDetails(string agreementID)
        {            
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.AgreementInfo>(json);
        }

        /// <summary>
        /// Gets a single combined PDF document for the documents associated with an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns>A File Stream of combined PDF document</returns>
        public async Task<byte[]> GetAgreementCombinedDocument(string agreementID)
        {
            var endpoint = string.Format("/agreements/{0}/combinedDocument", agreementID);
            return await API.GetRestBytes(endpoint);
        }

        /// <summary>
        /// Retrieves info of all pages of a combined PDF document for the documents associated with an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns>Response Class: CombinedDocumentPagesInfo</returns>
        public async Task<AdobeSignatureV6.CombinedDocumentPagesInfo> GetAgreementCombinedDocumentPagesInfo(string agreementID)
        {
            //On null or empty agreement id, API is returning all agreements.
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/combinedDocument/pagesInfo", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.CombinedDocumentPagesInfo>(json);
        }

        /// <summary>
        /// Retrieves url of all visible pages of all the documents associated with an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns>Agreement combined document url</returns>
        public async Task<AdobeSignatureV6.DocumentUrl> GetAgreementCombinedDocumentDocumentURL(string agreementID)
        {
            //On null or empty agreement id, API is returning all agreements.
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/combinedDocument/url", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.DocumentUrl>(json);
        }

        /// <summary>
        /// Retrieves the IDs of the documents of an agreement identified by agreementId
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns>Response Class: AgreementDocuments</returns>
        public async Task<AdobeSignatureV6.AgreementDocuments> GetAgreementDocuments(string agreementID)
        {
            //On null or empty agreement id, API is returning all agreements.
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/documents", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.AgreementDocuments>(json);
        }

        /// <summary>Retrieves the file stream of a document of an agreement</summary>
        /// <param name="agreementID">Agreement identifier</param>
        /// <param name="documentID">The document identifier, as retrieved from the API which fetches the documents of a specified agreement</param>
        /// <returns>Retrieves the file stream of a document of an agreement</returns>
        public async Task<byte[]> GetAgreementDocumentbyDocumentId(string agreementID, string documentID)
        {
            var endpoint = string.Format("/agreements/{0}/documents/{1}", agreementID, documentID);
            return await API.GetRestBytes(endpoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agreementID">Agreement identifier</param>
        /// <param name="documentID">Document identifier</param>
        /// <param name="imageSizes">A comma separated list of image sizes i.e. {FIXED_WIDTH_50px, FIXED_WIDTH_250px, FIXED_WIDTH_675px, ZOOM_50_PERCENT, ZOOM_75_PERCENT, ZOOM_100_PERCENT, ZOOM_125_PERCENT, ZOOM_150_PERCENT, ZOOM_200_PERCENT}. Default sizes returned are {FIXED_WIDTH_50px, FIXED_WIDTH_250px, FIXED_WIDTH_675px, ZOOM_100_PERCENT}</param>
        /// <param name="showImageAvailabilityOnly">When set to true, returns only image availability. Else, returns both image urls and its availability</param>
        /// <param name="startPage">Start of page number range for which imageUrls are requested. Starting page number should be greater than 0</param>
        /// <param name="endPage">End of page number range for which imageUrls are requested</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.AgreementDocumentImageUrlsInfo> GetAgreementDocumentImageUrlsbyDocumentId(string agreementID, string documentID,string imageSizes="", bool showImageAvailabilityOnly = false, int startPage = 0, int endPage = 0)
        {
            if (string.IsNullOrWhiteSpace(agreementID) && string.IsNullOrWhiteSpace(documentID))
            {
                return null;
            }

            var query = string.Empty;
            query = string.IsNullOrEmpty(imageSizes) ? "" : "?imageSizes=" + imageSizes;
            query = string.IsNullOrEmpty(query) ? (showImageAvailabilityOnly) ? "?showImageAvailabilityOnly=true" : "" : (showImageAvailabilityOnly) ? query + "&showImageAvailabilityOnly=true" : query;
            query = string.IsNullOrEmpty(query) ? (startPage > 0) && (endPage > 0) && (endPage >= startPage) ? "?startPage=" + startPage + "&endPage=" + endPage : "" : (startPage > 0) && (endPage > 0) && (endPage >= startPage) ? query + "&startPage=" + startPage + "&endPage=" + endPage : query;
            var endpoint = string.Format("/agreements/{0}/documents/{1}/imageUrls"+query, agreementID, documentID);            
            string json =  await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.AgreementDocumentImageUrlsInfo>(json);
        }

        /// <summary>
        /// Retrieves image urls of all visible pages of all the documents associated with an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <param name="versionId">The version identifier of agreement as provided by the API</param>
        /// <param name="participantId">The participant identifier to be used to retrieve documents</param>
        /// <param name="imageSizes">A comma separated list of image sizes</param>
        /// <param name="includeSupportingDocumentsImageUrls">When set to true, returns image urls of supporting documents as well. Else, returns image urls of only the original documents</param>
        /// <param name="showImageAvailabilityOnly">When set to true, returns only image availability. Else, returns both image urls and its availability</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.AgreementDocumentImageUrlsInfo> GetAgreementDocumentImageUrls(string agreementID, string versionId="", string participantId="", string imageSizes = "", bool includeSupportingDocumentsImageUrls = false, bool showImageAvailabilityOnly = false)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var query = string.Empty;
            query = string.IsNullOrEmpty(versionId) ? "" : "?versionId=" + versionId;
            query = string.IsNullOrEmpty(query) ? (string.IsNullOrEmpty(participantId)) ? "?participantId="+ participantId : "" : (string.IsNullOrEmpty(participantId)) ? "?participantId="+ participantId : query;
            query = string.IsNullOrEmpty(query) ? (string.IsNullOrEmpty(imageSizes)) ? "?imageSizes=" + imageSizes : "" : (string.IsNullOrEmpty(imageSizes)) ? "?imageSizes=" + imageSizes : query;
            query = string.IsNullOrEmpty(query) ? (includeSupportingDocumentsImageUrls) ? "?includeSupportingDocumentsImageUrls=true" : "" : (includeSupportingDocumentsImageUrls) ? query + "&includeSupportingDocumentsImageUrls=true" : query;
            query = string.IsNullOrEmpty(query) ? (showImageAvailabilityOnly) ? "?showImageAvailabilityOnly=true" : "" : (showImageAvailabilityOnly) ? query + "&showImageAvailabilityOnly=true" : query;

            var endpoint = string.Format("/agreements/{0}/documents/imageUrls" + query, agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.AgreementDocumentImageUrlsInfo>(json);
        }

        /// <summary>
        /// Retrieves the events information for an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns>Response Class: EventList</returns>
        public async Task<AdobeSignatureV6.EventList> GetAgreementEvents(string agreementID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/events", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.EventList>(json);
        }

        /// <summary>
        /// Retrieves details of form fields of an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns>Response Class : AgreementFormFields</returns>
        public async Task<AdobeSignatureV6.AgreementFormFields> GetAgreementFormFields(string agreementID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/formFields", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.AgreementFormFields>(json);
        }

        /// <summary>
        /// If agreement is in DRAFT state then it retrieves the merge info stored with an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.FormFieldMergeInfo> GetAgreementMergeFieldInfo(string agreementID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/formFields/mergeInfo", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.FormFieldMergeInfo>(json);
        }

        /// <summary>
        /// Retrieves the latest note associated with an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.Note> GetAgreementNote(string agreementID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/me/note", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.Note>(json);
        }

        /// <summary>
        /// Retrieves the reminders of an agreement, identified by agreementId in the path
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.MegaSignRemindersResponse> GetAgreementReminders(string agreementID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/reminders", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MegaSignRemindersResponse>(json);
        }

        /// <summary>
        /// Retrieves a specific reminder associated with an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <param name="reminderID">The reminder identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.ReminderInfo> GetAgreementReminderDetails(string agreementID, string reminderID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/reminders/{1}", agreementID, reminderID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.ReminderInfo>(json);
        }

        /// <summary>
        /// Retrieves the URL for the e-sign page for the current signer(s) of an agreement
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.SigningUrlResponse> GetAgreementSigningUrls(string agreementID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/signingUrls", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.SigningUrlResponse>(json);
        }

        /// <summary>
        /// Retrieves the agreement information related to the api caller
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.MyAgreementInfo> GetAgreementApiCallerInfo(string agreementID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/me", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MyAgreementInfo>(json);
        }

        /// <summary>
        /// Retrieves data entered into the interactive form fields of the agreement
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns>string, Accept: text/csv</returns>
        public async Task<byte[]> GetAgreementFormData(string agreementID)
        {
            var endpoint = string.Format("/agreements/{0}/formData", agreementID);
            return await API.GetRestBytes(endpoint);

        }

        /// <summary>
        /// Retrieves information of members of the agreement
        /// </summary>
        /// <param name="agreementID"></param>
        /// <returns> Response Class : AdobeSignatureV6.MembersInfo  </returns>
        public async Task<AdobeSignatureV6.MembersInfo> GetAgreementMembersInfo(string agreementID)
        {
            //On null or empty agreement id, API is returning all agreements.
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/members", agreementID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MembersInfo>(json);
        }

        /// <summary>
        /// Retrieves the audit trail of an agreement identified by agreementId
        /// </summary>
        /// <param name="agreementID">The agreement identifier</param>
        /// <returns>PDF file stream containing audit trail information</returns>
        public async Task<byte[]> GetAgreementAuditTrail(string agreementID)
        {
            if (string.IsNullOrWhiteSpace(agreementID))
            {
                return null;
            }

            var endpoint = string.Format("/agreements/{0}/auditTrail", agreementID);
            return await API.GetRestBytes(endpoint);

        }        

        #endregion Agreements

        #region MegaSign

        /// <summary>
        /// This is a primary endpoint which is used to create a new megaSign. A megaSign can be created using transientDocument, libraryDocument or a URL. You can create a megaSign in IN_PROCESS - Create a megaSign in this state to immediately send it. You can use the PUT/megaSigns/{megaSignId}/state endpoint to transition the state of megaSign. An allowed transition would follow the following sequence: IN_PROCESS -> CANCELLED.
        /// </summary>
        /// <param name="megaSignCreationRequest">Information about the MegaSign that you want to send</param>
        /// <returns>Response Class: MegaSignCreationResponse</returns>
        public async Task<AdobeSignatureV6.MegaSignCreationResponse> CreateMegaSignAgreement(AdobeSignatureV6.MegaSignInfo megaSignCreationRequest)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.MegaSignInfo>(megaSignCreationRequest);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string json = await API.PostRest("/megaSigns", byteContent);
            return API.DeserializeJSon<AdobeSignatureV6.MegaSignCreationResponse>(json);

        }

        /// <summary>
        /// This endpoint can be used by creator of the MegaSign to transition between the states of megaSign. An allowed transition would follow the following sequence : IN_PROCESS->CANCELLED
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement, as returned by the megaSign creation API or retrieved from the API to fetch megaSign agreements</param>
        /// <param name="megaSignstatusUpdate">MegaSign state update information object</param>
        /// <returns></returns>
        public async Task<string> CancelMegaSignAgreement(string megaSignId, AdobeSignatureV6.MegaSignStateInfo megaSignstatusUpdate)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.MegaSignStateInfo>(megaSignstatusUpdate);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/megaSigns/{0}/state", megaSignId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Update the expirationTime on an existing megaSign parent and all the child agreements that are already out for signature
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement</param>
        /// <param name="megaSignInfo">Information necessary to update a modifiable megaSign parent that is presently out for signature</param>
        /// <returns></returns>
        public async Task<string> UpdateMegaSignAgreement(string megaSignId, AdobeSignatureV6.MegaSignInfo megaSignInfo)
        {
            if (string.IsNullOrWhiteSpace(megaSignId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.MegaSignInfo>(megaSignInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/megaSigns/{0}", megaSignId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Updates an existing reminder for a MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement, as returned by the megaSign creation API or retrieved from the API to fetch megaSign agreements</param>
        /// <param name="reminderId">The reminder identifier</param>
        /// <param name="reminderInfo">The information about a reminder associated with a recipient of an agreement</param>
        /// <returns></returns>
        public async Task<string> UpdateMegaSignReminder(string megaSignId, string reminderId, AdobeSignatureV6.ReminderInfo reminderInfo)
        {
            if (string.IsNullOrWhiteSpace(megaSignId) && string.IsNullOrWhiteSpace(reminderId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.ReminderInfo>(reminderInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/megaSigns/{0}/reminders/{1}", megaSignId, reminderId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Updates the latest note of a MegaSign parent agreement for the user
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement</param>
        /// <param name="note">The note to be associated with the MegaSign parent agreement</param>
        /// <returns></returns>
        public async Task<string> UpdateMegaSignNote(string megaSignId, AdobeSignatureV6.Note note)
        {
            if (string.IsNullOrWhiteSpace(megaSignId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.Note>(note);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/megaSigns/{0}/me/note", megaSignId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Share a MegaSign parent and all the child agreements with someone
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement</param>
        /// <param name="shareCreationInfoList">List of agreement share creation information objects</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.ShareCreationResponseList> ShareMegaSignAgreement(string megaSignId, AdobeSignatureV6.ShareCreationInfoList shareCreationInfoList)
        {
            if (string.IsNullOrWhiteSpace(megaSignId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.ShareCreationInfoList>(shareCreationInfoList);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/megaSigns/{0}/members/share", megaSignId);
            string json = await API.PostRest(endpoint, byteContent);
            return API.DeserializeJSon<AdobeSignatureV6.ShareCreationResponseList>(json);

        }

        /// <summary>
        /// Updates the visibility of a MegaSign
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement, as returned by the megaSign creation API or retrieved from the API to fetch megaSign agreements</param>
        /// <param name="visibilityInfo">Information to update visibility of agreement</param>
        /// <returns>Returns eempty string if call is successful else if returns an error message</returns>
        public async Task<string> UpdateMegaSignVisibility(string megaSignId, AdobeSignatureV6.VisibilityInfo visibilityInfo)
        {
            if (string.IsNullOrWhiteSpace(megaSignId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.VisibilityInfo>(visibilityInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/megaSigns/{0}/me/visibility", megaSignId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Retrieves MegaSign parent agreements for a user
        /// </summary>
        /// <returns>Response Class: MegaSigns</returns>
        public async Task<AdobeSignatureV6.MegaSigns> GetMegaSignsList()
        {
            string json = await API.GetRestJson("/megaSigns");
            return API.DeserializeJSon<AdobeSignatureV6.MegaSigns>(json);
        }


        /// <summary>
        /// Get detailed information of the specified MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">Response Class: MegaSignInfo</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.MegaSignInfo> GetMegaSignDetails(string megaSignID)
        {          
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}", megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MegaSignInfo>(json);
        }

        /// <summary>
        /// Get all the child agreements of the specified MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement, as returned by the megaSign creation API or retrieved from the API to fetch megaSign agreements</param>
        /// <returns>Response Class: MegaSignChildAgreements</returns>
        public async Task<AdobeSignatureV6.MegaSignChildAgreements> GetMegaSignChildAgreements(string megaSignID)
        {
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/agreements", megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MegaSignChildAgreements>(json);
        }

        /// <summary>
        /// Retrieves data entered by recipients into interactive form fields at the time they signed the child agreements of the specified MegaSign agreement
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement, as returned by the megaSign creation API or retrieved from the API to fetch megaSign agreements</param>
        /// <returns>CSV file stream containing form data information</returns>
        public async Task<byte[]> GetMegaSignFormData(string megaSignId)
        {
            var endpoint = string.Format("/megaSigns/{0}/formData", megaSignId);
            return await API.GetRestBytes(endpoint);

        }

        /// <summary>
        /// Retrieves the file stream of the original childAgreementsInfoFile that was uploaded by sender while creating the MegaSign
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement</param>
        /// <param name="childAgreementsInfoFileId">The identifier of the childAgreementsInfoFile that has been uploaded by sender while creating the megaSign or retrieved from the API to fetch megaSignInfo</param>
        /// <returns></returns>
        public async Task<byte[]> GetMegaSignChildAgreementInfoFile(string megaSignId, string childAgreementsInfoFileId)
        {
            if (string.IsNullOrWhiteSpace(megaSignId) && string.IsNullOrWhiteSpace(childAgreementsInfoFileId))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/childAgreementsInfo/{1}", megaSignId, childAgreementsInfoFileId);
            return await API.GetRestBytes(endpoint);

        }

        /// <summary>
        /// Retrieves a single combined PDF document for the documents associated with the MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement</param>
        /// <returns></returns>
        public async Task<byte[]> GetMegaSignCombinedDocument(string megaSignId)
        {
            if (string.IsNullOrWhiteSpace(megaSignId))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/combinedDocument", megaSignId);
            return await API.GetRestBytes(endpoint);

        }

        /// <summary>
        /// Retrieves url of all visible pages of all the documents associated with the MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.DocumentUrl> GetMegaSignCombinedDocumentUrl(string megaSignID)
        {
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/combinedDocument/url", megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.DocumentUrl>(json);
        }

        /// <summary>
        /// Retrieves the events information for the MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.EventList> GetMegaSignEvents(string megaSignID)
        {
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/events", megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.EventList>(json);
        }

        /// <summary>
        /// Retrieves the latest note of a MegaSign parent agreement for the user
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.Note> GetMegaSignNote(string megaSignID)
        {
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/note", megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.Note>(json);
        }

        /// <summary>
        /// Retrieves image urls of all visible pages of a document associated with a MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement</param>
        /// <param name="imageSizes">A comma separated list of image sizes i.e. {FIXED_WIDTH_50px, FIXED_WIDTH_250px, FIXED_WIDTH_675px, ZOOM_50_PERCENT, ZOOM_75_PERCENT, ZOOM_100_PERCENT, ZOOM_125_PERCENT, ZOOM_150_PERCENT, ZOOM_200_PERCENT}. Default sizes returned are {FIXED_WIDTH_50px, FIXED_WIDTH_250px, FIXED_WIDTH_675px, ZOOM_100_PERCENT}</param>
        /// <param name="showImageAvailabilityOnly">When set to true, returns only image availability. Else, returns both image urls and its availability</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.BasicDocumentsImageUrlsInfo> GetMegaSignDocumentsImageUrls(string megaSignID,string imageSizes="",bool showImageAvailabilityOnly=false)
        {
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var query = string.Empty;
            query = string.IsNullOrEmpty(imageSizes) ? "" : "?imageSizes=" + imageSizes;            
            query = string.IsNullOrEmpty(query) ? (showImageAvailabilityOnly) ? "?showImageAvailabilityOnly=false" : "" : (showImageAvailabilityOnly) ? query + "&showImageAvailabilityOnly=true" : query;

            var endpoint = string.Format("/megaSigns/{0}/documents/imageUrls" + query, megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.BasicDocumentsImageUrlsInfo>(json);
        }

        /// <summary>
        /// Retrieves the IDs of the documents associated with a MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.AgreementDocuments> GetMegaSignDocuments(string megaSignID)
        {
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/documents", megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.AgreementDocuments>(json);
        }

        /// <summary>
        /// Retrieves the file stream of a document of a MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement</param>
        /// <param name="documentId">The document identifier</param>
        /// <returns></returns>
        public async Task<byte[]> GetMegaSignDocumentsByDocumentId(string megaSignId, string documentId)
        {
            if (string.IsNullOrWhiteSpace(megaSignId))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/documents/{1}", megaSignId, documentId);
            return await API.GetRestBytes(endpoint);

        }

        /// <summary>
        /// Retrieves the MegaSign parent agreement information related to the api caller
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.MyAgreementInfo> GetMegaSignApiCallerInfo(string megaSignID)
        {
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/me", megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MyAgreementInfo>(json);
        }

        /// <summary>
        /// Retrieves the reminders of the specified MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement</param>
        /// /// <param name="status">A comma-separated list of reminder statuses of the reminders which should be returned in the response. Currently supported values are ACTIVE, CANCELED, COMPLETE</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.MegaSignRemindersResponse> GetMegaSignReminders(string megaSignID, string status="")
        {
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }
            var query = string.Empty;
            query = string.IsNullOrEmpty(status) ? "" : "?status=" + status;

            var endpoint = string.Format("/megaSigns/{0}/reminders" + query, megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MegaSignRemindersResponse>(json);
        }

        /// <summary>
        /// Retrieves a specific reminder given the reminder id for the specified MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement</param>
        /// <param name="reminderId">The reminder identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.ReminderInfo> GetMegaSignReminderDetails(string megaSignID, string reminderId)
        {
            if (string.IsNullOrWhiteSpace(megaSignID) && string.IsNullOrWhiteSpace(reminderId))
            {
                return null;
            }
            
            var endpoint = string.Format("/megaSigns/{0}/reminders/{1}", megaSignID,reminderId);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.ReminderInfo>(json);
        }

        /// <summary>
        /// Retrieves detailed member info along with IDs for different types of participants associated with the MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignID">The identifier of the MegaSign parent agreement, as returned by the megaSign creation API or retrieved from the API to fetch megaSign agreements</param>
        /// <returns>Response Class: MegaSignMembersInfo</returns>
        public async Task<AdobeSignatureV6.MegaSignMembersInfo> GetMegaSignMembersInfo(string megaSignID)
        {
            //On null or empty agreement id, API is returning all agreements.
            if (string.IsNullOrWhiteSpace(megaSignID))
            {
                return null;
            }

            var endpoint = string.Format("/megaSigns/{0}/members", megaSignID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MegaSignMembersInfo>(json);
        }

        #endregion

        #region Transient Document
        /// <summary>
        /// The document uploaded through this call is referred to as transient since it is available only for 7 days after the upload. 
        /// The returned transient document ID can be used in the API calls where the uploaded file needs to be referred. 
        /// The transient document request is a multipart request consisting of three parts - filename, mime type and the file stream. 
        /// You can only upload one file at a time in this request.
        /// </summary>
        /// <param name="fileName">A name for the document being uploaded. Maximum number of characters in the name is restricted to 255</param>
        /// <param name="fileData">byte[]  of the document being uploaded</param>
        /// <param name="mimeType">The mime type of the document being uploaded</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.TransientDocumentResponse> CreateTransientDocument(string fileName, byte[] fileData, string mimeType = "")
        {
            string json = string.Empty;
            //try
            //{
                var content = new MultipartFormDataContent();
                HttpContent fileContent = new ByteArrayContent(fileData);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "File",
                    FileName = fileName

                };
                content.Add(fileContent);

                content.Add(new StringContent(fileName), String.Format("\"{0}\"", "File-Name"));


                if (!string.IsNullOrWhiteSpace(mimeType))
                {
                    content.Add(new StringContent(mimeType), String.Format("\"{0}\"", "Mime-Type"));
                }

                json = await API.PostRest("/transientDocuments", content);

            //}
            //catch (Exception ex)
            //{
            //}
            return API.DeserializeJSon<AdobeSignatureV6.TransientDocumentResponse>(json);
        }
        #endregion Transient Document

        #region Library Document

        /// <summary>
        /// Creates a Templates that is places in the library of the user for resuse
        /// </summary>
        /// <param name="libraryCreationInfo">Information about the library document that you want to create and authoring options that you want to apply at the time of creation.</param>
        /// <returns>Response Class: LibraryDocumentCreationResponse {id (string): The unique identifier that is used to refer to the library template }</returns>
        public async Task<AdobeSignatureV6.LibraryDocumentCreationResponse> CreateLibraryDocument(AdobeSignatureV6.LibraryDocumentInfo libraryCreationInfo)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.LibraryDocumentInfo>(libraryCreationInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string json = await API.PostRest("/libraryDocuments", byteContent);
            return API.DeserializeJSon<AdobeSignatureV6.LibraryDocumentCreationResponse>(json);
        }

        /// <summary>
        /// Currently state can be changed from AUTHORING to ACTIVE, AUTHORING to REMOVED or ACTIVE to REMOVED
        /// </summary>
        /// <param name="libraryDocumentId">The document identifier, as retrieved from the API to fetch library documents</param>
        /// <param name="libraryDocumentStateInfo">Information about the state of library document to which you want to update</param>
        /// <returns>Empty string if success else gives an error message</returns>
        public async Task<string> CancelLibraryDocument(string libraryDocumentId, AdobeSignatureV6.LibraryDocumentStateInfo libraryDocumentStateInfo)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.LibraryDocumentStateInfo>(libraryDocumentStateInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/libraryDocuments/{0}/state", libraryDocumentId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Updates the visibility of an LibraryDocument
        /// </summary>
        /// <param name="libraryDocumentId">The LibraryDocument identifier, as returned by the LibraryDocument creation API or retrieved from the API to fetch LibraryDocument</param>
        /// <param name="visibilityInfo">Information to update visibility of LibraryDocument</param>
        /// <returns>Returns eempty string if call is successful else if returns an error message</returns>
        public async Task<string> UpdateLibraryDocumentVisibility(string libraryDocumentId, AdobeSignatureV6.VisibilityInfo visibilityInfo)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.VisibilityInfo>(visibilityInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/libraryDocuments/{0}/me/visibility", libraryDocumentId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Currently status, name, sharingMode and templateTypes of the library document can only be updated
        /// </summary>
        /// <param name="libraryDocumentId">The document identifier, as retrieved from the API to fetch library documents</param>
        /// <param name="libraryDocumentInfo">Information to update visibility of LibraryDocument</param>
        /// <returns>Returns eempty string if call is successful else if returns an error message</returns>
        public async Task<string> UpdateLibraryDocument(string libraryDocumentId, AdobeSignatureV6.LibraryDocumentInfo libraryDocumentInfo)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.LibraryDocumentInfo>(libraryDocumentInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/libraryDocuments/{0}", libraryDocumentId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// The note to be associated with the library document
        /// </summary>
        /// <param name="libraryDocumentId">The document identifier</param>
        /// <param name="note">The note to be associated with the library document</param>
        /// <returns></returns>
        public async Task<string> UpdateLibraryDocumentNote(string libraryDocumentId, AdobeSignatureV6.Note note)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentId))
            {
                return null;
            }

            var jsonContent = API.SerializeJSon<AdobeSignatureV6.Note>(note);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/libraryDocuments/{0}/me/note", libraryDocumentId);
            string json = await API.PutRest(endpoint, byteContent);
            return json;

        }

        /// <summary>
        /// Retrieves library documents for a user
        /// </summary>
        /// <returns>Response Class: LibraryDocuments</returns>
        public async Task<AdobeSignatureV6.LibraryDocuments> GetlibraryDocuments()
        {
            string json = await API.GetRestJson("/libraryDocuments");
            
            return API.DeserializeJSon<AdobeSignatureV6.LibraryDocuments>(json);
        }

        /// <summary>
        /// Retrieves the details of a library document
        /// </summary>
        /// <param name="libraryDocumentID"></param>
        /// <returns>Response Class: LibraryDocumentInfo</returns>
        public async Task<AdobeSignatureV6.LibraryDocumentInfo> GetlibraryDocumentDetails(string libraryDocumentID)
        {
            //On null or empty agreement id, API is returning all agreements.
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var endpoint = string.Format("/libraryDocuments/{0}", libraryDocumentID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.LibraryDocumentInfo>(json);
        }

        /// <summary>
        /// Retrieves the combined document associated with a library document
        /// </summary>
        /// <param name="libDocID">The document identifier, as retrieved from the API to fetch library documents</param>
        /// <returns>File Stream of PDF file</returns>
        public async Task<byte[]> GetLibraryCombinedDocument(string libDocID)
        {
            var endpoint = string.Format("/libraryDocuments/{0}/combinedDocument", libDocID);
            return await API.GetRestBytes(endpoint);
        }

        /// <summary>
        /// Retrieves url of all visible pages of all the documents associated with a library document
        /// </summary>
        /// <param name="libDocID">The document identifier</param>
        /// <returns></returns>
        public async Task<byte[]> GetLibraryDocumentAuditTrail(string libDocID)
        {
            var endpoint = string.Format("/libraryDocuments/{0}/auditTrail", libDocID);
            return await API.GetRestBytes(endpoint);
        }

        /// <summary>
        /// Retrieves url of all visible pages of all the documents associated with a library document
        /// </summary>
        /// <param name="libraryDocumentID">The document identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.DocumentUrl> GetlibraryTemplateCombinedDocumentUrl(string libraryDocumentID)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var endpoint = string.Format("/libraryDocuments/{0}/combinedDocument/url", libraryDocumentID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.DocumentUrl>(json);
        }

        /// <summary>
        /// Retrieves the IDs of the documents associated with library document
        /// </summary>
        /// <param name="libraryDocumentID">The document identifier</param>
        /// <param name="versionId">The version identifier of library_document as provided by the API</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.Documents> GetlibraryTemplateDocuments(string libraryDocumentID, string versionId="")
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var query = string.IsNullOrEmpty(versionId) ? "" : "?versionId=" + versionId;
            var endpoint = string.Format("/libraryDocuments/{0}/documents", libraryDocumentID);
            string json = await API.GetRestJson(endpoint+query);
            return API.DeserializeJSon<AdobeSignatureV6.Documents>(json);
        }

        /// <summary>
        /// Retrieves the file stream of a document of library document
        /// </summary>
        /// <param name="libraryDocumentID">The document identifier</param>
        /// <param name="documentId">The document identifier</param>
        /// <returns></returns>
        public async Task<byte[]> GetlibraryTemplateDocumentByDocumentId(string libraryDocumentID, string documentId)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var endpoint = string.Format("/libraryDocuments/{0}/documents/{1}",libraryDocumentID, documentId);
            return await API.GetRestBytes(endpoint);
        }

        /// <summary>
        /// Retrieves data entered into the interactive form fields of the library document
        /// </summary>
        /// <param name="libraryDocumentID">The document identifier</param>        
        /// <returns></returns>
        public async Task<byte[]> GetlibraryTemplateFormData(string libraryDocumentID)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var endpoint = string.Format("/libraryDocuments/{0}/formData", libraryDocumentID);
            return await API.GetRestBytes(endpoint);
        }

        /// <summary>
        /// Retrieves information of members (creator) of the library document
        /// </summary>
        /// <param name="libraryDocumentID">The document identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.LibraryDocumentMembersInfo> GetlibraryTemplateDocuments(string libraryDocumentID)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var endpoint = string.Format("/libraryDocuments/{0}/members", libraryDocumentID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.LibraryDocumentMembersInfo>(json);
        }

        /// <summary>
        /// Retrieves the library document information related to the api caller
        /// </summary>
        /// <param name="libraryDocumentID">The document identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.MyAgreementInfo> GetlibraryTemplateApiCallerInfo(string libraryDocumentID)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var endpoint = string.Format("/libraryDocuments/{0}/me", libraryDocumentID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.MyAgreementInfo>(json);
        }

        /// <summary>
        /// Retrieves the events information for a library document
        /// </summary>
        /// <param name="libraryDocumentID">The document identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.EventList> GetlibraryTemplateEvents(string libraryDocumentID)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var endpoint = string.Format("/libraryDocuments/{0}/events", libraryDocumentID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.EventList>(json);
        }

        /// <summary>
        /// Retrieves the latest note of a library document for the API user
        /// </summary>
        /// <param name="libraryDocumentID">The document identifier</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.Note> GetlibraryTemplateNote(string libraryDocumentID)
        {
            if (string.IsNullOrWhiteSpace(libraryDocumentID))
            {
                return null;
            }

            var endpoint = string.Format("/libraryDocuments/{0}/note", libraryDocumentID);
            string json = await API.GetRestJson(endpoint);
            return API.DeserializeJSon<AdobeSignatureV6.Note>(json);
        }

        #endregion Library Document

        #region Reminder

        /// <summary>
        /// Creates a reminder on the specified participants of an agreement identified by agreementId in the path
        /// </summary>
        /// <param name="agreementId">The agreement identifier</param>
        /// <param name="reminderInfo">The information about a reminder associated with a recipient of an agreement</param>
        /// <returns>Response Class: ReminderCreationResult</returns>
        public async Task<AdobeSignatureV6.ReminderCreationResult> SendAgreementReminder(string agreementId, ReminderInfo reminderInfo)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.ReminderInfo>(reminderInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/agreements/{0}/reminders", agreementId);
            string json = await API.PostRest(endpoint, byteContent);
            return API.DeserializeJSon<AdobeSignatureV6.ReminderCreationResult>(json);
        }

        /// <summary>
        /// Retrieves the reminders of the specified MegaSign parent agreement
        /// </summary>
        /// <param name="megaSignId">The identifier of the MegaSign parent agreement, as returned by the megaSign creation API or retrieved from the API to fetch megaSign agreements</param>
        /// <param name="reminderInfo">The information about a reminder associated with a recipient of an agreement</param>
        /// <returns></returns>
        public async Task<AdobeSignatureV6.ReminderCreationResult> SendMegaSignReminder(string megaSignId, ReminderInfo reminderInfo)
        {
            var jsonContent = API.SerializeJSon<AdobeSignatureV6.ReminderInfo>(reminderInfo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var endpoint = string.Format("/megaSigns/{0}/reminders", megaSignId);
            string json = await API.PostRest(endpoint, byteContent);
            return API.DeserializeJSon<AdobeSignatureV6.ReminderCreationResult>(json);
        }

        #endregion Reminder

        #region Create Merge File Info List

        /// <summary>
        /// Dynamically add the megeFieldInfor for all the items in the Dictionary mergefields
        /// </summary>
        /// <param name="agreementInfo"></param>
        /// <param name="mergefields"></param>
        /// <returns></returns>
        public List<MergeFieldInfo> AddMergeFieldInfo(AgreementInfo agreementInfo, Dictionary<string, string> mergefields)
        {
            agreementInfo.mergeFieldInfo = new List<MergeFieldInfo>();

            var mergeInfo = agreementInfo.mergeFieldInfo;

            foreach (var mergeField in mergefields)
            {
                AdobeSignatureV6.MergeFieldInfo mergeFieldInfo1 = new AdobeSignatureV6.MergeFieldInfo();
                mergeFieldInfo1.fieldName = mergeField.Key.ToString();
                mergeFieldInfo1.defaultValue = mergeField.Value.ToString();

                mergeInfo.Add(mergeFieldInfo1);
            }
            return mergeInfo;

        }
        #endregion

        #region STATIC METHODS

        /// <summary>
        /// Get the access token using authorization code
        /// </summary>
        /// <param name="apiURL">API Uri</param>
        /// <param name="authorization_code">Authorization Code - the authorization code obtained in Authorization Request process</param>
        /// <param name="clientid">Application ID - obtained from OAuth Configuration page / Identifies the application</param>
        /// <param name="client_secret">Client secret key - obtained from OAuth Configuration page / Authenticates the application</param>
        /// <param name="redirectURL">Redirect URL - must match the value used during the Authorization Code step / This value must belong to the set of values specified on the OAuth Configuration page</param>        
        /// <returns>AccessToken object</returns>
        public static async Task<AdobeSignatureV6.AccessToken> GetAccessToken(string apiURL, string authorization_code, string clientid, string client_secret, string redirectURL)
        {
            RestAPI API = new RestAPI(apiURL, "");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("code", authorization_code);
            parameters.Add("client_id", clientid);
            parameters.Add("client_secret", client_secret);
            parameters.Add("redirect_uri", redirectURL);
            parameters.Add("grant_type", "authorization_code");

            FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(parameters);

            string json = await API.PostRest("/oauth/token", encodedContent, "application/x-www-form-urlencoded");
            return API.DeserializeJSon<AdobeSignatureV6.AccessToken>(json);
        }


        /// <summary>
        /// Get Access Token using refresh token
        /// </summary
        /// <param name="apiURL">API Uri</param>
        /// <param name="refresh_token">Refresh Token, which can be used to get a fresh Access Token</param>
        /// <param name="clientid">Application ID - obtained from OAuth Configuration page / Identifies the application</param>
        /// <param name="client_secret">Client secret key - obtained from OAuth Configuration page / Authenticates the application</param>
        /// <returns>AccessToken object - Refresh_token property would be null on this call.</returns>
        public static async Task<AdobeSignatureV6.AccessToken> GetAccessTokenByRefreshToken(string apiURL, string refresh_token, string clientid, string client_secret)
        {
            RestAPI API = new RestAPI(apiURL, "");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("refresh_token", refresh_token);
            parameters.Add("client_id", clientid);
            parameters.Add("client_secret", client_secret);
            parameters.Add("grant_type", "refresh_token");

            FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(parameters);

            string json = await API.PostRest("/oauth/refresh", encodedContent, "application/x-www-form-urlencoded");
            return API.DeserializeJSon<AdobeSignatureV6.AccessToken>(json);
        }
        
        #endregion STATIC METHODS


    }
}
