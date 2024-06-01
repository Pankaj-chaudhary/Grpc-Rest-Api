﻿using MyGrpcService.Models;

namespace MyGrpcService.Services
{
    public interface ICategoryService
    {
        Task<Category> AddCategory(Category category);
        Task<Category> EditCategory(Category category);
        IEnumerable<Category> AllCategories();
        Category GetCategoryById(string Id);
        bool DeleteCategoryById(string Id);
    }
}
