using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RecipeApp
{
    class Program
    {
        public delegate void HighCalorieNotification(string recipeName);

        static void Main(string[] args)
        {
            List<Recipe> recipes = new List<Recipe>();
            HighCalorieNotification highCalorieNotification = NotifyHighCalorie;

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to my Recipe App! \n");
                Console.WriteLine("Options:");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. Display a specific recipe");
                Console.WriteLine("3. List all recipes that were recorded");
                Console.WriteLine("4. Exit the program");
                Console.Write("Please select one of the following options: ");

                if (int.TryParse(Console.ReadLine(), out int mainChoice))
                {
                    switch (mainChoice)
                    {
                        case 1:
                            AddRecipe(recipes, highCalorieNotification);
                            break;
                        case 2:
                            DisplayRecipe(recipes);
                            break;
                        case 3:
                            ListRecipes(recipes);
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static void AddRecipe(List<Recipe> recipes, HighCalorieNotification highCalorieNotification)
        {
            Console.WriteLine("\nPlease enter the details for your recipe:");

            Console.Write("Please type the name of your recipe: ");
            string recipeName = Console.ReadLine() ?? string.Empty;

            Recipe recipe = new Recipe(recipeName);

            Console.Write("Please type the number of ingredients required: ");
            if (int.TryParse(Console.ReadLine(), out int ingredientCount))
            {
                for (int i = 0; i < ingredientCount; i++)
                {
                    Console.Write($"Ingredient {i + 1} name: ");
                    string name = Console.ReadLine() ?? string.Empty;

                    Console.Write($"Quantity of {name}: ");
                    if (double.TryParse(Console.ReadLine(), out double quantity))
                    {
                        Console.Write($"Unit of measurement for {name}: ");
                        string unit = Console.ReadLine() ?? string.Empty;

                        Console.Write($"Number of calories for {name}: ");
                        if (double.TryParse(Console.ReadLine(), out double calories))
                        {
                            Console.WriteLine("Food group (Either Dairy, Protein, Fruit, Vegetable, Grain, Fat or Sugar): ");
                            if (Enum.TryParse(Console.ReadLine(), out FoodGroup foodGroup))
                            {
                                recipe.AddIngredient(name, quantity, unit, calories, foodGroup);
                            }
                            else
                            {
                                Console.WriteLine("Invalid food group. Please enter a valid food group.");
                                i--;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number for calories.");
                            i--; // Retry the current ingredient
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number for quantity.");
                        i--; // Retry the current ingredient
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid ingredient count input. Returning to main menu.");
                return;
            }

            Console.Write("Please type how many steps are needed: ");
            if (int.TryParse(Console.ReadLine(), out int stepCount))
            {
                for (int i = 0; i < stepCount; i++)
                {
                    Console.Write($"Step {i + 1}: ");
                    string step = Console.ReadLine() ?? string.Empty;
                    recipe.AddStep(step);
                }
            }
            else
            {
                Console.WriteLine("Invalid step count input. Returning to main menu.");
                return;
            }

            recipes.Add(recipe);
            if (recipe.TotalCalories > 300)
            {
                highCalorieNotification(recipeName);
            }

            Console.WriteLine("\nRecipe has been added successfully!");
        }

        static void DisplayRecipe(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            ListRecipes(recipes);

            Console.Write("Please type the name of the recipe to display: ");
            string recipeName = Console.ReadLine() ?? string.Empty;

            Recipe? recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe != null)
            {
                Console.WriteLine(recipe);
                DisplayRecipeOptions(recipe);
            }
            else
            {
                Console.WriteLine("The recipe was not found.");
            }
        }

        static void ListRecipes(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("\nRecipes:");
            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }
        }

        static void DisplayRecipeOptions(Recipe recipe)
        {
            bool recipeExit = false;
            while (!recipeExit)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Scale the recipe");
                Console.WriteLine("2. Reset quantities");
                Console.WriteLine("3. Clear all data");
                Console.WriteLine("4. Return to main menu");
                Console.Write("Please select an option: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ScaleRecipe(recipe);
                            break;
                        case 2:
                            recipe.ResetQuantities();
                            Console.WriteLine("\nQuantities reset to original values.");
                            Console.WriteLine(recipe);
                            break;
                        case 3:
                            recipe.Clear();
                            Console.WriteLine("\nAll data has been cleared.");
                            recipeExit = true;
                            break;
                        case 4:
                            recipeExit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        static void ScaleRecipe(Recipe recipe)
        {
            Console.Write("Enter scale factor (Either 0.5, 2, or 3): ");
            if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double factor))
            {
                recipe.Scale(factor);
                Console.WriteLine("\nScaled recipe:");
                Console.WriteLine(recipe);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static void NotifyHighCalorie(string recipeName)
        {
            Console.WriteLine($"\nWarning: The total calories of the recipe '{recipeName}' exceed 300 calories.");
        }
    }
}
