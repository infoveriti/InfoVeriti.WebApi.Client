using System.Security.Cryptography;
using System.Text;

namespace InfoVeriti.WebApi.Extensions;

public static class StringExtensions
{
	public static string? ToMd5(this string? s) => s.ToMd5((Encoding) new UTF8Encoding(false));

	public static string? ToMd5(this string? s, Encoding encoding)
	{
		return s == null ? null : MD5.Create().ComputeHash( encoding.GetBytes( s ) ).ToHex() ?? string.Empty;
	}
	
	
	
}