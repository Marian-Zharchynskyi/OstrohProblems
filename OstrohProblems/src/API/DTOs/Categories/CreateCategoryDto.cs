using Domain.Categories;

namespace API.DTOs.Categories;

public record CreateCategoryDto(Guid? Id, string Name)
{
    // public static CreateCategoryDto FromDomainModel(Category category)
    //     => new(
    //         category.Name
    //     );
}