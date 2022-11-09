namespace PersonnelManagement.Infrastracture.Identity
{
    public class JwtSettingsOptions
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifeTime { get; set; }
    }
}
