using System.Net;
using InfoVeriti.Libs.Network.Exceptions;
using InfoVeriti.WebApi.Contracts;

namespace InfoVeriti.WebApi.Facade.Extensions;

public static class ExceptionsExtensions
{
		public static string GetHttpStatusMessage( this Exception error )
		{
			if ( error is HttpStatusCodeException )
				return $"{((error as HttpStatusCodeException)?.BodyData as ErrorApiResponse)?.Error} (code {(int?)(error as HttpStatusCodeException)?.StatusCode ?? -1}: {Enum.GetName( (error as HttpStatusCodeException)?.StatusCode ?? HttpStatusCode.NotImplemented )})";

			return error.Message;
		}

}