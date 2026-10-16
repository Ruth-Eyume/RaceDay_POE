namespace RaceDay.API.DTOs.CategoryDTOs
{
    public class CategoryResponseDTO
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}