using System;

namespace Kitpymes.Core.Shared.Tests
{
    [Serializable]
    public class FakeUser
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public int Age { get; set; }

        public string[]? Permissions { get; set; }
    }
}
