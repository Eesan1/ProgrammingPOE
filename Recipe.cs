using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp
{
    class Recipe
    {
        public string Name { get; private set; }
        private List<Ingredient> ingredients;
        private List<string> steps;
        private List<double> originalQuantities;

        public double TotalCalories => CalculateTotalCalories();

        public Recipe(string name)
        {
            Name = name;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            originalQuantities = new List<double>();
        }

        public void AddIngredient(string name, double quantity, string unit, double calories, FoodGroup foodGroup)
        {
            ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            originalQuantities.Add(quantity);
        }

        public void AddStep(string step)
        {
            steps.Add(step);
        }

        public void Scale(double factor)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Quantity *= factor;
            }
        }

        public void Clear()
        {
            ingredients.Clear();
            steps.Clear();
            originalQuantities.Clear();
        }

        public void ResetQuantities()
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Quantity = originalQuantities[i];
            }
        }

        public double CalculateTotalCalories()
        {
                double total = 0;
                foreach (var ingredient in ingredients)
                {
                    total += ingredient.Calories * ingredient.Quantity;
                }
                return total;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name}");
            sb.AppendLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                sb.AppendLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }
            sb.AppendLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {steps[i]}");
            }
            sb.AppendLine($"\nTotal Calories: {TotalCalories}");
            return sb.ToString();
        }
    }

    class Ingredient
    {
        public string Name { get; }
        public double Quantity { get; set; }
        public string Unit { get; }
        public double Calories { get; }
        public FoodGroup FoodGroup { get; }

        public Ingredient(string name, double quantity, string unit, double calories, FoodGroup foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }

    enum FoodGroup
    {
        Dairy,
        Protein,
        Fruit,
        Vegetable,
        Grain,
        Fat,
        Sugar
    }
}
