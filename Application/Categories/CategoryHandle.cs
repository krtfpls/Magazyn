using Data;
using Entities;

namespace Application.Categories
{
    public class CategoryHandle
    {
        public static Category PrepareCategory(string name, DataContext context)
        {
            string trimmedName = name.Trim().ToLower();

            if (trimmedName == string.Empty)
                return new Category {Name= "niezdefiniowana"};
            
            Category category = context.Categories.FirstOrDefault(x => x.Name == trimmedName);

            if (category == null)
            {
                category = new Category { Name = trimmedName };
            }

            return category;
        }
    }
}