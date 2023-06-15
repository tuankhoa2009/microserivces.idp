namespace Microservices.IDP.Common
{
    public class PermissionHelper
    {
        public static string GetPermission(string functionCode, string commandCode)
        => string.Join(".", functionCode, commandCode);
    }
}
