using Domain.Problems;

namespace Domain.Categories;

public class Category
{
    public CategoryId Id { get; }
    public string Name { get; private set; }
    public ICollection<Problem> Problems { get; private set; } = new List<Problem>();

    private Category(CategoryId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Category New(CategoryId id, string name)
    {
        return new Category(id, name);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}