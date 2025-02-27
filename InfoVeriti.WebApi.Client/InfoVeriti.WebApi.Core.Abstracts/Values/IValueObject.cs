namespace InfoVeriti.WebApi.Core.Abstracts.Values;

public interface IValueObject: IValueValidate
{
	string StringValue { get; }
}

public interface IValueObject<T>: IValueObject, IEquatable<T>
{
}