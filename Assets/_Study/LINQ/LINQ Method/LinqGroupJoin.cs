using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 두 개 이상의 데이터 집합에서 일치하는 키를 기준으로 그룹화된 데이터를 반환
/// Join() 메서드와 GroupBy() 메서드의 기능이 하나로 합쳐진 메서드
/// Query Syntax에서는 GroupJoin() 메서드를 사용할 수 없어 join 연산자와 into 연산자를 함께 사용
/// 
/// IEqualityComparer를 사용하는 오버로드 버전이 존재
/// 
/// ** IEnumerable<TOuter> **
/// - GroupJoin() 메서드를 호출하는 데이터 집합
/// 
/// ** IEnumerable<TInner> **
/// - TOuter와 Join되는 데이터 집합
/// - TInner는 GroupJoin() 메서드의 첫 번째 매개변수
/// 
/// ** Func<TOuter, TKey> outerKeySelector **
/// - TOuter에서 Join에 필요한 키를 가져옴
/// - outerKeySelector는 GroupJoin() 메서드의 두 번재 매개변수
/// 
/// ** Func<TInner, TKey> innerKeySelector **
/// - TInner에서 Join에 필요한 키를 가져옴
/// - innerKeySelector는 GroupJoin() 메서드의 세 번재 매개변수
/// 
/// ** Func<Touter, TInner, TResult>resultSelector **
/// - Join 결과에서 추출하고 싶은 데이터를 작성
/// - Select절과 유사
/// - resultSelector는 GroupJoin() 메서드의 네 번재 매개변수
/// </summary>
public class LinqGroupJoin : MonoBehaviour
{
    public class Employee
    {
        public string Emp_Code { get; set; }  // 사원코드
        public string Emp_Name { get; set; }  // 사원명
        public string Dept_Code { get; set; } // 부서코드
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee { Emp_Code = "1", Emp_Name = "홍길동", Dept_Code = "1000"},
                new Employee { Emp_Code = "2", Emp_Name = "호날두", Dept_Code = "1000"},
                new Employee { Emp_Code = "3", Emp_Name = "김첨지", Dept_Code = "2000"},
                new Employee { Emp_Code = "4", Emp_Name = "다람쥐", Dept_Code = "2000"},
                new Employee { Emp_Code = "5", Emp_Name = "김환자", Dept_Code = "2000"},
                new Employee { Emp_Code = "6", Emp_Name = "마이콜", Dept_Code = "2000"},
                new Employee { Emp_Code = "7", Emp_Name = "나랑두", Dept_Code = "3000"}
            };
        }
    }

    public class Department
    {
        public string Dept_Code { get; set; } // 부서코드
        public string Dept_Name { get; set; } // 부서명
        public static List<Department> GetDeptments()
        {
            return new List<Department>()
            {
                new Department { Dept_Code = "1000", Dept_Name = "영업"},
                new Department { Dept_Code = "2000", Dept_Name = "기획"}
            };
        }
    }


    private void Start()
    {
        Sample1();
    }


    private void Sample1()
    {
        // Query
        var queryResult = (from department in Department.GetDeptments()         // TOuter
                           join employee in Employee.GetEmployees()             // TInner
                           on department.Dept_Code equals employee.Dept_Code    // on outerKeySelector equals innerKeySelector
                           into groupResult                                     // Group
                           select new                                           // Select
                           {
                               department,
                               groupResult
                           });

        // Method
        var methodResult = Department.GetDeptments()
            .GroupJoin(Employee.GetEmployees(),
            (department) => department.Dept_Code,
            (employee) => employee.Dept_Code,
            (department, employee) => new
            {
                department,
                employee
            });


        CanvasLog.AddTextLine("Query");
        foreach(var result in queryResult)
        {
            CanvasLog.AddTextLine("부서명: " + result.department.Dept_Name);
            foreach(var employee in result.groupResult)
            {
                CanvasLog.AddTextLine("사원 코드: " + employee.Emp_Code + ", 사원명: " + employee.Emp_Name);
            }
            CanvasLog.AddLine();
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach(var result in methodResult)
        {
            CanvasLog.AddTextLine("부서명: " + result.department.Dept_Name);
            foreach(var employee in result.employee)
            {
                CanvasLog.AddTextLine("사원 코드: " + employee.Emp_Code + ", 사원명: " + employee.Emp_Name);
            }
            CanvasLog.AddLine();
        }
    }

}