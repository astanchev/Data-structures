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
        private OrderedDictionary<string, SortedSet<Person>> personByAge = new OrderedDictionary<string, SortedSet<Person>>();
        private Dictionary<string, SortedSet<Person>> personByTownAndAge = new Dictionary<string, SortedSet<Person>>();

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

            this.personByEmail.Add(email, person);

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

            var personDeleted = this.personByEmail.Remove(email);
            return true;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            throw new NotImplementedException();
        }
    }
}
