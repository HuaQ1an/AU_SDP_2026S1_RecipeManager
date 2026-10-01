using System.Collections.Generic;
using RecipeManagement.Core;

namespace RecipeManagement.Tests;

/// <summary>
/// Example tests from the assignment specification. Add your own tests as you work.
/// </summary>
public sealed class RecipeManagerTests
{
    [Fact]
    public void Constructor_BuildsRecipeDictionary()
    {
        var manager = CreateManager();
        Assert.Equal(2, manager.RecipeCount);
        Assert.Equal("Recipe A", manager.FindRecipe(10)?.Title);
    }

    [Fact]
    public void InstructionsAreCompletedInFileOrder()
    {
        var manager = CreateManager();
        Assert.True(manager.StartCooking(10));
        Assert.Equal("First step", manager.PeekNextInstruction());
        Assert.Equal("First step", manager.CompleteNextInstruction());
        Assert.Equal("Second step", manager.PeekNextInstruction());
    }

    [Fact]
    public void RemovedRecipesAreRestoredLastInFirstOut()
    {
        var manager = CreateManager();
        manager.AddRecipeToCookingPlan(10);
        manager.AddRecipeToCookingPlan(20);
        manager.RemoveRecipeFromCookingPlan(10);
        manager.RemoveRecipeFromCookingPlan(20);
        Assert.Equal(20, manager.PeekLastRemovedRecipe());
        Assert.True(manager.RestoreLastRemovedRecipe());
        Assert.Equal(new[] { 20 }, manager.GetCookingPlan());
    }


    [Fact]
    public void AddRecipe_Samething_twice_Returns_False()
    {

        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};

        List<Recipe> recipts = new List<Recipe>();
        // add inside
        recipts.Add(recipt);

        RecipeManager managers = new RecipeManager(recipts);

        Recipe recipt2 =  new Recipe{ Id = 10, Title = "Recipe A"};

        // add same thing
        bool result = managers.AddRecipe(recipt2);

        Assert.False(result);
        Assert.Equal(1, managers.RecipeCount);

    }


    [Fact]
    public void Cooking_Plan_duplicate_test_twice_Returns_False()
    {

        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};


        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);


        RecipeManager managers = new RecipeManager(recipes);


        // create two same cooking
        bool first = managers.AddRecipeToCookingPlan(10);
        bool second = managers.AddRecipeToCookingPlan(10);


        Assert.True(first);
        Assert.False(second);
        Assert.Equal(1, managers.CookingPlanCount);

    }

    [Fact]
    public void Empty_InstructionQueue_Returns_Null()
    {
        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};

        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);

        RecipeManager manager = new RecipeManager(recipes);

        // Act
        string completeResult = manager.CompleteNextInstruction();

        // Assert
        Assert.Null(completeResult);
    }



    [Fact]
    public void Empty_InstructionQueue_Second_Returns_Null()
    {
        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};

        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);

        RecipeManager manager = new RecipeManager(recipes);

        // Act
        string peek = manager.PeekNextInstruction();

        // Assert
        Assert.Null(peek);

    }



    [Fact]
    public void ClearShoppingList_testing()
    {
        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};
        Recipe recipt2 =  new Recipe{ Id = 11, Title = "Recipe b"};
        Recipe recipt3 =  new Recipe{ Id = 12, Title = "Recipe c"};
        Recipe recipt4 =  new Recipe{ Id = 13, Title = "Recipe d"};

        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);
        recipes.Add(recipt2);
        recipes.Add(recipt3);
        recipes.Add(recipt4);


        RecipeManager manager = new RecipeManager(recipes);

        manager.AddIngredientsToShoppingList(1);

        // clear shoppinglist
        manager.ClearShoppingList();

        // Assert
        Assert.Equal(0, manager.ShoppingItemCount);
    }


    [Fact]
    public void FindRecipe_Notexist_ID_ReturnsNull()
    {

        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};

        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);


        RecipeManager manager = new RecipeManager(recipes);

        // 999 should not exist
        Recipe notExistresult = manager.FindRecipe(999);

        // Assert
        Assert.Null(notExistresult);
    }


    [Fact]
    public void FindRecipe_Notexist_ID_ReturnzeroCount()
    {

        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};

        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);


        RecipeManager manager = new RecipeManager(recipes);

        // 999 should not exist
        int notexistResult = manager.AddIngredientsToShoppingList(999);

        // Assert
        Assert.Equal(0, notexistResult);
        Assert.Equal(0, manager.ShoppingItemCount);
    }



    [Fact]
    public void AddreceiptToCookingPlan_whenNoReceipt_ReturnFalse()
    {

        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};

        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);


        RecipeManager manager = new RecipeManager(recipes);

        // 999 should not exist
        bool addCookingResult = manager.AddRecipeToCookingPlan(999);

        // Assert
        Assert.False(addCookingResult);
        Assert.Equal(0, manager.CookingPlanCount);

    }



    [Fact]
    public void Test_Empty_Stack_Return()
    {

        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};

        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);


        RecipeManager manager = new RecipeManager(recipes);

        // 999 should not exist
        int? peekRes = manager.PeekLastRemovedRecipe();


        // Assert
        Assert.Null(peekRes);

    }



    [Fact]
    public void Test_Empty_Stack_Second_Return()
    {

        Recipe recipt =  new Recipe{ Id = 10, Title = "Recipe A"};

        List<Recipe> recipes = new List<Recipe>();
        recipes.Add(recipt);

        RecipeManager manager = new RecipeManager(recipes);

        // 999 should not exist
        bool restoreResult = manager.RestoreLastRemovedRecipe();


        // Assert
        Assert.Equal(0, manager.CookingPlanCount);
        
    }


    private static RecipeManager CreateManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe
            {
                Id = 10,
                Title = "Recipe A",
                Ingredients = new() { "1 apple" },
                Instructions = new() { "First step", "Second step" }
            },
            new Recipe
            {
                Id = 20,
                Title = "Recipe B"
            }
        });
    }
}
