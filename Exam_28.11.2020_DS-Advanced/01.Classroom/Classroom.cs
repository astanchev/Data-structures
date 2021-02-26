namespace _01.Classroom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Classroom : IClassroom
    {
        private Dictionary<int, Student> students = new Dictionary<int, Student>();

        private Dictionary<string, Class> classes = new Dictionary<string, Class>();

        private Dictionary<string, HashSet<Student>> byTown = new Dictionary<string, HashSet<Student>>();

        private Dictionary<int, HashSet<Student>> byHeight = new Dictionary<int, HashSet<Student>>();

        private Dictionary<int, HashSet<Student>> byAge = new Dictionary<int, HashSet<Student>>();

        public void RegisterStudent(Student student, Class classToAdd)
        {
            if (this.students.ContainsKey(student.Id) || !this.classes.ContainsKey(classToAdd.Name))
            {
                throw new ArgumentException();
            }

            if (classToAdd.Students.ContainsKey(student.Name))
            {
                throw new ArgumentException();
            }

            student.Class = classToAdd;
            this.students.Add(student.Id, student);

            classToAdd.Students.Add(student.Name, student);
            this.classes[classToAdd.Name] = classToAdd;

            if (!this.byTown.ContainsKey(student.Town))
            {
                this.byTown[student.Town] = new HashSet<Student>();
            }

            this.byTown[student.Town].Add(student);

            if (!this.byHeight.ContainsKey(student.Height))
            {
                this.byHeight[student.Height] = new HashSet<Student>();
            }

            this.byHeight[student.Height].Add(student);

            if (!this.byAge.ContainsKey(student.Age))
            {
                this.byAge[student.Age] = new HashSet<Student>();
            }

            this.byAge[student.Age].Add(student);
        }

        public void CreateClass(string name)
        {
            if (this.classes.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            this.classes.Add(name, new Class(name));
        }

        public bool Exists(Student student) => this.students.ContainsKey(student.Id);

        public bool Exists(Class classToSearch) => this.classes.ContainsKey(classToSearch.Name);

        public Student GetStudent(string name, Class studentClass)
        {
            if (!this.classes.ContainsKey(studentClass.Name) || !this.classes[studentClass.Name].Students.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            return this.classes[studentClass.Name].Students[name];
        }

        public Student RemoveStudent(string name, Class studentClass)
        {
            var studentToRemove = this.GetStudent(name, studentClass);

            this.students.Remove(studentToRemove.Id);
            this.classes[studentClass.Name].Students.Remove(name);

            this.byTown[studentToRemove.Town].Remove(studentToRemove);

            if (this.byTown[studentToRemove.Town].Count == 0)
            {
                this.byTown.Remove(studentToRemove.Town);
            }

            this.byHeight[studentToRemove.Height].Remove(studentToRemove);

            if (this.byHeight[studentToRemove.Height].Count == 0)
            {
                this.byHeight.Remove(studentToRemove.Height);
            }

            this.byAge[studentToRemove.Age].Remove(studentToRemove);

            if (this.byAge[studentToRemove.Age].Count == 0)
            {
                this.byAge.Remove(studentToRemove.Age);
            }

            return studentToRemove;
        }

        public IEnumerable<Student> GetStudentsByClass(Class studentsClass)
        {
            if (!this.classes.ContainsKey(studentsClass.Name))
            {
                throw new ArgumentException();
            }

            return this.classes[studentsClass.Name].Students.Values.ToList();
        }

        public IEnumerable<Student> GetStudentByTown(string town)
        {
            if (!this.byTown.ContainsKey(town))
            {
                return new List<Student>();
            }

            return this.byTown[town].ToList();
        }

        public void MoveClass(Class oldClass, Class newClass, string studentName)
        {
            if (!this.classes.ContainsKey(oldClass.Name) 
                || !this.classes.ContainsKey(newClass.Name) 
                || !this.classes[oldClass.Name].Students.ContainsKey(studentName))
            {
                throw new ArgumentException();
            }

            var studentToMove = this.classes[oldClass.Name].Students[studentName];

            studentToMove.Class = newClass;
            
            this.classes[oldClass.Name].Students.Remove(studentName);
            this.classes[newClass.Name].Students.Add(studentName, studentToMove);
        }

        public IEnumerable<Student> GetAllOrderedByHeightDescThenByNameAscThenByTownNameDesc()
        {
            return this.students
                .Select(s => s.Value)
                .OrderByDescending(s => s.Height).ThenBy(s => s.Name)
                .ThenByDescending(s => s.Town).ToList();
        }

        public IEnumerable<Student> GetStudentByAge(int age)
        {
            if (!this.byAge.ContainsKey(age))
            {
                return new List<Student>();
            }

            return this.byAge[age].ToList();
        }

        public IEnumerable<Student> GetStudentsInHeightRange(int low, int hi)
        {
            return this.byHeight
                .Where(s => low <= s.Key && s.Key <= hi)
                .SelectMany(s => s.Value)
                .ToList();
        }
    }
}