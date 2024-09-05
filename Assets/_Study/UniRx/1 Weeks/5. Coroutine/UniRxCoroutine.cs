using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

/// <summary>
/// UniRx의 선언적 프로그래밍 기법은 높은 가독성을 보장해주지만 복잡한 처리를 수행하기에는 
/// 기존 절차적 방식의 프로그래밍에 비해 불리하며 내부적으로 적은 크기라 해도 동적 생성으로 인한 가비지가 쌓이는 문제가 있다.
/// 때문에 오퍼레이터들로 처리하기에 복잡한 코드들은 Coroutine을 사용하고 스트림을 적절히 연동
/// 
/// Coroutine을 Stream으로 변환함으로써 Coroutine에서 처리 완료된 작업의 결과를 UniRx의 오퍼레이터에서 이어 받아 사용할 수 있게 해줌.
/// </summary>
public class UniRxCoroutine : MonoBehaviour
{
    private void Start()
    {
        //CoroutineToStream();
        //CoroutineToStream_GetYieldReturn();
        //OnNextInCoroutine();
        //ConvertStreamToCoroutine();
        //FromCoroutine();
        WhenAll_FromCoroutine();
    }


    /// <summary>
    /// Observable.FromCoroutine을 통해 Coroutine을 시작시키고 해당 Coroutine이 완료될 때까지 대기하다가 .Subscribe()를 수행
    /// </summary>
    private void ConvertCoroutineToStream()
    {
        Observable.FromCoroutine(Example1_Coroutine)
            .Subscribe(                                     // Subscribe(OnNext, OnCompleted) 오버로드 메서드
                _ => CanvasLog.AddTextLine("OnNext"),
                () => CanvasLog.AddTextLine("OnCompleted")
            ).AddTo(gameObject);                            // 게임 오브젝트 인스턴스가 DestroyAddTo로 수명 관리


        /*
         * Result
         * Start Coroutine
         * Finish Coroutine
         * OnNext
         * OnCompleted
         */
    }

    IEnumerator Example1_Coroutine()
    {
        CanvasLog.AddTextLine("Start Coroutine");

        // your code
        yield return new WaitForSeconds(3.0f);

        CanvasLog.AddTextLine("Finish Coroutine");
    }



    private List<int> intList;
    /// <summary>
    /// Coroutine의 yield return 값을 스트림으로 받아옴.
    /// </summary>
    private void CoroutineToStream_GetYieldReturn()
    {
        intList = new List<int>();
        intList.Add(1);
        intList.Add(2);
        intList.Add(3);

        Observable.FromCoroutineValue<int>(Example2_Coroutine)
            .Subscribe(x => CanvasLog.AddText(x + " "))
            .AddTo(gameObject);

        /*
         * Result
         * 1
         * 2
         * 3
         */
    }

    IEnumerator Example2_Coroutine()
    {
        foreach(var item in intList)
        {
            yield return item;
        }
    }



    public bool isPauesd;
    /// <summary>
    /// 코루틴 내부에서 OnNect를 직접 발행
    /// Observable.FromCoroutine<T>에 IObserver<T>를 전달하도록 구현 가능
    /// IObserver<T>를 코루틴에 넘겨줌으로써 코루틴 내의 임의의 타이밍에 직접 OnNext를 발행할 수 있다.
    /// </summary>
    private void OnNextInCoroutine()
    {
        Observable.FromCoroutine<long>(observer => CountCoroutine(observer))
            .Subscribe(x => CanvasLog.AddText(x + " "))
            .AddTo(gameObject);
    }

    IEnumerator CountCoroutine(System.IObserver<long> observer)
    {
        long current = 0;
        float deltaTime = 0;


        // while(true)로 코루틴을 돌려도 Dispose로 코루틴을 직접 중지 시킬 수 있다.
        while (true)
        {
            if (isPauesd == false)
            {
                deltaTime += Time.deltaTime;

                if(deltaTime >= 1.0f)                               // 누적된 deltaTime이 1초를 넘으면 정수 부분을 통지
                {
                    var integerPart = (int)Mathf.Floor(deltaTime);
                    current += integerPart;
                    deltaTime -= integerPart;

                    observer.OnNext(current);                       // 코루틴 내에서 직접 IObserver.OnNext를 발행사여 Subscribe에 통지
                }
            }

            yield return null;
        }
    }



    /// <summary>
    /// Stream을 Coroutine으로 변환하면 데이터의 변환과 필터링을 UniRx를 통해 처리한 후 최적화 된 값을 전달받아 코루틴에 넘길 수 있다.
    /// </summary>
    private void ConvertStreamToCoroutine()
    {
        StartCoroutine(WaitCoroutine());
    }

    /// <summary>
    /// ToYieldInstruction의 OnCompleted 메세지를 받아야만 코루틴 함수의 yield return 이 완료되고 다음 라인으로 넘어가기 때문에
    /// 만약 OnCompleted를 발급하지 않으면 무한 스트림에 걸려 코루틴 함수가 영원히 종료되지 않으므로 주의!!!
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitCoroutine()
    {
        CanvasLog.AddTextLine("Wait for 1 second.");
        yield return Observable.Timer(TimeSpan.FromSeconds(1)).ToYieldInstruction();    // Subscribe 대신 ToYieldInstruction()을 이용. 이 스트림이 완료될 때까지 코루틴을 대기시킴.

        CanvasLog.AddTextLine("Press Any Key");
        yield return this.UpdateAsObservable()
            .FirstOrDefault(_ => Input.anyKeyDown)                                      // FirstOrDefault: 조건 만족 시 OnNext, OnCompleted를 모두 발행
            .ToYieldInstruction();
        
        CanvasLog.AddTextLine("Pressed.");
    }




    private void FromCoroutine()
    {
        var cancel = Observable.FromCoroutine(CoroutineA)
            .SelectMany(CoroutineB)                         // SelectMany: 다른 스트림으로 교체
            .Subscribe(_ => CanvasLog.AddTextLine("Subscribe!"));

        /*
         * Result
         * Start A
         * Finish A
         * Start B
         * Finish B
         * Subscribe!
         */
    }

    IEnumerator CoroutineA()
    {
        CanvasLog.AddTextLine("Start A");
        yield return new WaitForSeconds(3.0f);
        CanvasLog.AddTextLine("Finish A");
    }

    IEnumerator CoroutineB()
    {
        CanvasLog.AddTextLine("Start B");
        yield return new WaitForSeconds(3.0f);
        CanvasLog.AddTextLine("Finish B");
    }



    /// <summary>
    /// Observable.WhenAll은 여러개의 스트림을 감시하며 모든 스트림의 OnCompleted가 발행 완료되는 순간 .Subscribe로 통지해주는 오퍼레이터
    /// </summary>
    private void WhenAll_FromCoroutine()
    {
        Observable.WhenAll(
            Observable.FromCoroutine<string>(x => CoroutineC(x)),
            Observable.FromCoroutine<string>(x => CoroutineD(x))
            ).Subscribe(xs =>
            {
                foreach (var x in xs)
                {
                    CanvasLog.AddTextLine("result: " + x);
                }
            }
        );


        /*
         * Result
         * Start A
         * Start B
         * Finish A
         * Finish B
         */
    }

    IEnumerator CoroutineC(IObserver<string> observer)
    {
        CanvasLog.AddTextLine("Start A");
        yield return new WaitForSeconds(3.0f);
        observer.OnNext("Finish A");
        observer.OnCompleted();
    }

    IEnumerator CoroutineD(IObserver<string> observer)
    {
        CanvasLog.AddTextLine("Start B");
        yield return new WaitForSeconds(1.0f);
        observer.OnNext("Finish B");
        observer.OnCompleted();
    }
}
