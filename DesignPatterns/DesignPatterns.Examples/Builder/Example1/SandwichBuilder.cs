namespace DesignPatterns.Examples.Builder.Example1
{
    public abstract class SandwichBuilder
    {
        protected Sandwich Sandwich;

        public Sandwich GetSandwich()
        {
            return Sandwich;
        }

        public void CreateNewSandwich()
        {
            Sandwich = new Sandwich();
        }

        public abstract void PrepareBread();
        public abstract void ApplyMeatAndCheese();
        public abstract void ApplyVegetables();
        public abstract void AddCondiments();
    }
}
