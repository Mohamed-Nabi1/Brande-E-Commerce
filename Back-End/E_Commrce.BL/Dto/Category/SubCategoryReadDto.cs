using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL;

public class SubCategoryReadDto
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<ProductDetailsReadDto>? products { get; set; }




}
