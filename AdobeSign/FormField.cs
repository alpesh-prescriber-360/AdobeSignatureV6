using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AdobeSignatureV6
{
    [DataContract]
    public class FormField
    {
        [DataMember(EmitDefaultValue = false)]
        public List<FormFieldLocation> locations { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string alignment { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string assignee { get; set; } //Valid values are a participant set id, null, "NOBODY" or "PREFILL"

        [DataMember(EmitDefaultValue = false)]
        public string backgroundColor { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string borderColor { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string borderStyle { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string borderWidth { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Boolean calculated { get; set; } //true if this field's value is calculated from an expression, else false,

        [DataMember(EmitDefaultValue = false)]
        public List<FormFieldConditionalAction> conditionalAction { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string contentType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string defaultValue { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string displayFormat { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string displayFormatType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string displayLabel { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string fontColor { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string fontName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string fontSize { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public List<string> hiddenOptions { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public FormFieldHyperLink hyperlink { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string inputType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool masked { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string maskingText { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int maxLength { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int maxValue { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int minLength { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int minValue { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string origin { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string radioCheckType { get; set; }        

        [DataMember(EmitDefaultValue = false)]
        public bool readOnly { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool required { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string tooltip { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool urlOverridable { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string validation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string validationData { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string validationErrMsg { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string valueExpression { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool visible { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> visibleOptions { get; set; }
    }

    [DataContract]
    public class FormFieldLocation
    {
        [DataMember(EmitDefaultValue = false)]
        public int height { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int left { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string pageNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int top { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int width { get; set; }
    }

    [DataContract]
    public class Condition
    {
        [DataMember(EmitDefaultValue = false)]
        public string value { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string fieldLocationIndex { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string fieldName { get; set; }
        
    }

    [DataContract]
    public class FormFieldConditionalAction
    {
        [DataMember(EmitDefaultValue = false)]
        public string action { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string anyOrAll { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string predicates { get; set; }

    }

    [DataContract]
    public class FormFieldConditionPredicate
    {
        [DataMember(EmitDefaultValue = false)]
        public int fieldLocationIndex { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string fieldName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string @operator { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string value { get; set; }
    }

    [DataContract]
    public class FormFieldHyperLink
    {
        [DataMember(EmitDefaultValue = false)]
        public FormFieldLocation documentLocation { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public string linkType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string url { get; set; }
    }

    [DataContract]
    public class FormFieldPostInfo 
    {
        [DataMember(EmitDefaultValue = false)]
        public string templateId { get; set; }   
    }

    [DataContract]
    public class FormFieldPutInfo
    {
        [DataMember(EmitDefaultValue = false)]
        public List<FormField> fields{ get; set; } //The list of fields to update or replace.PDF_SIGNATURE inputType field is currently not supported.
    }

}
