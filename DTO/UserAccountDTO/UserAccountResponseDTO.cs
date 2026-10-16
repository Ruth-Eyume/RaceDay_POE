namespace RaceDay.API.DTOs.UserAccountDTOs
{
    public class UserAccountResponseDTO
    {
        public int UserAccountID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}

