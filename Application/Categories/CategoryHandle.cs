using Data;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories
{
    public class CategoryHandle
    {
        public Category category { get; private set; }
        public bool isNew { get; private set; }
        private readonly string _name;
        private DataContext _context;

        public CategoryHandle(string name, DataContext context)
        {
            this._context = context;
            this._name = name.Trim().ToLower();
            PrepareCategory();
        }

        private void PrepareCategory()
        {
            category = _context.Categories
                    .FirstOrDefault(x =>
                        x.Name == _name);

            if (category == null)
                {
                    category = new Category { Name= _name};
                    isNew= true;
                }
            else{
                isNew= false;
            }
        }


    }
}