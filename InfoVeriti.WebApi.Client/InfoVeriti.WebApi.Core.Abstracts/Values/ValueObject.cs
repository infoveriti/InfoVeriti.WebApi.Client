namespace InfoVeriti.WebApi.Core.Abstracts.Values;

public abstract class ValueObject<T> : IValueObject<T>
{
	public abstract string StringValue { get; }

	
	public abstract bool IsSameAs( T other );

	public bool Equals( T? other )
	{
		if ( other is null )
			return false;

		return IsSameAs( other );
	}
	
	public override bool Equals( object? obj )
	{
		if ( obj is null ) return false;
		if ( ReferenceEquals( this, obj ) ) return true;
		if ( obj is not T ) return false;
		return IsSameAs( (T)obj );
	}

	public override string ToString() => StringValue;
	public virtual void Validate( Exception exception )
	{
	}

	public abstract int HashCode();

	public override int GetHashCode() => HashCode();
}