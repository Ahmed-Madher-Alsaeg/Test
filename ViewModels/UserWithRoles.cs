namespace AlphaBeratung.ViewModels
{
    public class UserWithRoles
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string RoleName { get; set; } = String.Empty;
    }
}
