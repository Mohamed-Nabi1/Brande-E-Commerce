using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL;

public class CategoryReadDto
{

    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Parent Category is required")]
    public Guid? ParentCategoryId { get; set; }

    public string? ParentCategoryName { get; set; }
    public string Image { get; set; } = string.Empty;
    public List<ProductReadDto> products { get; set; } = null!;


}
