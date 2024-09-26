using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;
public class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository, IRecipeUpdateOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext;

    public RecipeRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Recipe recipe) => await _dbContext.Recipes.AddAsync(recipe);

    public async Task Delete(long recipeId)
    {
        var recipe = await _dbContext.Recipes.FindAsync(recipeId);

        _dbContext.Recipes.Remove(recipe!);
    }

    public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
    {
        var query = _dbContext.Recipes
            .AsNoTracking()
            .Include(recipe => recipe.Ingredients)
            .Where(recipe => recipe.Active && recipe.UserId == user.Id);

        if (filters.Difficulties.Any())
        {
            query = query.Where(recipe => recipe.Difficulty.HasValue && filters.Difficulties.Contains(recipe.Difficulty.Value));
        }

        if (filters.CookingTimes.Any())
        {
            query = query.Where(recipe => recipe.CookingTime.HasValue && filters.CookingTimes.Contains(recipe.CookingTime.Value));
        }

        if (filters.DishTypes.Any())
        {
            query = query.Where(recipe => recipe.DishTypes.Any(dishType => filters.DishTypes.Contains(dishType.Type)));
        }

        if (filters.RecipeTitle_Ingredient.NotEmpty())
        {
            query = query.Where(recipe => recipe.Title.Contains(filters.RecipeTitle_Ingredient)
            || recipe.Ingredients.Any(ingredient => ingredient.Item.Contains(filters.RecipeTitle_Ingredient)));
        }

        return await query.ToListAsync();
    }

    async Task<Recipe?> IRecipeReadOnlyRepository.GetById(User user, long recipeId) => await GetFullRecipe(user, recipeId, track: false);

    async Task<Recipe?> IRecipeUpdateOnlyRepository.GetById(User user, long recipeId) => await GetFullRecipe(user, recipeId, track: true);

    public void Update(Recipe recipe) => _dbContext.Recipes.Update(recipe);

    public async Task<IList<Recipe>> GetForDashboard(User user)
    {
        return await _dbContext.Recipes
            .AsNoTracking()
            .Include(recipe => recipe.Ingredients)
            .Where(recipe => recipe.Active && recipe.UserId == user.Id)
            .OrderByDescending(recipe => recipe.CreatedOn)
            .Take(5)
            .ToListAsync();
    }

    private Task<Recipe?> GetFullRecipe(User user, long recipeId, bool track = false)
    {
        var query = _dbContext.Recipes
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Instructions)
            .Include(recipe => recipe.DishTypes)
            .AsQueryable();

        if (track == false)
            query = query.AsNoTracking();

        var result = query.FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);

        return result;
    }
}
