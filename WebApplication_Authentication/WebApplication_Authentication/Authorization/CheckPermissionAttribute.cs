using WebApplication_Authentication.Data;

namespace WebApplication_Authentication.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckPermissionAttribute: Attribute
    {
        public Permission Permission { get; }

        public CheckPermissionAttribute(Permission permission)
        {
            Permission = permission;
        }
    }
}
