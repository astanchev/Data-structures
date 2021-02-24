namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonCollectionSlow : IPersonCollection
    {
        private List<Person> persons = new List<Person>();

        public bool AddPerson(string email, string name, int age, string town)
        {
            var person = new Person()
            {                
                Age = age,
                Name = name,
                Email = email,
                Town = town,
            };

            this.persons.Add(person);

            return true;
        }

        public int Count => this.persons.Count;

        public Person FindPerson(string email)
        {
            return this.persons.FirstOrDefault(p => p.Email == email);
        }

        public bool DeletePerson(string email)
        {
            var person = this.FindPerson(email);

            return this.persons.Remove(person);
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
