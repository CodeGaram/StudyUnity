using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Student
{
    public string ID { get; set; } // 학생을 식별하는 키
    public string Name { get; set; } // 학생의 이름
    public int Grade { get; set; } // 학년
    public int Class { get; set; } // 반

    public static List<Student> GetStudents()
    {
        return new List<Student>()
        {
            // 1학년 1반
            new Student { ID = "0001", Name = "Preety",   Grade = 1, Class = 1},
            new Student { ID = "0002", Name = "Snurag",   Grade = 1, Class = 1},
            // 1학년 2반
            new Student { ID = "0003", Name = "Pranaya",  Grade = 1, Class = 2},

            // 2학년 1반
            new Student { ID = "0004", Name = "Anurag",   Grade = 2, Class = 1},
            new Student { ID = "0005", Name = "Hina",     Grade = 2, Class = 1},
            // 2학년 2반
            new Student { ID = "0006", Name = "Priyanka", Grade = 2, Class = 2},

            // 3학년 1반
            new Student { ID = "0007", Name = "santosh",  Grade = 3, Class = 1},
            new Student { ID = "0008", Name = "Tina",     Grade = 3, Class = 1},
            // 3학년 2반
            new Student { ID = "0009", Name = "Celina",   Grade = 3, Class = 2},
            // 3학년 3반
            new Student { ID = "0010", Name = "Sambit",   Grade = 3, Class = 3}
        };
    }
}

public class LinqGroupBy : MonoBehaviour
{
    private void Start()
    {
        //CountExample();
        //GroupByWithSorting();
        GroupByAnyField();
    }

    private void CountExample()
    {
        // Query
        var queryResult = (from student in Student.GetStudents()
                           group student by student.Grade into groupData
                           select new
                           {
                               Grade = groupData.Key,
                               Count = groupData.Count()
                           });

        // Method
        var methodResult = Student.GetStudents()
            .GroupBy(student => student.Grade)
            .Select(groupData => new
            {
                Grade = groupData.Key,
                Count = groupData.Count()
            });

        CanvasLog.AddTextLine("Query");
        foreach(var group in queryResult)
        {
            CanvasLog.AddTextLine(group.Grade + "학년의 인원 수: " + group.Count);
        }

        CanvasLog.AddTextLine("Method");
        foreach(var group in queryResult)
        {
            CanvasLog.AddTextLine(group.Grade + "학년의 인원 수: " + group.Count);
        }

    }

    /// <summary>
    /// 1. Grade 프로퍼티를 기준으로 데이터를 그룹화
    /// 2. 데이터 그룹 기준인 Grade 프로퍼티를 내림차순으로 정렬
    /// 3. select절에서 Class 프로퍼티를 내림차순으로 정렬
    /// </summary>
    private void GroupByWithSorting()
    {
        // Query
        var queryResult = (from student in Student.GetStudents()
                           group student by student.Grade into studentData
                           orderby studentData.Key descending
                           select new
                           {
                               Grade = studentData.Key,
                               Students = studentData.OrderByDescending(student => student.Class)
                           });

        // Method
        var methodResult = Student.GetStudents()
            .GroupBy(student => student.Grade)
            .OrderByDescending(student => student.Key)
            .Select(student => new
            {
                Grade = student.Key,
                Students = student.OrderByDescending(student => student.Class)
            });



        CanvasLog.AddTextLine("Query");
        foreach (var group in queryResult)
        {
            CanvasLog.AddTextLine(group.Grade + "학년의 인원 수: " + group.Students.Count());
            foreach(var student in group.Students)
            {
                CanvasLog.AddTextLine(student.Grade + "학년 " + student.Class + "반 " + student.Name);
            }
            CanvasLog.AddLine();
        }

        CanvasLog.AddTextLine("Method");
        foreach (var group in queryResult)
        {
            CanvasLog.AddTextLine(group.Grade + "학년의 인원 수: " + group.Students.Count());
            foreach (var student in group.Students)
            {
                CanvasLog.AddTextLine(student.Grade + "학년 " + student.Class + "반 " + student.Name);
            }
            CanvasLog.AddLine();
        }
    }

    private void GroupByAnyField()
    {
        // Query
        var queryResult = (from student in Student.GetStudents()
                           group student by new
                           {
                               student.Grade,
                               student.Class
                           } into groupData
                           select new
                           {
                               Grade = groupData.Key.Grade,
                               Class = groupData.Key.Class,
                               Count = groupData.Count()
                           });

        // Method
        var methodResult = Student.GetStudents()
            .GroupBy(student => new
            {
                student.Grade,
                student.Class
            })
            .Select(student => new
            {
                Grade = student.Key.Grade,
                Class = student.Key.Class,
                Count = student.Count()
            });


        CanvasLog.AddTextLine("Query");
        foreach(var group in queryResult)
        {
            CanvasLog.AddTextLine(group.Grade + "학년 " + group.Class + "반의 인원 수: " + group.Count);
        }

        CanvasLog.AddTextLine("Method");
        foreach(var group in queryResult)
        {
            CanvasLog.AddTextLine(group.Grade + "학년 " + group.Class + "반의 인원 수: " + group.Count);
        }
    }
}
