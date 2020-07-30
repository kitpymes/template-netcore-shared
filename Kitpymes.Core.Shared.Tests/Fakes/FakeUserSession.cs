using System.Collections.Generic;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeUserSession 
    {
        private const string _key = "66DEC5B5-D04B-4BA7-8AAF-46E99B5F4E9B";
        public static string Key => _key;

        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public IEnumerable<string>? Permissions { get; set; }
    }
}
