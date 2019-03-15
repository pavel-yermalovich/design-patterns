namespace DesignPatterns.Examples.Null_Object.Example1
{
    public class Program
    {
        public static void Demo()
        {
            var autoRepository = new AutoRepository();

            var automobile = autoRepository.Find("mini cooper");

            automobile.Start();
            automobile.Stop();
        }
    }
}
