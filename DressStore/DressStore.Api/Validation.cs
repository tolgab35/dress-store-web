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

        public static bool IsValidProductName(string name, out string message)
        {
            // Ürün adı zorunlu, en az 3, en fazla 100 karakter olmalı
            if (string.IsNullOrWhiteSpace(name))
            {
                message = Resource.ProductNameRequired;
                return false;
            }
            if (name.Length < 3)
            {
                message = Resource.ProductNameTooShort;
                return false;
            }
            if (name.Length > 100)
            {
                message = Resource.ProductNameTooLong;
                return false;
            }
            message = "";
            return true;
        }

        public static bool IsValidSlug(string slug, out string message)
        {
            // Slug zorunlu, en az 3 karakter, sadece küçük harf, rakam ve tire içerebilir
            if (string.IsNullOrWhiteSpace(slug))
            {
                message = Resource.SlugRequired;
                return false;
            }
            if (slug.Length < 3)
            {
                message = Resource.SlugTooShort;
                return false;
            }
            foreach (char c in slug)
            {
                if (!(char.IsLower(c) || char.IsDigit(c) || c == '-'))
                {
                    message = Resource.SlugFormatInvalid;
                    return false;
                }
            }
            message = "";
            return true;
        }

        public static bool IsValidPrice(decimal price, out string message)
        {
            if (price < 0)
            {
                message = Resource.PriceCannotBeNegative;
                return false;
            }
            if (price == 0)
            {
                message = Resource.PriceCannotBeZero;
                return false;
            }
            message = "";
            return true;
        }

        public static bool IsValidStock(int stock, out string message)
        {
            if (stock < 0)
            {
                message = Resource.StockCannotBeNegative;
                return false;
            }
            message = "";
            return true;
        }

        public static bool IsValidDescription(string description, out string message)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                message = Resource.DescriptionRequired;
                return false;
            }
            if (description.Length < 10)
            {
                message = Resource.DescriptionTooShort;
                return false;
            }
            message = "";
            return true;
        }

        public static bool IsValidUrl(string url, out string message)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                message = Resource.UrlRequired;
                return false;
            }
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                message = Resource.UrlInvalid;
                return false;
            }
            message = "";
            return true;
        }

        public static bool IsValidSku(string sku, out string message)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                message = Resource.SkuRequired;
                return false;
            }
            if (sku.Length < 3)
            {
                message = Resource.SkuTooShort;
                return false;
            }
            message = "";
            return true;
        }
    }
}
