using Newtonsoft.Json;
using SpeakMore.Application.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SpeakMore.Application.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static bool IsValid(this object obj)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            return Validator.TryValidateObject(obj, context, result, true);
        }


        public static void ThrowIfIsNotValid(this object obj)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            var isValid = Validator.TryValidateObject(obj, context, result, true);

            if (!isValid)
            {
                var errorMessage = result.Select(e => e.ErrorMessage).Aggregate((acc, e) => $"{acc} {e}");
                throw new InvalidRequestException($"[{obj.GetType().Name}] {errorMessage}");
            }
        }

    }
}
