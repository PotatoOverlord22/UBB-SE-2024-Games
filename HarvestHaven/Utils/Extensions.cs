namespace HarvestHaven.Utils
{
    public static class Extensions
    {
        // Extension method to convert enum to string.
        public static string ToEnumString<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return enumValue.ToString();
        }

        // Extension method to convert string to enum.
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}