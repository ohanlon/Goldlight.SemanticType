using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace Goldlight.SemanticType.Tests
{
    public class SemanticTypeTests
    {
        [Fact]
        public void GivenSemanticType_WhenTryCreateCalledWithoutValidation_ThenValueIsRetrieved()
        {
            EmailAddress emailAddress = EmailAddress.TryCreate("bob@bob.com");
            EmailAddress mail = EmailAddress.TryCreate("fred.bbb");
            Assert.Equal("fred.bbb", mail.Value);
            Assert.Equal("bob@bob.com", emailAddress.Value);
        }

        [Fact]
        public void GivenSemanticType_WhenTryCreateCalledWithoutAnEmailAddress_ThenExceptionIsThrown()
        {
            Assert.Throws<ApplicationException>(() => EmailAddress.TryCreate(""));
        }

        [Fact]
        public void GivenSemanticType_WhenTryCreateCalledWithoutAnEmailAddressAndExceptionThrown_Then()
        {
            string message = string.Empty;
            try
            {
                EmailAddress.TryCreate("");
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Assert.Equal("The email address cannot be empty", message);
        }

        [Fact]
        public void GivenEmailAddress_WhenDeserializing_ThenEqualityOfEmail()
        {
            EmailAddress emailAddress = EmailAddress.TryCreate("bob@bob.com");
            using MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, emailAddress);
            stream.Seek(0, SeekOrigin.Begin);
            EmailAddress copyAddress = (EmailAddress) formatter.Deserialize(stream);
            Assert.Equal(emailAddress, copyAddress);
        }

        [Fact]
        public void GivenEmailAddress_WhenDeserializing_ThenEqualityOfEmailUsingEqualityOperator()
        {
            EmailAddress emailAddress = EmailAddress.TryCreate("bob@bob.com");
            using MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, emailAddress);
            stream.Seek(0, SeekOrigin.Begin);
            EmailAddress copyAddress = (EmailAddress)formatter.Deserialize(stream);
            Assert.True(emailAddress == copyAddress);
        }

        [Fact]
        public void GivenEmailAddress_WhenSecondEmailAddressCreatedWithDifferentValue_ThenEqualityReturnsFalse()
        {
            EmailAddress emailAddress = EmailAddress.TryCreate("bob@bob.com");
            EmailAddress copyAddress = EmailAddress.TryCreate("fred@fred.com");
            Assert.True(emailAddress != copyAddress);
        }
    }
}