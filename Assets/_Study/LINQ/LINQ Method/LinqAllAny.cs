using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Quantifiers 연산은 컬렉션과 같은 데이터 집합에서 모든 요소들이 특정 조건을 만족하는지 확인할 수 있는 방법들을 제공
/// All(): 데이터 집합의 모든 요소가 주어진 조건을 만족하면 true / false
/// Any(): 데이터 집합의 요소 중 하나라도 특정 조건을 만족하면 true / false
/// </summary>
public class LinqAllAny : MonoBehaviour
{
    private void Start()
    {
        //IntegerTypeAll();
        //CustomClassAll();
        //AnyWithoutParameter();
        //IntegerTypeAny();
        CustomClassAny();
    }

    private void IntegerTypeAll()
    {
        List<int> intList = new List<int>()
        { 
            5, 10, 15, 20, 25, 30
        };

        // Query
        bool queryResult = (from num in intList
                            select num).All(num => num > 10);

        // Method
        bool methodResult = intList.All(num => num > 10);


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);

    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return "Name: " + Name + ", Age: " + Age;
        }
    }

    private void CustomClassAll()
    {
        List<Person> people = new List<Person>
        {
            new Person{Name ="Bob", Age = 20},
            new Person{Name ="Nick", Age = 30},
            new Person{Name ="Tom", Age = 40}
        };

        // Query
        bool queryResult = (from person in people
                            select person).All(person => person.Name.Length > 2 && person.Age > 10);

        // Method
        bool methodResult = people.All(person => person.Name.Length > 2 && person.Age > 10);

        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    private void AnyWithoutParameter()
    {
        List<int> intList1 = new List<int>();
        List<int> intList2 = new List<int>()
        {
            5, 10, 15, 20, 25, 30
        };

        bool methodResult1 = intList1.Any();
        bool methodResult2 = intList2.Any();


        CanvasLog.AddTextLine("Query: " + methodResult1);
        CanvasLog.AddTextLine("Method: " + methodResult2);
    }

    private void IntegerTypeAny()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25, 30
        };

        // Query
        bool queryResult = (from num in intList
                            select num)
                            .Any(num => num < 10);

        // Method
        bool methodResult = intList.Any(num => num < 10);


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    private void CustomClassAny()
    {
        List<Person> people = new List<Person>
        {
            new Person{Name ="Bob", Age = 20},
            new Person{Name ="Nick", Age = 30},
            new Person{Name ="Tom", Age = 40}
        };

        // Query
        bool queryResult = (from person in people
                            select person)
                            .Any(person => person.Name.Length > 3 && person.Age > 40);

        // Method
        bool methodResult = people.Any(person => person.Name.Length > 3 && person.Age > 40);

        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }
}
