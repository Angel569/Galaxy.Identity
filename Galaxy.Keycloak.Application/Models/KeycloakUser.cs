namespace Galaxy.Keycloak.Application.Models
{
    public class KeycloakUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public long CreatedTimestamp { get; set; }
        public bool Enabled { get; set; }
        public bool Totp { get; set; }
        public string[] DisableableCredentialTypes { get; set; }
        public string[] RequiredActions { get; set; }
        public int NotBefore { get; set; }
        public string Password { get; set; }
        public KeycloakUserAccess Access { get; set; }
    }

    public class KeycloakUserAccess
    {
        public bool ManageGroupMembership { get; set; }
        public bool View { get; set; }
        public bool MapRoles { get; set; }
        public bool Impersonate { get; set; }
        public bool Manage { get; set; }
    }
}
