using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqSelectMany : MonoBehaviour
{
    private void Start()
    {
        //Example1();
        //UsedInQuerySyntax();
        //CustomClass();
        MultiplePropertyExtraction();
    }

    // SelectMany()는 Query Syntax에서 사용할 수 없으며 Method Syntax에서만 사용할 수 있다.

    // .Net 공식 문서에서는 SelectMany() 메서드를 다음과 같이 정의
    // 시퀀스의 각 요소를 IEnumerable<T>에 투영하고 결과 시퀀스를 단일 시퀀스로 평면화합니다.

    /// <summary>
    /// String List를 SelectMany().ToList()를 사용하여 List로 변환했는데 결과값은 List<char>로 반환됨
    /// 즉, SelectMany()는 추출하는 항목을 세부적인 요소로 반환한다는 것 (string은 char타입의 집합이므로 char 타입으로 반환됨)
    /// </summary>
    private void Example1()
    {
        List<string> strList = new List<string>()
        {
            "Hello", "C Sharp"
        };

        List<char> selectManyResult = strList.SelectMany(item => item).ToList();

        foreach (char item in selectManyResult)
        {
            CanvasLog.AddText(item);
        }
    }

    private void UsedInQuerySyntax()
    {
        List<string> strList = new List<string>()
        {
            "Hello", "C Sharp"
        };

        List<char> selectManyResult = (from str in strList
                                       from ch in str
                                       select ch).ToList();

        foreach (char item in selectManyResult)
        {
            CanvasLog.AddText(item);
        }
    }

    class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<string> Subject { get; set; }
    }
    
    private void CustomClass()
    {
        List<Person> people = new List<Person>()
        {
            new Person { ID = 100, Name = "Bob",        Subject = new List<string>() {"C#", "Java"}},
            new Person { ID = 200, Name = "Tim",        Subject = new List<string>() {"C++", "JS"}},
            new Person { ID = 300, Name = "Charles",    Subject = new List<string>() {"React"}},
        };

        // Query
        List<string> queryResult = (from person in people
                                    from subject in person.Subject
                                    select subject).ToList();

        // Method
        List<string> methodResult = people.SelectMany(person => person.Subject).ToList();


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
            new Person { ID = 100, Name = "Bob",        Subject = new List<string>() {"C#", "Java"}},
            new Person { ID = 200, Name = "Tim",        Subject = new List<string>() {"C++", "JS"}},
            new Person { ID = 300, Name = "Charles",    Subject = new List<string>() {"React"}},
        };

        // Query
        var queryResult = (from person in people
                            from subject in person.Subject
                            select new
                            {
                                Name = person.Name,
                                Subject = subject
                            }).ToList();

        // Method
        var methodResult = people
            .SelectMany(person => person.Subject,
            (student, subject) => new
            {
                Name = student.Name,
                Subject = subject
            }).ToList();


        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText("Name: " + person.Name + " / Subejct : " + person.Subject);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText("Name: " + person.Name + " / Subejct : " + person.Subject);
        }
    }
}
