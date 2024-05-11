using Microsoft.AspNetCore.Authorization;

namespace WebApplication_Authentication.Authorization
{
    public class AgeGreaterThan25Requirement: IAuthorizationRequirement
    {
    }
}
