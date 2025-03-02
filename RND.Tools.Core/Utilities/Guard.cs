using System.Diagnostics.CodeAnalysis;

namespace RND.Tools.Core.Utilities
{
    public static class Guard
    {
        public static void AgainstNull([NotNull] object? argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void AgainstNullOrWhiteSpace([NotNull] string? argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException($"Argument '{argumentName}' cannot be null or whitespace.", argumentName);
            }
        }

        // Add more guard methods as needed
    }
}
