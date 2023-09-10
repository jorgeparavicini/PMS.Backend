using System.Net.Mail;

namespace PMS.Backend.Features.Shared.ValueObjects;

internal record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }

        Value = value;
    }

    public static Email? FromString(string? email) => string.IsNullOrWhiteSpace(email) ? null : new Email(email);


    private static bool IsValid(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private Email() : this(string.Empty)
    {
    }
}
