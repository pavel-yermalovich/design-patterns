using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Examples.Composite
{
    class Before
    {
        static void Main()
        {
            var goldForKill = 1023;

            var pavel = new Person("Pavel");
            var john = new Person("John");
            var mary = new Person("Mary");
            var birgit = new Person("Birgit");
            var emma = new Person("Emma");
            var david = new Person("David");

            var developers = new Group { Name = "Developers", Members = { pavel, emma, john } };
            var individuals = new List<Person> { mary, birgit, david };
            var groups = new List<Group> { developers };

            var totalToSplitBy = groups.Count() + individuals.Count();

            var amountForEach = goldForKill / totalToSplitBy;
            var leftOver = goldForKill % totalToSplitBy;

            foreach(var individual in individuals)
            {
                individual.Gold += amountForEach + leftOver;
                leftOver = 0;
                individual.Stats();
            }

            foreach(var group in groups)
            {

            }
        }
    }
}
