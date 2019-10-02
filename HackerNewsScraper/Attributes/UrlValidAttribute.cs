using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HackerNewsScraper.Attributes
{
    /// <summary>
    /// Custom validation attribute to validate the the Uri is rfc3986 compliant.
    /// Using the IsWellFormedUriString function validates the Uri is rfc3986 compliant.
    /// https://docs.microsoft.com/en-us/dotnet/api/system.uri.iswellformeduristring?view=netcore-3.0
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UrlValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Uri.IsWellFormedUriString(value.ToString(), UriKind.Absolute);
        }
    }
}
