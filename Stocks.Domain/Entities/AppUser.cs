using Microsoft.AspNetCore.Identity;

namespace Stocks.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    private string email = string.Empty;
    public override string? Email
    {
        get => email;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be null or empty");

            email = value;
        }
    }

    private string username = string.Empty;
    public override string? UserName
    {
        get => username;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be null or empty");

            username = value;
        }
    }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public AppUser() { }

    public AppUser(string email, string name, string lastName)
        : base()
    {
        Email = email;
        UserName = email;
        SetFirstName(name);
        SetLastName(lastName);
    }

    public void UpdateEmail(string email)
    {
        Email = email;
        UserName = email;
    }

    public void SetFirstName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty");

        FirstName = name;
    }

    public void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty");

        LastName = lastName;
    }
}