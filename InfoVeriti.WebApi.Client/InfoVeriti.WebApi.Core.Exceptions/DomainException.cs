namespace InfoVeriti.WebApi.Core.Exceptions;

public class DomainException : Exception
{
	public DomainException()
	{
	}

	public DomainException(string? message)
		: base(message)
	{
	}

	public DomainException(string? message, Exception innerException)
		: base(message, innerException)
	{
	}

	public DomainException(string? message, IEnumerable<Exception> innerExceptions)
		: base(message, new AggregateException(innerExceptions))
	{
	}
}
