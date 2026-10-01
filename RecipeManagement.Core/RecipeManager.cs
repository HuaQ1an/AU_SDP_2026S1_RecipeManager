using System;
using System.Collections.Generic;

namespace RecipeManagement.Core;

/// <summary>
/// Implement this class using the five Part A collections as private fields:
/// Dictionary&lt;int, Recipe&gt;, List&lt;string&gt;, LinkedList&lt;int&gt;,
/// Stack&lt;int&gt; and Queue&lt;string&gt;.
/// </summary>



public sealed class RecipeManager : IRecipeManager
{
    // TODO Part A: add your private collection fields here.
    private Dictionary<int, Recipe> allRecipes;
    private List<string> allShoppingList ;
    private LinkedList<int> cookingPlan;
    private Stack<int> removeStack;



    public RecipeManager(IEnumerable<Recipe> recipes)
    {
        // TODO Part A: validate recipes and build Dictionary<int, Recipe>.
        
        allRecipes = new Dictionary<int, Recipe>();
        allShoppingList = new List<string>();
        cookingPlan = new LinkedList<int>();
        removeStack = new Stack<int>();

        // recipes is all the recipes
        //this.allRecipes = recipes;

        foreach (Recipe c in recipes)
        {
            // copy one by one
            allRecipes.Add(c.Id,c);
        }

        // check
        /*
        foreach (var item in allRecipes)
        {
            Console.WriteLine($"ID: {item.Key}");
            Console.WriteLine($"Title: {item.Value.Title}");
            Console.WriteLine("--------------------");
        }
        */
    }

    public int RecipeCount => allRecipes.Count;
    public int ShoppingItemCount => allShoppingList.Count;
    public int CookingPlanCount => cookingPlan.Count;
    public int PendingInstructionCount => 0;
    public int RemovedRecipeCount => 0;

    public bool AddRecipe(Recipe recipe){

            ArgumentNullException.ThrowIfNull(recipe);

            // add to
            allRecipes.Add(recipe.Id, recipe);

            // check
            foreach (var item in allRecipes)
            {
                Console.WriteLine($"ID: {item.Key}");
                Console.WriteLine($"Title: {item.Value.Title}");
                Console.WriteLine("--------------------");
            }

            return true;
        }

    public Recipe? FindRecipe(int recipeId) {
        ArgumentNullException.ThrowIfNull(recipeId);

        if(recipeId < 0)
        {
            return null;
        }

        foreach (var item in allRecipes)
        {
            if(item.Key == recipeId)
            {
                Console.WriteLine($"ID: {item.Key}");
                return item.Value;
            }
                

        }

        return null;
    }

    public bool RemoveRecipe(int recipeId){

        ArgumentNullException.ThrowIfNull(recipeId);

        if(recipeId < 0)
        {
            return false;
        }

        foreach (var item in allRecipes)
        {
            if(item.Key == recipeId)
            {
                Console.WriteLine($"ID: {item.Key}");
                // remove once found
                allRecipes.Remove(recipeId);
                return true;
            }
                

        }
        // can't find
        return false;
    }

    public int AddIngredientsToShoppingList(int recipeId){

        Recipe recipe = this.FindRecipe(recipeId);
        // didn't found or other reason
        if(recipe == null)
        {
            return 0;
        }
        else
        {
            int count = 0;

            foreach (string ing in recipe.Ingredients)
            {
                //if(item.Key == recipeId)
                //{
                    //Console.WriteLine($"ID: {item.Key}");
                    // remove once found
                    //allRecipes.Remove(recipeId);

                //}
                    
                // add to shoppinglist
                allShoppingList.Add(ing);
                count++;
            }

            // testing only
            foreach (string item in allShoppingList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("=========================");
            return count;
        }

        
    }
        
    public IReadOnlyList<string> GetShoppingList()
    {
        List<string> getShopping = new List<string>();

        // use the code from aboved
        foreach (string item in allShoppingList)
        {
            getShopping.Add(item);
        }

        return getShopping;
    }

    public void ClearShoppingList()
    {
        allShoppingList.Clear();
    }
    public bool AddRecipeToCookingPlan(int recipeId)
    {
        ArgumentNullException.ThrowIfNull(recipeId);

        Recipe receipt = this.FindRecipe(recipeId);
        // when can't find receipt
        if(receipt == null)
        {
            return false;
        }
        // when the number is incorrect
        if(recipeId < 0)
        {
            return false;
        }

        foreach (int id in cookingPlan)
        {
            // see if the same, if already inside, return false
            if (id == recipeId)
            {
                Console.WriteLine("Cooking plan AddRecipe found the same");
                return false;
            }
        }

        cookingPlan.AddLast(recipeId);

        return true;
    }


    public bool RemoveRecipeFromCookingPlan(int recipeId){

        ArgumentNullException.ThrowIfNull(recipeId);

        Console.WriteLine("Cooking plan:");
        foreach (int id in cookingPlan)
        {
            Console.WriteLine("id: " + id);

        }

        foreach (int id in cookingPlan)
        {
            // if the receiptid already in cooking plan
            if (id == recipeId)
            {
                Console.WriteLine("Cooking plan removeRecipt found the same");

                // On success push the ID onto the Stack<int> and return true.
                removeStack.Push(recipeId);
                cookingPlan.Remove(recipeId);



                foreach (int stackId in removeStack)
                {
                    Console.WriteLine(stackId);
                }

                Console.WriteLine("==============================");


                return true;
            }
        }


        return false;
    }


    public bool RestoreLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement RestoreLastRemovedRecipe.");

    public int? PeekLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement PeekLastRemovedRecipe.");

    public IReadOnlyList<int> GetCookingPlan() =>
        throw new NotImplementedException("Part A: implement GetCookingPlan.");

    public bool StartCooking(int recipeId) =>
        throw new NotImplementedException("Part A: implement StartCooking.");

    public string? PeekNextInstruction() =>
        throw new NotImplementedException("Part A: implement PeekNextInstruction.");

    public string? CompleteNextInstruction() =>
        throw new NotImplementedException("Part A: implement CompleteNextInstruction.");

    public IReadOnlyList<Recipe> SearchByTitle(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByTitle.");

    public IReadOnlyList<Recipe> SearchByIngredient(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByIngredient.");

    public IReadOnlyList<Recipe> GetHighestProteinRecipes(int count) =>
        throw new NotImplementedException("Part B: implement GetHighestProteinRecipes.");

    public bool AddSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement AddSavedRecipe.");

    public bool RemoveSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement RemoveSavedRecipe.");

    public bool IsRecipeSaved(int recipeId) =>
        throw new NotImplementedException("Part B: implement IsRecipeSaved.");

    public IReadOnlyList<int> GetSavedRecipes() =>
        throw new NotImplementedException("Part B: implement GetSavedRecipes.");
}
