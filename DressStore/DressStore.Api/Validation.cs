using DressStore.Api.Resources;

namespace DressStore.Api
{
    public class Validation
    {
        public static bool IsValidName(string str, out string message)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                message = Resource.NameRequired;
                return false;
            }
            if (str.ToLower().StartsWith("ğ"))
            {
                message = Resource.NameCannotStartsWithGh; 
                return false;
            }
            if (!char.IsUpper(str[0]))
            {
                message = Resource.NameMustStartWithUppercase;
                return false;
            }
            message = "";
            return true;
        }
    }
}
