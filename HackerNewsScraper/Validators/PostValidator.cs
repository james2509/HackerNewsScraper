using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HackerNewsScraper.Models;

namespace HackerNewsScraper.Validators
{
    /// <summary>
    /// Provides functionality to Validate a Post object.
    /// This takes advantage of the DataAnnotations Validation functionality which takes a Post object
    /// and checks it's validity using the DataAnnotation attributes on the Post class
    /// </summary>
    public static class PostValidator
    {
        /// <summary>
        /// Triggers the validation on a provided post object
        /// </summary>
        public static bool Validate(Post post)
        {
            var validatonResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                post,
                new ValidationContext(post, null, null),
                validatonResults,
                true);

            return isValid;
        }
    }
}
