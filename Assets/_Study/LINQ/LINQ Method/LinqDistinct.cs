using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LinqDistinct : MonoBehaviour
{
    private void Start()
    {
        //IntDistinct();
        //StringDistinct();
        //StringDistinct_OrdinalIgnoreCase();
        //PersonDistinct();
        //PersonDictinct_SameObj();
        PersonDictinct_2();
    }

    private void IntDistinct()
    {
        List<int> list = new List<int>()
        {
            1, 1, 2, 3, 2, 3, 4, 4, 5, 4
        };

        // Query
        var queryResult = (from num in list 
                           select num).Distinct();

        // Method
        var methodResult = list.Distinct();

        CanvasLog.AddTextLine("Query");
        foreach (var num in queryResult)
        {
            CanvasLog.AddText(num + " ");
        }

        CanvasLog.AddTextLine("Method");
        foreach (var num in methodResult)
        {
            CanvasLog.AddText(num + " ");
        }
    }

    private void StringDistinct()
    {
        List<string> list = new List<string>()
        {
            "AA", "AA", "BB", "BB", "aa", "bb", "C", "C"
        };

        // Query
        var queryResult = (from str in list 
                           select str).Distinct();

        // Method
        var methodResult = list.Distinct();

        CanvasLog.AddTextLine("Query");
        foreach (var str in queryResult)
        {
            CanvasLog.AddText(str + " ");
        }

        CanvasLog.AddTextLine("Method");
        foreach (var str in methodResult)
        {
            CanvasLog.AddText(str + " ");
        }
    }

    private void StringDistinct_OrdinalIgnoreCase()
    {
        List<string> list = new List<string>()
        {
            "AA", "AA", "BB", "BB", "aa", "bb", "C", "C"
        };

        // Query
        var queryResult = (from str in list 
                           select str).Distinct(StringComparer.OrdinalIgnoreCase);

        // Method
        var methodResult = list.Distinct(StringComparer.OrdinalIgnoreCase);

        CanvasLog.AddTextLine("Query");
        foreach (var str in queryResult)
        {
            CanvasLog.AddText(str + " ");
        }

        CanvasLog.AddTextLine("Method");
        foreach (var str in methodResult)
        {
            CanvasLog.AddText(str + " ");
        }
    }


    class Person
    {
        public string name;
        public int age;
    }

    private void PersonDistinct()
    {
        List<Person> people = new List<Person>
        {
            new Person() {name = "Bob", age = 20},
            new Person() {name = "Bob", age = 21},
            new Person() {name = "Nick", age = 22},
            new Person() {name = "Nick", age = 23},
            new Person() {name = "John", age = 24},
            new Person() {name = "John", age = 25},
            new Person() {name = "Tim", age = 26},
            new Person() {name = "Tim", age = 27}
        };

        // Query
        var queryResult = (from person in people 
                           select person.name).Distinct();

        // Method
        var methodResult = people.Select(person => person.name).Distinct();

        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText(person + " ");
        }

        CanvasLog.AddTextLine("Method");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText(person + " ");
        }
    }

    /// <summary>
    /// Distinct는 객체의 프로퍼티 값을 비교하지 않기 때문에 모든 객체를 반환함
    /// </summary>
    private void PersonDictinct_SameObj()
    {
        List<Person> people = new List<Person>
        {
            new Person() { name = "Bob", age = 20 },
            new Person() { name = "Bob", age = 20 },
            new Person() { name = "Bob", age = 20 },
            new Person() { name = "Bob", age = 20 }
        };

        // Query
        var queryResult = (from person in people
                           select person).Distinct();

        // Method
        var methodResult = people.Distinct();

        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText("name " + person.name + " / age : " + person.age);
        }

        CanvasLog.AddTextLine("Method");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText("name " + person.name + " / age : " + person.age);
        }
    }

    /// <summary>
    /// 사용자 정의 클래스로 구성된 컬렉션에서 중복되는 요소를 제거하기 위해 다음 세 가지 방법을 사용
    /// 1. Equals() 및 GetHashCode() 메서드 재정의
    /// 2. 익명 타입 사용
    /// 3. 사용자 정의 클래스에서 IEquatable<T> 인터페이스 구현
    /// </summary>
    class Person1
    {
        public string name;
        public int age;

        public override bool Equals(object obj)
        {
            return this.name == ((Person1)obj).name && this.age == ((Person1)obj).age;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ age.GetHashCode(); 
        }
    }

    private void PersonDictinct_2()
    {
        List<Person> people = new List<Person>()
        {
            new Person() { name = "Bob", age = 20 },
            new Person() { name = "Bob", age = 20 },
            new Person() { name = "Bob", age = 20 },
            new Person() { name = "Bob", age = 20 }
        };

        // Query
        var queryResult = (from person in people
                           select new { person.name, person.age })
                           .Distinct();

        // Method
        var methodResult = people.Select(person => new {person.name, person.age}).Distinct();

        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText("name " + person.name + " / age : " + person.age);
        }

        CanvasLog.AddTextLine("Method");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText("name " + person.name + " / age : " + person.age);
        }
    }

    class Person3 : IEquatable<Person3>
    {
        public string name;
        public int age;

        public bool Equals(Person3 person)
        {
            if(object.ReferenceEquals(person, null))
            {
                return false;
            }
            
            if(object.ReferenceEquals(this, person))
            {
                return true;
            }

            return this.name.Equals(person.name) && this.age.Equals(person.age);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ age.GetHashCode();
        }
    }
}
