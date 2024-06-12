namespace AspMedSystem.API
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public JWTSettings JWTSettings { get; set; }
    }
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
