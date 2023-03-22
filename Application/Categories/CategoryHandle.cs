using Data;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories
{
    public class CategoryHandle
    {
        public Category Category { get; private set; }
        public bool isNew { get; private set; }
        private readonly string _name;
        private DataContext _context;

        public CategoryHandle(string name, DataContext context)
        {
            this._context = context;
            this._name = name.Trim().ToLower();
            PrepareCategory();
        }

        private async void PrepareCategory()
        {
            Category = await _context.Categories
                    .FirstOrDefaultAsync(x =>
                        x.Name == _name);

            if (Category == null)
                {
                    Category = new Category { Name= _name};
                    isNew= true;
                }
            else{
                isNew= false;
            }
        }


    }
}