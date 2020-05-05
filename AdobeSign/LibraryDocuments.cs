using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdobeSignatureV6
{
    #region Library Document Creation

    [DataContract]
    public class LibraryDocumentInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<FileInfo> fileInfos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string sharingMode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string state { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> templateTypes { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string createdDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string creatorEmail { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string creatorName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string groupId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool isDocumentRetentionApplied { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string modifiedDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; }

        
        public LibraryDocumentInfo()
        {

        }

    }

    [DataContract]
    public class LibraryDocumentCreationResponse 
    {
        
        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; } //The unique identifier that is used to refer to the library template
    }

    #endregion Library Document Creation

    #region Library Documents List

    [DataContract]
    public class LibraryDocuments
    {
        [DataMember(EmitDefaultValue = false)]
        public List<LibraryDocument> libraryDocumentList { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PageInfo page { get; set; }

    }

    [DataContract]
    public class LibraryDocument
    {
        [DataMember(EmitDefaultValue = false)]
        public string hidden { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string modifiedDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string sharingMode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> templateTypes { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string creatorEmail { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string groupId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string status { get; set; }

    }

    #endregion Library Documents List

    #region LibraryDocument => Documents

    [DataContract]
    public class Documents
    {
        [DataMember(EmitDefaultValue = false)]
        public List<Document> documents { get; set; } //A list of documents
    }

    #endregion LibraryDocument => Documents

    #region LibraryDocument MembersInfo

    [DataContract]
    public class LibraryDocumentMembersInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public SenderInfo creatorInfo { get; set; } //A list of documents
    }

    #endregion LibraryDocument MembersInfo

    #region LibraryDocument state

    [DataContract]
    public class LibraryDocumentStateInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public string state { get; set; } //A list of documents
    }

    #endregion LibraryDocument State

}
