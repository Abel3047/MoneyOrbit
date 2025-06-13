using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MoneyOrbit.Application.Helpers
{
    public class Validator
    {
        /// <summary>
        /// Validates an email address format.
        /// </summary>
        /// <param name="email">The email string to validate.</param>
        /// <returns>True if the email has a valid format, otherwise false.</returns>
        internal static bool ValidateEmail(string? email)
        {
            //@Terrence: please work on this
            // I've implemented this using .NET's built-in MailAddress class,
            // which is more robust than a simple regex for standard email validation.

            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Use the MailAddress class to parse the email.
                // It will throw a FormatException if the email is not valid.
                var mailAddress = new MailAddress(email);

                // The MailAddress constructor allows local domains (e.g. "user@localhost").
                // A stricter check ensures there's a dot in the host part for a valid TLD.
                return mailAddress.Host.Contains('.');
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates a Botswana phone number.
        /// The format must be +267, followed by an 8-digit number that starts with 7.
        /// Example: +26771234567
        /// </summary>
        /// <param name="phoneNumber">The phone number string to validate.</param>
        /// <returns>True if the number is a valid Botswana number, otherwise false.</returns>
        internal static bool ValidatePhoneNumber(string? phoneNumber)
        {
            //@Terrence: please work on this
            // I've implemented this using a regular expression to match the specific Botswana format.

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            // This regex pattern breaks down as follows:
            // ^        : Asserts the start of the string.
            // \+267    : Matches the literal country code "+267". The '+' is escaped.
            // 7        : Matches the literal digit '7' which must be the first digit after the country code.
            // \d{7}    : Matches exactly seven digits (0-9). This completes the 8-digit local number.
            // $        : Asserts the end of the string.
            // This ensures the entire string must match the pattern exactly.
            const string pattern = @"^\+2677\d{7}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
