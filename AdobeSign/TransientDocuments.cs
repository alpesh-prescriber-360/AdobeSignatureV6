using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdobeSignatureV6
{
    class TransientDocuments
    {
    }

    [DataContract]
    public class TransientDocumentResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public string transientDocumentId { get; set; }
    }
}
