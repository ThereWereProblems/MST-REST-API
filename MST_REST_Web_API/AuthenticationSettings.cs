namespace MST_REST_Web_API
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssurl { get; set; }
    }
}
