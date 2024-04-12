using System;

namespace RecipeApp
{
    class Recipe
    {
        private string[] ingredients;
        private double[] quantities;
        private string[] units;
        private string[] steps;

        public Recipe()
        {
            ingredients = new string[0];
            quantities = new double[0];
            units = new string[0];
            steps = new string[0];
        }

        public void AddIngredient(string name, double quantity, string unit)
        {
            Array.Resize(ref ingredients, ingredients.Length + 1);
            Array.Resize(ref quantities, quantities.Length + 1);
            Array.Resize(ref units, units.Length + 1);

            int index = ingredients.Length - 1;
            ingredients[index] = name;
            quantities[index] = quantity;
            units[index] = unit;
        }

        public void AddStep(string step)
        {
            Array.Resize(ref steps, steps.Length + 1);
            int index = steps.Length - 1;
            steps[index] = step;
        }

        public void Scale(double factor)
        {
            for (int i = 0; i < quantities.Length; i++)
            {
                quantities[i] *= factor;
            }
        }

        public void Reset()
        {
            
        }

        public void Clear()
        {
            ingredients = new string[0];
            quantities = new double[0];
            units = new string[0];
            steps = new string[0];
        }

        public override string ToString()
        {
            string recipeString = "Ingredients:\n";
            for (int i = 0; i < ingredients.Length; i++)
            {
                recipeString += $"{quantities[i]} {units[i]} of {ingredients[i]}\n";
            }

            recipeString += "\nSteps:\n";
            for (int i = 0; i < steps.Length; i++)
            {
                recipeString += $"{i + 1}. {steps[i]}\n";
            }

            return recipeString;
        }
    }
}
