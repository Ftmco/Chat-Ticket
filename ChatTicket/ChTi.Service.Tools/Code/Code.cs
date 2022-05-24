using ChTi.Service.Tools.Crypto;

namespace Identity.Service.Tools.Code;

public static class CodeExtension
{
    public static string CreateCode(this int length)
    {
        string? token = length.CreateToken();
        return token.GetHashCode().ToString();
    }

    public static string CreateCode(this short length)
        => ((int)length).CreateCode();

    public static string CreateToken(this int length)
    {
        string newString = Guid.NewGuid().ToString().Replace("-", "");
        while (newString.Length < length)
            newString += Guid.NewGuid().ToString().Replace("-", "");
        return newString[0..length].CreateSHA256();
    }
}