using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqSelect : MonoBehaviour
{
    private void Start()
    {
        //BasicUsageMethod();
        //SinglePropertyExtraction();
        //MultiplePropertyExtraction();
        //ChangeType();
        ChangePropertyValue();
    }

    class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + ", Name: " + Name + ", Age: " + Age;
        }
    }

    private void BasicUsageMethod()
    {
        List<Person> people = new List<Person>()
        {
            new Person() { ID = 100, Name = "Bob",      Age = 20},
            new Person() { ID = 200, Name = "Tim",      Age = 25},
            new Person() { ID = 300, Name = "Charles",  Age = 30},
            new Person() { ID = 400, Name = "Nick",     Age = 35},
            new Person() { ID = 500, Name = "John",     Age = 40},
            new Person() { ID = 500, Name = "Sam",      Age = 45}
        };

        // Query
        List<Person> queryResult = (from person in people
                                   select person).ToList();

        // Method
        List<Person> methodResult = people.Select(person => person).ToList();

        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText(person.ToString());
        }

        CanvasLog.AddTextLine("Method");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText(person.ToString());
        }
    }

    private void SinglePropertyExtraction()
    {
        List<Person> people = new List<Person>()
        {
            new Person() { ID = 100, Name = "Bob",      Age = 20},
            new Person() { ID = 200, Name = "Tim",      Age = 25},
            new Person() { ID = 300, Name = "Charles",  Age = 30},
            new Person() { ID = 400, Name = "Nick",     Age = 35},
            new Person() { ID = 500, Name = "John",     Age = 40},
            new Person() { ID = 500, Name = "Sam",      Age = 45}
        };

        // Query
        List<int> queryResult = (from person in people
                                    select person.ID).ToList();

        // Method
        List<int> methodResult = people.Select(person => person.ID).ToList();

        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText(person);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText(person);
        }
    }

    private void MultiplePropertyExtraction()
    {
        List<Person> people = new List<Person>()
        {
            new Person() { ID = 100, Name = "Bob",      Age = 20},
            new Person() { ID = 200, Name = "Tim",      Age = 25},
            new Person() { ID = 300, Name = "Charles",  Age = 30},
            new Person() { ID = 400, Name = "Nick",     Age = 35},
            new Person() { ID = 500, Name = "John",     Age = 40},
            new Person() { ID = 500, Name = "Sam",      Age = 45}
        };

        // Query
        List<Person> queryResult = (from person in people
                                    select new Person()
                                    {
                                        ID = person.ID,
                                        Name = person.Name
                                    }).ToList();

        // Method
        List<Person> methodResult = people.Select(person => new Person()
        {
            ID = person.ID,
            Name = person.Name,
        }).ToList();


        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText(person.ToString());
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText(person.ToString());
        }
    }

    class PersonCopy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + ", Name: " + Name + ", Age: " + Age;
        }
    }

    private void ChangeType()
    {
        List<Person> people = new List<Person>()
        {
            new Person() { ID = 100, Name = "Bob",      Age = 20},
            new Person() { ID = 200, Name = "Tim",      Age = 25},
            new Person() { ID = 300, Name = "Charles",  Age = 30},
            new Person() { ID = 400, Name = "Nick",     Age = 35},
            new Person() { ID = 500, Name = "John",     Age = 40},
            new Person() { ID = 500, Name = "Sam",      Age = 45}
        };

        // Query
        List<PersonCopy> queryResult = (from person in people
                                        select new PersonCopy()
                                        {
                                            ID = person.ID,
                                            Name = person.Name,
                                        }).ToList();

        // Method
        List<PersonCopy> methodResult = people.Select(person => new PersonCopy()
        {
            ID = person.ID,
            Name = person.Name
        }).ToList();


        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText(person.ToString());
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText(person.ToString());
        }
    }

    private void ChangePropertyValue()
    {
        List<Person> people = new List<Person>()
        {
            new Person() { ID = 100, Name = "Bob",      Age = 20},
            new Person() { ID = 200, Name = "Tim",      Age = 25},
            new Person() { ID = 300, Name = "Charles",  Age = 30},
            new Person() { ID = 400, Name = "Nick",     Age = 35},
            new Person() { ID = 500, Name = "John",     Age = 40},
            new Person() { ID = 500, Name = "Sam",      Age = 45}
        };

        // Query
        List<Person> queryResult = (from person in people
                                    select new Person()
                                    {
                                        ID = person.ID,
                                        Name = person.Name.ToLower(),
                                        Age = person.Age * 10
                                    }).ToList();

        // Method
        List<Person> methodResult = people.Select(person => new Person()
        {
            ID = person.ID,
            Name = person.Name.ToLower(),
            Age = person.Age * 10
        }).ToList();

        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText(person.ToString());
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText(person.ToString());
        }
    }
}
