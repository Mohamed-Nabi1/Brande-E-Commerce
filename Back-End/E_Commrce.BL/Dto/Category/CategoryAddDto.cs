using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL;

public class CategoryAddDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = true)]
    public Guid? ParentCategoryId { get; set; } = Guid.Empty;
    public string? Image { get; set; } = string.Empty;

}
