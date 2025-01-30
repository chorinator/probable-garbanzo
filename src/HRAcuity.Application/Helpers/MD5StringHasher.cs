using System.Security.Cryptography;
using System.Text;

namespace HRAcuity.Application.Helpers;

// From: https://stackoverflow.com/a/24031467
public class Md5StringHasher : IStringHasher
{
    public string Hash(string value)
    {
        var inputBytes = Encoding.UTF8.GetBytes(value);
        var hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}