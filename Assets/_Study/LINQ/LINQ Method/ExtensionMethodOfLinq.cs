using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExtensionMethodOfLinq : MonoBehaviour
{
    private void Start()
    {
        
    }

    // Linq의 Where(), Union(), Join() 등 Enumerable 클래스에서 구현됨.
    // 이 메서드들은 IEnumerable<T> 의 확장 메서드로 구현됨.

    private void Example1()
    {
        List<string> strArr = new List<string>
        {
            "Apple", "Banana", "Car", "Angular", "Add", "Sum"
        };

        // List에는 Where 메서드가 없지만, IEnumerable<T>를 상속받고 있어 Linq의 Enumerable에 구현되어 있는 Where을 통해 확장 메서드로 구현됨
        IEnumerable<string> linqResult = strArr.Where(item => item.StartsWith("A"));
    }
}
