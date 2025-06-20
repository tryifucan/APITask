namespace APITask_Framework.Utils
{
    public static class Generator
    {
        private static readonly Random _random = new Random();
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => chars[_random.Next(chars.Length)]).ToArray());
        }

        public static int GenerateRandomId()
        {
            return _random.Next(10000, 99999);
        }
    }
}
