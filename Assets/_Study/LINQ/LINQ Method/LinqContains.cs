using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// LINQ의 Contains()는 시퀀스 또는 컬렉션에 특정 요소가 포함되어 있는지 여부를 확인하기 위해 사용(true/false)
/// Contains는 System.Linq와 System.Collections.Generic 두 네임스페이스에 존재
/// System.Linq의 contains는 IEqualityComparer를 지원하는 오버로드가 있음
/// System.Collections.Generic에 있는 Contains()도 bool 반환
/// 사용자 정의 클래스 타입에서 특정 프로터피에 특정 값이 존재하는지 확인하기 위해서는 System.Linq의 Contains()를 사용해야 함
/// </summary>
public class LinqContains : MonoBehaviour
{
    private void Start()
    {
        //IntegerType();
        //StringType();
        UsedIEqualityComparer();
    }

    private void IntegerType()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25, 30
        };

        // Query
        bool queryResult = (from num in intList
                            select num).Contains(30);

        // Method
        bool methodResult1 = intList.Contains<int>(30);
        bool methodResult2 = intList.AsEnumerable().Contains(30);
        bool methodResult3 = intList.AsQueryable().Contains(30);

        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult1 + " " + methodResult2 + " " + methodResult3);

    }

    private void StringType()
    {
        List<string> strList = new List<string>()
        {
            "oracle", "Java", "JavaScript"
        };

        // Query
        bool queryResult = (from str in strList
                            select str).Contains("javascript");
        
        bool queryResult_IEqualityComparer = (from str in strList
                            select str).Contains("javascript", StringComparer.OrdinalIgnoreCase);

        // Method
        bool methodResult = strList.Contains<string>("javascript");
        
        bool methodResult_IEqualityComparer = strList.Contains<string>("javascript", StringComparer.OrdinalIgnoreCase);

        CanvasLog.AddTextLine("Query: " + queryResult + " / IEqualityComparer: " + queryResult_IEqualityComparer);
        CanvasLog.AddTextLine("Method: " + methodResult + " / IEqualityComparer: " + methodResult_IEqualityComparer);
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

    public class PersonComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if(object.ReferenceEquals(x, y)) return true;
            if(object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

            return x.Name == y.Name && x.Age == y.Age;
        }

        public int GetHashCode(Person obj)
        {
            if(obj == null) return 0;

            int NameHashCode = obj.Name == null ? 0 : obj.Name.GetHashCode();
            int AgeHashCode = obj.Age.GetHashCode();

            return NameHashCode ^ AgeHashCode;
        }
    }

    private void UsedIEqualityComparer()
    {
        List<Person> peopleA = new List<Person>
        {
            new Person{Name ="Bob", Age = 20},
            new Person{Name ="Nick", Age = 30},
            new Person{Name ="Tom", Age = 40}
        };

        List<Person> peopleB = new List<Person>
        {
            new Person{Name ="Bob", Age = 20}
        };


        PersonComparer personComparer = new PersonComparer();

        // Query
        bool queryResult = (from person in peopleA
                                    select person).Contains(peopleB[0], personComparer);

        // Method
        bool methodResult = peopleA.Contains(peopleB[0], personComparer);

        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

}
