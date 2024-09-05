using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 내부 조인은 두 개 이상의 데이터 집합에서 일치하는 데이터만 반환. 만약, 일치하는 데이터가 없을 경우 반환되는 데이터는 존재하지 않음
/// IEqyalityComparer을 가진 오버로드 메서드가 존재
/// 
/// ** IEnumerable<TOuter> **
/// - Join() 메서드를 호출하는 데이터 집합
/// 
/// ** IEnumerable<TInner> **
/// - TOuter와 Join되는 데이터 집합
/// - 내부 데이터 소스는 Join() 메서드의 첫 번째 매개변수
/// 
/// ** Func<TOuter, TKey> outerKeySelector **
/// - TOuter에서 Join에 필요한 키를 가져옴
/// - 외부 키 선택자는 Join() 메서드의 두 번째 매개변수
/// 
/// ** Func<TInner, TKey> innerKeySelector **
/// - TInner에서 join에 필요한 키를 가져옴
/// - 내부 키 선택자는 Join() 메서드의 세 번째 매개변수
/// 
/// ** Func<Touter, TInner, TResult>resultSelector **
/// - Join 결과에서 추출하고 싶은 데이터를 작성
/// - Select절과 유사
/// - 결과 선택자는 Join() 메서드의 네 번재 매개변수
/// </summary>
public class LinqJoin : MonoBehaviour
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
                new Employee { Emp_Code = "1", Emp_Name = "홍길동",   Dept_Code = "1000"},
                new Employee { Emp_Code = "2", Emp_Name = "김첨지",   Dept_Code = "2000"},
                new Employee { Emp_Code = "3", Emp_Name = "나랑두",   Dept_Code = "3000"}
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
                new Department { Dept_Code = "3000", Dept_Name = "기획"}
            };
        }
    }

    public class Address
    {
        public string Emp_Code { get; set; }     // 사원코드
        public string Address_Name { get; set; } // 주소
        public static List<Address> GetAddresses()
        {
            return new List<Address>()
            {
                new Address { Emp_Code = "1", Address_Name = "서울 어딘가"},
                new Address { Emp_Code = "2", Address_Name = "부산 어딘가"}
            };
        }
    }


    private void Start()
    {
        //Sample1();
        Join3orMoreData();
    }


    /// <summary>
    /// Employee 데이터 집합의 Emp_Code가 Address 데이터 집합의 Emp_Code에 존재하는 데이터를 추출
    /// </summary>
    private void Sample1()
    {
        // Query
        var queryResult = (from employee in Employee.GetEmployees() // TOuter
                           join address in Address.GetAddresses() // join inner in TInner
                           on employee.Emp_Code equals address.Emp_Code // on outerKeySelector equals innterKeySelector
                           select new // return
                           {
                               Emp_Code = employee.Emp_Code,
                               Emp_Name = employee.Emp_Name,
                               Address_Name = address.Address_Name
                           });

        // Method
        var methodResult = Employee.GetEmployees()
            .Join(Address.GetAddresses(),
            (employee) => employee.Emp_Code,
            (address) => address.Emp_Code,
            (employee, address) => new
            {
                Emp_Code = employee.Emp_Code,
                Emp_Name = employee.Emp_Name,
                Address_Name = address.Address_Name
            });


        CanvasLog.AddTextLine("Query");
        foreach(var result in queryResult)
        {
            CanvasLog.AddTextLine("사원코드: " + result.Emp_Code + " / 사원명: " + result.Emp_Name + " / 주소" + result.Address_Name);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach(var result in methodResult)
        {
            CanvasLog.AddTextLine("사원코드: " + result.Emp_Code + " / 사원명: " + result.Emp_Name + " / 주소" + result.Address_Name);
        }

    }

    /// <summary>
    /// 1. Employee 데이터 집합의 Dept_Code가 Department 데이터 집합의 Dept_Code와 일치하는 데이터 추출
    /// 2. 1번 과정에서 추출된 데이터의 Emp_Code가 Address 데이터 집합의 Emp_Code와 일치하는 데이터 추출
    /// </summary>
    private void Join3orMoreData()
    {
        // Query
        var queryResult = (from employee in Employee.GetEmployees()
                           join department in Department.GetDeptments()
                           on employee.Dept_Code equals department.Dept_Code
                           join address in Address.GetAddresses()
                           on employee.Emp_Code equals address.Emp_Code
                           select new
                           {
                               Dept_Name = department.Dept_Name,
                               Emp_Name = employee.Emp_Name,
                               Address_Name = address.Address_Name
                           });

        // Method
        var methodResult = Employee.GetEmployees()
            .Join(Department.GetDeptments(),
            (employee) => employee.Dept_Code,
            (department) => department.Dept_Code,
            (employee, department) => new
            {
                Dept_Name = department.Dept_Name,
                Emp_Code = employee.Emp_Code,
                Emp_Name = employee.Emp_Name
            })
            .Join(Address.GetAddresses(),
            (employee) => employee.Emp_Code,
            (address) => address.Emp_Code,
            (employee, address) => new
            {
                Dept_Name = employee.Dept_Name,
                Emp_Name = employee.Emp_Name,
                Address_Name = address.Address_Name
            });

        CanvasLog.AddTextLine("Query");
        foreach(var result in queryResult)
        {
            CanvasLog.AddTextLine("부서명: " + result.Dept_Name + " / 사원명: " + result.Emp_Name + " / 주소: " + result.Address_Name);
        }

        CanvasLog.AddTextLine("Method");
        foreach(var result in methodResult)
        {
            CanvasLog.AddTextLine("부서명: " + result.Dept_Name + " / 사원명: " + result.Emp_Name + " / 주소: " + result.Address_Name);
        }

        
    }
}

