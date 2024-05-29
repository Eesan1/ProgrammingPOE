using System;
using System.Globalization;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to my Recipe App! \n");
            Console.WriteLine("Please enter the details for your recipe: \n");

            Console.WriteLine("Please type the name of your recipe:");
            string recipeName = Console.ReadLine() ?? string.Empty;

            Recipe recipe = new Recipe(recipeName);

            Console.Write("Please type the number of ingredients: ");
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
                        string? unit = Console.ReadLine();

                        recipe.AddIngredient(name, quantity, unit!);
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity input. Please enter a valid number.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid ingredient count input. Exiting program.");
                return;
            }

            Console.Write("Please type how many steps are needed: \n");
            if (int.TryParse(Console.ReadLine(), out int stepCount))
            {
                for (int i = 0; i < stepCount; i++)
                {
                    Console.Write($"Step {i + 1}: ");
                    string? step = Console.ReadLine();

                    recipe.AddStep(step!);
                }
            }
            else
            {
                Console.WriteLine("Invalid step count input. Exiting program.");
                return;
            }

            Console.WriteLine("\nYour recipe:");
            Console.WriteLine(recipe);

            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Scale the recipe");
            Console.WriteLine("2. Reset quantities");
            Console.WriteLine("3. Clear all data");
            Console.WriteLine("4. Exit");

            bool exit = false;
            while (!exit)
            {
                Console.Write("\nPlease select an option: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        double factor;
                        Console.WriteLine("Enter scale factor (0.5, 2, or 3): ");

                        string input = Console.ReadLine() ?? "";

                        if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out factor))
                            {
                            recipe.Scale(factor);
                            Console.WriteLine("\nScaled recipe:");
                            Console.WriteLine(recipe.ScaledToString());
                            }
                        else
                            Console.WriteLine("Invalid input. Please enter a valid double number.");
                                        break;
                    case 2:
                        Console.WriteLine("\nQuantities reset to original values.");
                        Console.WriteLine(recipe.ToString());
                        break;
                    case 3:
                        recipe.Clear();
                        Console.WriteLine("\nAll data cleared. Enter new recipe details.");
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
