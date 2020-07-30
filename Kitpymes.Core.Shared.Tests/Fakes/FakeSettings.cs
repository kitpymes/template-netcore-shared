namespace Kitpymes.Core.Shared.Tests
{
    public class FakeSettings
    {
        public FacebookSettings FacebookSettings { get; set; } = new FacebookSettings();
        public GoogleSettings GoogleSettings { get; set; } = new GoogleSettings();
        public LinkedinSettings LinkedinSettings { get; set; } = new LinkedinSettings();
    }

    public class FacebookSettings
    {
        public bool Enabled { get; set; }
        public string? Key { get; set; }
    }

    public class GoogleSettings : FacebookSettings
    {
       
    }

    public class LinkedinSettings : FacebookSettings
    {
        
    }
}
