# Goldlight.SemanticType

With .NET, we are used to the idea that we have type safety, so 
when we declare a variable as a string, we know that we have 
certain operations available t o us on that type. This takes us so far,
but is lacking in that it does not actually provide any further
constraints so while we could call a variable emailAddress, for instance,
this doesn't guarantee that the underlying string actually was an
email address.
```csharp
private string emailAddress = "test";
public void SendEmail()
{
  EmailClient client = new EmailClient();
  client.To = emailAddress;
  client.From = "tony";
  client.Send("This is a simple message");
}
```
What we see with this code is that it would compile but it will
fail to send the email because both the from and the to email
address are invalid.

What we need to do is to add a level of safety on top of a strong
type to ensure that it has semantic safety as well. This is where semantic
types come in. The idea behind a semantic type is that, when
we need to use an email type for instance, that we actually
use an email type. If we take the example above, we would rewrite
this like so.
```csharp
private EmailAddress emailAddress = EmailAddress.TryCreate("test@goldlight.com");
public void SendEmail()
{
  EmailClient client = new EmailClient();
  client.To = emailAddress;
  client.From = EmailAddress.TryCreate("info@goldlight.com");
  client.Send();
}
```
We can see that this is much clearer. Our `From` and `To` are
constrained so that they can only accept the EmailAddress type. This
also serves to make our code clearer because the From and To types
tell us that they must accept EmailAddress. This makes our code less
ambiguous.
## Using Goldlight.SemanticType
