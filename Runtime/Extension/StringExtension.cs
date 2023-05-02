using Services.Utility;

public static class StringExtension
{
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsNullOrSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static string SetColor(this string value, string color)
    {
        return Helper.StringColor(value, color);
    }
}