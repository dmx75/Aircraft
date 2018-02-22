namespace App1.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveExtension(this string value)
        {
            var res = value.Replace(".jpeg", string.Empty);
            res = res.Replace(".jpg", string.Empty);
            return res;
        }
    }
}