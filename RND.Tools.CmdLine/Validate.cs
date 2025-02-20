using System.CommandLine.Parsing;

namespace RND.Tools.CmdLine;

internal static class Validate
{
	public static void IsNotEmpty(OptionResult result)
	{
		if (string.IsNullOrEmpty(result.GetValueForOption(result.Option)?.ToString()))
		{
			result.ErrorMessage = $"Option '{result.Option.Name}' should not be empty";
		}
	}

	public static void IsNotEmpty(ArgumentResult result)
	{
		var value = result.Tokens.SingleOrDefault()?.Value;

		if (string.IsNullOrEmpty(value))
		{
			result.ErrorMessage = $"Argument '{result.Argument.Name}' should not be empty";
		}
	}
}
