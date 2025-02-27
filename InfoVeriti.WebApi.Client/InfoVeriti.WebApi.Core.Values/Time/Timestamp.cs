using System.Globalization;
using InfoVeriti.WebApi.Core.Abstracts.Values;

namespace InfoVeriti.WebApi.Core.Values.Time;

public class Timestamp : ValueObject<Timestamp>, IComparable<Timestamp>, IComparable, IFormattable
{
	internal const string DefaultFormat = "0.000000";
	internal const decimal EmptyValue = decimal.MinValue;
	internal const decimal ZeroValue = decimal.Zero;


	private static readonly Lazy<Timestamp> LazyZero = new(() => new Timestamp( ZeroValue ), true);
	private static readonly Lazy<Timestamp> LazyEmpty = new(() => new Timestamp( EmptyValue ), true);

	public static Timestamp Now => new(DateTimeOffset.UtcNow);
	public static Timestamp Zero => LazyZero.Value;
	public static Timestamp Empty => LazyEmpty.Value;


	public bool IsEmpty => Value == EmptyValue;
	public bool IsZero => Value == ZeroValue;

	public decimal Value { get; }

	public long UnixTimeSeconds => (long)Value;

	public long UnixTimeMilliseconds => (long)(Value * 1_000);

	public long UnixTimeMicroseconds => (long)(Value * 1_000_000);
	public decimal FractionalPart => Value - Math.Truncate( Value );
	public long FractionalPartMicroseconds => (long)(FractionalPart * 1_000_000);


#region Ctor

	public Timestamp( TimeSpan timeSpan ) : this( (decimal)timeSpan.TotalMicroseconds / 1_000_000m )
	{
	}

	public Timestamp( DateTime dateTime ) : this( new DateTimeOffset( dateTime.ToUniversalTime() ) )
	{
	}

	public Timestamp( DateTimeOffset dateTime ) : this( dateTime.ToUnixTimeSeconds() + dateTime.Millisecond / 1_000m + dateTime.Microsecond / 1_000_000m )
	{
	}

	public Timestamp( decimal? ts )
	{
		Value = Math.Round( ts ?? EmptyValue, 6 );
	}

	public Timestamp( double? ts ) : this( ts != null ? Math.Round( (decimal)ts, 6 ) : null )
	{
	}
	
	public Timestamp( long? ts )
	{
		Value = ts ?? EmptyValue;
	}


#endregion

#region Help methods

	public DateTime ToDateTime() => ToDateTimeOffset().DateTime;
	public DateTimeOffset ToDateTimeOffset() => DateTimeOffset.FromUnixTimeSeconds( UnixTimeSeconds ).AddMicroseconds( FractionalPartMicroseconds ).ToOffset( Time.DefaultOffset );

	public TimeSpan ToTimeSpan() => TimeSpan.FromSeconds( UnixTimeSeconds ).Add( TimeSpan.FromMicroseconds( FractionalPartMicroseconds ) );

	public Timestamp AddYear( bool isLeapYaer ) => isLeapYaer ? AddYear366( 1m ) : AddYear365( 1m );

	public Timestamp AddYear365( decimal years ) => AddDays( 365m * years );

	public Timestamp AddYear366( decimal years ) => AddDays( 366m * years );

	public Timestamp AddQuarter90( decimal quarters ) => AddDays( 90m * quarters );

	public Timestamp AddMonths30( decimal months ) => AddDays( 30m * months );

	public Timestamp AddMonths31( decimal months ) => AddDays( 31m * months );

	public Timestamp AddMonth( MonthEnum month, bool isLeapYear ) => AddDays( month.GetDaysInMonth( isLeapYear ) );

	public Timestamp AddWeeks( decimal weeks ) => AddDays( 7m * weeks );

	public Timestamp AddDays( decimal days ) => AddHours( 24m * days );

	public Timestamp AddHours( decimal hours ) => AddMinutes( 60m * hours );
	public Timestamp AddMinutes( decimal minutes ) => AddSeconds( 60m * minutes );

	public Timestamp AddSeconds( decimal seconds ) => new(Value + seconds);

	public Timestamp AddMiliseconds( decimal miliseconds ) => new(Value + miliseconds / 1_000m);

	public Timestamp AddMicroseconds( decimal microseconds ) => new(Value + microseconds / 1_000_000m);

#endregion

#region Others Opertators

	// decimal
	public static implicit operator decimal?( Timestamp? ts ) => ts?.Value;

	public static implicit operator Timestamp?( decimal? ts ) => ts is null ? null : new Timestamp( ts );

	public static implicit operator decimal( Timestamp ts ) => ts.Value;

	public static implicit operator Timestamp( decimal ts ) => new(ts);

	// double
	public static implicit operator double?( Timestamp? ts ) => (double?)ts?.Value;

	public static implicit operator Timestamp?( double? ts ) => ts is null ? null : new Timestamp( ts );

	public static implicit operator double( Timestamp ts ) => (double)ts.Value;

	public static implicit operator Timestamp( double ts ) => new( ts );
	
	
	// long
	public static implicit operator long?( Timestamp? ts ) => ts?.UnixTimeSeconds;

	public static implicit operator Timestamp?( long? ts ) => ts is null ? null : new Timestamp( ts );

	public static implicit operator long( Timestamp ts ) => ts.UnixTimeSeconds;

	public static implicit operator Timestamp( long ts ) => new( ts );
	

	// DateTime
	public static implicit operator Timestamp?( DateTime? dateTime ) => dateTime != null ? new(dateTime.Value) : null;

	public static implicit operator DateTime?( Timestamp? ts ) => ts != null ? ts.ToDateTime() : null;

	public static implicit operator Timestamp( DateTime dateTime ) => new(dateTime);

	public static implicit operator DateTime( Timestamp ts ) => ts.ToDateTime();


	// DateTimeOffset
	public static implicit operator Timestamp?( DateTimeOffset? dateTime ) => dateTime != null ? new(dateTime.Value) : null;

	public static implicit operator DateTimeOffset?( Timestamp? ts ) => ts != null ? ts.ToDateTimeOffset() : null;

	public static implicit operator Timestamp( DateTimeOffset dateTime ) => new(dateTime);

	public static implicit operator DateTimeOffset( Timestamp ts ) => ts.ToDateTimeOffset();

	
	// TimeSpan
	public static implicit operator Timestamp?( TimeSpan? timeSpan ) => timeSpan != null ? new(timeSpan.Value) : null;

	public static implicit operator TimeSpan?( Timestamp? ts ) => ts != null ? ts.ToTimeSpan() : null;

	public static implicit operator Timestamp( TimeSpan timeSpan ) => new(timeSpan);

	public static implicit operator TimeSpan( Timestamp ts ) => ts.ToTimeSpan();

	
	public int CompareTo( Timestamp? other )
	{
		if ( ReferenceEquals( this, other ) ) return 0;
		if ( ReferenceEquals( null, other ) ) return 1;
		return Value.CompareTo( other.Value );
	}

	public int CompareTo( object? obj )
	{
		if ( ReferenceEquals( null, obj ) ) return 1;
		if ( ReferenceEquals( this, obj ) ) return 0;
		return obj is Timestamp other ? CompareTo( other ) : throw new ArgumentException( $"Object must be of type {nameof( Timestamp )}" );
	}

	public static bool operator <( Timestamp? left, Timestamp? right )
	{
		return Comparer<Timestamp>.Default.Compare( left, right ) < 0;
	}

	public static bool operator >( Timestamp? left, Timestamp? right )
	{
		return Comparer<Timestamp>.Default.Compare( left, right ) > 0;
	}

	public static bool operator <=( Timestamp? left, Timestamp? right )
	{
		return Comparer<Timestamp>.Default.Compare( left, right ) <= 0;
	}

	public static bool operator >=( Timestamp? left, Timestamp? right )
	{
		return Comparer<Timestamp>.Default.Compare( left, right ) >= 0;
	}

	public static Timestamp operator +( Timestamp ts, TimeSpan timeSpan )
	{
		return new Timestamp( ts.ToDateTimeOffset().Add( timeSpan ) );
	}

	public static Timestamp operator -( Timestamp ts, TimeSpan timeSpan )
	{
		return new Timestamp( ts.ToDateTimeOffset().Add( -timeSpan ) );
	}

	public static Timestamp operator +( Timestamp ts, Timestamp ts2 )
	{
		return new Timestamp( ts.Value + ts2.Value );
	}

	public static Timestamp operator -( Timestamp ts, Timestamp ts2 )
	{
		return new Timestamp( ts.Value - ts2.Value );
	}

#endregion

#region Format

	public override string ToString()
		=> StringValue;

	public string ToString( string? format, IFormatProvider? formatProvider )
		=> Value.ToString( string.IsNullOrWhiteSpace( format ) ? DefaultFormat : format, formatProvider );

	public string ToString( IFormatProvider? formatProvider )
		=> Value.ToString( DefaultFormat, formatProvider );

#endregion

#region Override methods

	public override bool IsSameAs( Timestamp other )
		=> other.Value == Value;

	public override int HashCode()
		=> Value.GetHashCode();

	public override string StringValue => Value.ToString( DefaultFormat, CultureInfo.InvariantCulture );

#endregion
}

public static class TimestampExtensions
{
	public static Timestamp ToTimestamp( this DateTimeOffset dateTime )
		=> new(dateTime);

	public static Timestamp ToTimestamp( this DateTime dateTime )
		=> new(dateTime);


	public static Timestamp ToTimestamp( this decimal ts )
		=> new(ts);

	public static Timestamp ToTimestamp( this double ts )
		=> new(ts);
	
	public static Timestamp ToTimestamp( this long ts )
		=> new(ts);
	

	public static Timestamp ToTimestamp( this TimeSpan timeSpan )
		=> new(timeSpan);
}