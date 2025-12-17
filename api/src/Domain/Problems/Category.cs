using Domain.Common;

namespace Domain.Problems;

public class CategoryConstants
{
    public const string Roads = "Дороги";
    public const string Lighting = "Освітлення";
    public const string Garbage = "Сміття";
    public const string Water = "Водопостачання";
    public const string PublicTransport = "Громадський транспорт";
    public const string Parks = "Парки та зелені зони";
    public const string Safety = "Безпека";
    public const string Noise = "Шум";
    public const string Animals = "Тварини";
    public const string Other = "Інше";
}

public class Category : ValueObject
{
    public string Value { get; }

    private Category(string value)
    {
        Value = value;
    }

    public static Category From(string value)
    {
        var category = new Category(value);

        if (!SupportedCategories.Contains(category))
        {
            throw new UnsupportedCategoryException(value);
        }

        return category;
    }

    public static Category Roads => new(CategoryConstants.Roads);
    public static Category Lighting => new(CategoryConstants.Lighting);
    public static Category Garbage => new(CategoryConstants.Garbage);
    public static Category Water => new(CategoryConstants.Water);
    public static Category PublicTransport => new(CategoryConstants.PublicTransport);
    public static Category Parks => new(CategoryConstants.Parks);
    public static Category Safety => new(CategoryConstants.Safety);
    public static Category Noise => new(CategoryConstants.Noise);
    public static Category Animals => new(CategoryConstants.Animals);
    public static Category Other => new(CategoryConstants.Other);

    public static implicit operator string(Category category)
    {
        return category.ToString();
    }

    public static explicit operator Category(string value)
    {
        return From(value);
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static IEnumerable<Category> SupportedCategories
    {
        get
        {
            yield return Roads;
            yield return Lighting;
            yield return Garbage;
            yield return Water;
            yield return PublicTransport;
            yield return Parks;
            yield return Safety;
            yield return Noise;
            yield return Animals;
            yield return Other;
        }
    }

    public static IEnumerable<string> AllCategoryNames => SupportedCategories.Select(c => c.Value);
}

public class UnsupportedCategoryException : Exception
{
    public UnsupportedCategoryException(string category) 
        : base($"Category '{category}' is not supported.") { }
}