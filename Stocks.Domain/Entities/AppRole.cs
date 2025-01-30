using Microsoft.AspNetCore.Identity;

namespace Stocks.Domain.Entities;

public class AppRole : IdentityRole<Guid>
{
    private string name = string.Empty;
    public override string? Name
    {
        get => name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Role name cannot be null or empty");
            name = value;
        }
    }

    public AppRole(string name)
        : base()
    {
        Name = name;
    }
}