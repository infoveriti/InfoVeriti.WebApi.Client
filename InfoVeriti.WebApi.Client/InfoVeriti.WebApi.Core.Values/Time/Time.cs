using System.Globalization;
using InfoVeriti.WebApi.Core.Abstracts.Values;
using InfoVeriti.WebApi.Core.Exceptions;

namespace InfoVeriti.WebApi.Core.Values.Time;

public class Time: ValueObject<Time>
{
	public static TimeSpan DefaultOffset { get; set; } = TimeSpan.Zero;
	
	public DateTimeOffset Value { get; }

	public Time(DateTimeOffset? value)
	{
		Value = Validate( value );
	}
	
	
	// DateTimeOffset
	public static implicit operator DateTimeOffset(Time time)
		=> time.Value;

	public static implicit operator Time( DateTimeOffset value )
		=> new(value);

	public static implicit operator DateTimeOffset?( Time? time )
		=> time is null ? null : time.Value;

	public static implicit operator Time( DateTimeOffset? value )
		=> new(value);

	
	
	//Timestamp
	public static implicit operator Timestamp( Time time ) => new( time.Value );
	public static implicit operator Time( Timestamp time ) => new( time.ToDateTimeOffset() );
	
	
	

	public override bool IsSameAs( Time other )
		=> Value == other.Value;

	public override int HashCode()
		=> Value.GetHashCode();

	public override string StringValue => Value.ToString( "O", CultureInfo.InvariantCulture );

	public override void Validate( Exception exception )
		=> Validate( Value, exception );

	public static DateTimeOffset Validate( DateTimeOffset? value, Exception? exception = null )
	{
		if ( value is null )
			throw exception ?? new DomainException( "Not valid datetime offset" );

		return value.Value.ToOffset( DefaultOffset );
	}
}