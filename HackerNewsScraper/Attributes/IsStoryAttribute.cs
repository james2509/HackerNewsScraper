using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HackerNewsScraper.Attributes
{
    /// <summary>
    /// Custom Validation attribute to validate if the type of post is a story. The get latest posts api includes job so this validation
    /// allows filtering out unwanted types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IsStoryAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return string.Compare("story", value.ToString(), StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}
