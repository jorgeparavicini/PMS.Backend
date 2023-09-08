using PhoneNumbers;

namespace PMS.Backend.Features.Shared.ValueObjects;

internal record Phone
{
    private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

    public string Value { get; }

    public Phone(string value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException("Invalid phone number format.", nameof(value));
        }

        Value = value;
    }

    private static bool IsValid(string phone)
    {
        try
        {
            PhoneNumber number = PhoneNumberUtil.Parse(phone, null);
            return PhoneNumberUtil.IsValidNumber(number);
        }
        catch
        {
            return false;
        }
    }
    
    private Phone() : this(string.Empty)
    {
    }
}
