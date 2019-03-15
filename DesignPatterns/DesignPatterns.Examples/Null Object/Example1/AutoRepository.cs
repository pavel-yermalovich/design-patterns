namespace DesignPatterns.Examples.Null_Object.Example1
{
    public class AutoRepository
    {
        public AutomobileBase Find(string carName)
        {
            if (carName.Contains("mini"))
                return new MiniCooper();

            return AutomobileBase.Null;
        }
    }
}