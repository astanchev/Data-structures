namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;

    using Wintellect.PowerCollections;

    public class PersonCollection : IPersonCollection
    {
        private Dictionary<string, Person> personByEmail = new Dictionary<string, Person>();
        private Dictionary<string, SortedSet<Person>> personByEmailDomain = new Dictionary<string, SortedSet<Person>>();
        private Dictionary<string, SortedSet<Person>> personByNameAndTown = new Dictionary<string, SortedSet<Person>>();
        private OrderedDictionary<int, SortedSet<Person>> personByAge = new OrderedDictionary<int, SortedSet<Person>>();
        private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> personByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();

        public bool AddPerson(string email, string name, int age, string town)
        {
            if (this.FindPerson(email) != null)
            {
                //Person already exists
                return false;
            }

            var person = new Person()
            {
                Age = age,
                Name = name,
                Email = email,
                Town = town,
            };

            // Add by email
            this.personByEmail.Add(email, person);

            // Add by email domain
            var emailDomain = this.ExtractEmailDomain(email);
            this.personByEmailDomain.AppendValueToKey(emailDomain, person);
            
            // Add by {name + town}
            var nameAndTown = this.CombineNameAndTown(name, town);
            this.personByNameAndTown.AppendValueToKey(nameAndTown, person);

            // Add by age
            this.personByAge.AppendValueToKey(age, person);

            return true;
        }

        public int Count => this.personByEmail.Count;

        public Person FindPerson(string email)
        {
            Person person = null;
            var personExists = this.personByEmail.TryGetValue(email, out person);

            return person;
        }

        public bool DeletePerson(string email)
        {
            var person = this.FindPerson(email);

            if (person == null)
            {
                //Person already exists
                return false;
            }

            // Delete person from personByEmail
            var personDeleted = this.personByEmail.Remove(email);

            //  Delete person from personByEmailDomain
            var emailDomain = this.ExtractEmailDomain(email);
            this.personByEmailDomain[emailDomain].Remove(person);

            //  Delete person from personByNameAndTown
            var nameAndTown = this.CombineNameAndTown(person.Name, person.Town);
            this.personByNameAndTown[nameAndTown].Remove(person);

            //  Delete person from personByAge
            this.personByAge[person.Age].Remove(person);

            return true;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            return this.personByEmailDomain.GetValuesForKey(emailDomain);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            var nameAndTown = this.CombineNameAndTown(name, town);

            return this.personByNameAndTown.GetValuesForKey(nameAndTown);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            var personsInRange = this.personByAge.Range(startAge, true, endAge, true);

            foreach (var personsByAge in personsInRange)
            {
                foreach (var person in personsByAge.Value)
                {
                    yield return person;
                }
            }
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            throw new NotImplementedException();
        }

        private string ExtractEmailDomain(string email)
        {
            var domain = email.Split('@')[1];

            return domain;
        }

        private string CombineNameAndTown(string name, string town)
        {
            const string separator = "[!]";

            return name + separator + town;
        }
    }
}
