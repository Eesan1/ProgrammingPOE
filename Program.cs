using System;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Recipe recipe = new Recipe();

            Console.WriteLine("Welcome to my Recipe App!");
            Console.WriteLine("Please enter the details for your recipe:");

            Console.Write("Please type the number of ingredients: ");
            int ingredientCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < ingredientCount; i++)
            {
                Console.Write($"Ingredient {i + 1} name: ");
                string name = Console.ReadLine();

                Console.Write($"Quantity of {name}: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.Write($"Unit of measurement for {name}: ");
                string? unit = Console.ReadLine();

                recipe.AddIngredient(name, quantity, unit);
            }

            Console.Write("Please type how many steps are needed: ");
            int stepCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < stepCount; i++)
            {
                Console.Write($"Step {i + 1}: ");
                string? step = Console.ReadLine();

                recipe.AddStep(step);
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
                        Console.Write("Enter scale factor (0.5, 2, or 3): ");
                        double factor;
                        if (!double.TryParse(Console.ReadLine(), out factor))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }
                        recipe.Scale(factor);
                        Console.WriteLine("\nScaled recipe:");
                        Console.WriteLine(recipe);
                        break;
                    case 2:
                        recipe.Reset();
                        Console.WriteLine("\nQuantities reset to original values.");
                        Console.WriteLine(recipe);
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
