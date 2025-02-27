using System.Text;

namespace InfoVeriti.WebApi.Client.Helpers;

internal class ConsolePasswordReader
{
	internal static string ReadPassword()
	{
		StringBuilder password = new StringBuilder();
		ConsoleKeyInfo key;

		do
		{
			key = Console.ReadKey(intercept: true);
			if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Escape)
			{
				password.Append(key.KeyChar);
				Console.Write("*");
			}
			else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
			{
				Console.Write("\b \b");
			}
		} while (key.Key != ConsoleKey.Enter);

		Console.WriteLine();
		return password.ToString();
	}
}