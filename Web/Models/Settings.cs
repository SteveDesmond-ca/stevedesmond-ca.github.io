namespace Web.Models
{
    public static class Settings
    {
        public const string Title = "Steve Desmond Software Development";
        public const string Domain = "https://stevedesmond.ca";
        public const string CDN = "https://d24kn0csv98dxo.cloudfront.net";
        public const string Location = "Ithaca, NY";
        public const string Description = "Web / app developer and software consultant based in " + Location + "; working remotely with businesses everywhere";
        public const string ImageBase = CDN + "/images";
        public const string ProfileImage = ImageBase + "/steve-0300.jpg";
        public const string ConnectionString = @"Data Source=/db/db.sqlite";
        public const string EmailFromAndTo = "hi@stevedesmond.ca";
        public const string EmailServer = "smtp.googlemail.com";
        public const string EmailUser = "steve@vtsv.ca";
        public const string EmailSubject = "Contact Form Submission";

        public const string OrgJSON =
        @"{
            ""@context"": ""http://schema.org"",
            ""@type"": ""Organization"",
            ""@id"": """ + Domain + @""",
            ""name"": """ + Title + @""",
            ""url"": """ + Domain + @""",
            ""image"": " + ProfileImageJSON + @",
            ""logo"": " + ProfileImageJSON + @",
            ""description"": """ + Description + @""",
            ""mainEntityOfPage"": """ + Domain + @""",
            ""email"": """ + EmailFromAndTo + @""",
            ""legalName"": """ + Title + @""",
            ""location"": {
                ""@context"": ""http://schema.org"",
                ""@type"": ""PostalAddress"",
                ""addressLocality"": ""Ithaca"",
                ""addressRegion"": ""NY"",
                ""addressCountry"": ""US""
            }
        }";

        public const string ProfileImageJSON =
        @"{
            ""@context"": ""http://schema.org"",
            ""@type"": ""ImageObject"",
            ""url"": ""https://stevedesmond.ca/images/steve-0300.jpg"",
            ""height"": ""300"",
            ""width"": ""300""
        }";
    }
}