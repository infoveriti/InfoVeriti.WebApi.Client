using System.Runtime.CompilerServices;

namespace InfoVeriti.WebApi.Extensions;

public static class BytesExteinsions
{
	public static string? ToHex( this byte[]? src, bool upperCase = false )
	{
		if ( src is null )
			return null;

		if ( src.Length <= 0 )
			return string.Empty;

		Span<char> dst = new char[src.Length * 2];
		Func<int, char> toChar = upperCase ? ToCharUpper : ToCharLower;
		int i = 0, j = 0;

		byte b = src[ i++ ];
		dst[ j++ ] = toChar( b >> 4 );
		dst[ j++ ] = toChar( b );

		while ( i < src.Length )
		{
			b = src[ i++ ];
			dst[ j++ ] = toChar( b >> 4 );
			dst[ j++ ] = toChar( b );
		}

		return dst.ToString();
	}
	
	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	private static char ToCharLower( int value )
	{
		value &= 0xF;
		value += '0';

		if ( value > '9' )
		{
			value += ('a' - ('9' + 1));
		}

		return (char)value;
	}

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	private static char ToCharUpper( int value )
	{
		value &= 0xF;
		value += '0';

		if ( value > '9' )
		{
			value += ('A' - ('9' + 1));
		}

		return (char)value;
	}


	public static string? ToBase64String( this byte[]? bytes )
	{
		if ( bytes is null )
			return null;

		if ( bytes.Length <= 0 )
			return string.Empty;

		return Convert.ToBase64String( bytes, Base64FormattingOptions.None );
	}


}