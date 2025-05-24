using Domain.Categories;

namespace API.DTOs.Categories;

public record CreateCategoryDto(string Name)
{
    public static CreateCategoryDto FromDomainModel(Category category)
        => new(
            category.Name
        );
}