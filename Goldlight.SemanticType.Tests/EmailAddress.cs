using Goldlight.SemanticTypes;
using System;
using System.Runtime.Serialization;

namespace Goldlight.SemanticType.Tests
{
    [Serializable]
    public class EmailAddress : SemanticType<EmailAddress, string>
    {
        public EmailAddress() : base()
        {
        }
        public EmailAddress(SerializationInfo info, StreamingContext context) : base(info, context) { }
        //protected EmailAddress(SerializationInfo info, StreamingContext context) : base(info, context) { }
        protected override void SetValidation(Func<string, bool> validator, string validationText)
        {
            base.SetValidation(t => !string.IsNullOrWhiteSpace(t), "The email address cannot be empty");
        }
    }
}
