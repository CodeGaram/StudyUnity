using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// OnNext(T value): 일반적인 이벤트 발생 시 통지
/// OnError(Exception error): 스트림 처리 중 예외 발생 시 통지
/// OnCompleted(): 스트림 종료 통지
/// 
/// Subscribe()는 이 메세지들 중 원하는 것만 받아서 처리할 수 있게 각 상황별로 오버로드 되어있음.
/// Subscribe(IObserver observer): 기본형
/// Subscribe(): 모든 메세지 무시
/// Subscribe(Action onNext): OnNext만 처리
/// Subscribe(Action onNext, Action onError)
/// Subscribe(Action onNext, Action onCompleted)
/// Subscribe(Action onNext, Action onError, Action onCompleted)
/// </summary>
public class UniRxIObserverIObservable : MonoBehaviour
{
    void Start()
    {
        //Example_OnNext();
        //Example_OnError();
        //Example_SubscribeAfterOnError();
        //Example_OnCompleted();
        //Example_Dispose();
        Example_DisposeMoreStream();
    }

   
    /// <summary>
    /// OnNext()가 호출될 때마다 정수형 이벤트를 Subscribe에 통지해줌
    /// </summary>
    private void Example_OnNext()
    {
        var intTest = new Subject<int>();

        intTest.Subscribe(x => CanvasLog.AddTextLine(x));
        intTest.OnNext(2);
        intTest.OnNext(50);
        intTest.OnNext(100);

        /*
         * result
         * 2
         * 50
         * 100
         */
    }



    /// <summary>
    /// OnError는 스트림 처리 중 예외가 발생하면 통지됨.
    /// 에러 처리가 필요 없다면 생략 가능.
    /// 
    /// OnError 메세지가 Subscribe에 도달하면 해당 스트림은 바로 구독이 종료됨.
    /// </summary>
    private void Example_OnError()
    {
        var stringSubject = new Subject<string>();

        stringSubject
            .Select(str => int.Parse(str))                    // int.Parse 불가능 시 예외 발생
            .Subscribe(
                x => CanvasLog.AddTextLine("성공: " + x),     // OnNext()
                ex => CanvasLog.AddTextLine("예외: " + ex)    // OnError(), 생략하면 예외처리 안함
            );

        stringSubject.OnNext("1");
        stringSubject.OnNext("2");
        stringSubject.OnNext("Hello");
        stringSubject.OnNext("4");

        /*
         * Result
         * 성공: 1
         * 성공: 2
         * 예외: System.FormatException : Input string was not in the correct format
         */
    }



    /// <summary>
    /// OnErrorRetry()는 예외를 중간에서 Catch 한 후 스트림을 재구축(Subject에 IObserver를 다시 등록)하여 구독을 계속 이어가게 해줌.
    /// 
    /// Retry: OnError 발생 시 다시 Subscribe.
    /// Catch: OnError 발생 시 예외 처리 후 다른 스트림으로 대체.
    /// CatchIgnore: OnError 발생 시 예외 처리 후 OnError를 무시하고 OnCompleted로 대체.
    /// OnErrorRetry: OnError 발생 시 예외 처리 후 다시 Subscribe (시간 지정 가능).
    /// </summary>
    private void Example_SubscribeAfterOnError()
    {
        var stringSubject = new Subject<string>();

        stringSubject
            .Select(str => int.Parse(str))
            .OnErrorRetry((FormatException ex) =>                   // 예외 정보의 필터링 가능
            {
                CanvasLog.AddTextLine("예외 발생하여 다시 실행");
            })
            .Subscribe(
                x => CanvasLog.AddTextLine("성공: " + x),
                ex => CanvasLog.AddTextLine("예외: " + ex)
            );


        stringSubject.OnNext("1");
        stringSubject.OnNext("2");
        stringSubject.OnNext("Hello");
        stringSubject.OnNext("4");
        stringSubject.OnNext("5");

        /*
         * Result
         * 성공: 1
         * 성공: 2
         * 예외 발생하여 다시 실행
         * 성공: 4
         * 성공: 5
         */
    }



    /// <summary>
    /// OnCompleted는 스트림이 완료 되었으므로 이후로는 메세지를 발행하지 않는다 라는 사실을 통지하는 메세지.
    /// Subscribe에 OnCompleted가 통지되면 해당 스트림은 바로 구독이 종료됨.
    /// 
    /// 이렇게 구독이 종료된 Subject는 재사용이 불가능하며 Subscribe를 호출해도 바로 OnCompleted가 발생하게 됨.
    /// </summary>
    private void Example_OnCompleted()
    {
        var subject = new Subject<int>();

        subject.Subscribe(
            x => CanvasLog.AddTextLine(x),
            () => CanvasLog.AddTextLine("OnCompleted")
        );

        subject.OnNext(1);
        subject.OnNext(2);
        subject.OnCompleted();

        subject.Subscribe(
            x => CanvasLog.AddTextLine(x),
            () => CanvasLog.AddTextLine("OnCompleted")
        );

        subject.OnNext(3);
        subject.OnNext(4);

        /*
         * Result
         * 1
         * 2
         * OnCompleted
         * OnCompleted
         */
    }



    /// <summary>
    /// Subscribe의 반환값인 IDisposable은 GC가 관리하지 않는 리소스임.
    /// IDisposable.Dispose()로 해제
    /// 
    /// 
    /// </summary>
    private void Example_Dispose()
    {
        var subject = new Subject<int>();

        var disposable = subject.Subscribe(
            x => CanvasLog.AddTextLine(x),
            () => CanvasLog.AddTextLine("OnCompleted")
        );

        subject.OnNext(1);
        subject.OnNext(2);

        disposable.Dispose();                           // => 구독은 종료되지만. OnCompleted를 전달하지 않음. 
                                                        // 때문에, 재구독 가능함.

        disposable = subject.Subscribe(
            x => CanvasLog.AddTextLine(x),
            () => CanvasLog.AddTextLine("OnCompleted")
        );

        subject.OnNext(3);
        subject.OnCompleted();

        /*
         * Result
         * 1
         * 2
         * 3
         * OnCompleted
         */
    }




    /// <summary>
    /// OnCompleted는 호출하는 Subject의 모든 스트림의 Subscribe가 중단
    /// Dispose는 Subject의 스트림 중 원하는 한 개의 스트림만 구독 중단
    /// </summary>
    private void Example_DisposeMoreStream()
    {
        var subject = new Subject<int>();

        var disposable1 = subject.Subscribe(
            x => CanvasLog.AddTextLine("스트림1: " + x),
            () => CanvasLog.AddTextLine("스트림1: OnCompleted")
        );

        var disposable2 = subject.Subscribe(
            x => CanvasLog.AddTextLine("스트림2: " + x),
            () => CanvasLog.AddTextLine("스트림2: OnCompleted")
        );

        subject.OnNext(1);
        subject.OnNext(2);

        disposable1.Dispose();

        subject.OnNext(3);
        subject.OnCompleted();

        /*
         * Result
         * 스트림1: 1
         * 스트림2: 1
         * 스트림1: 2
         * 스트림2: 2
         * 스트림2: 3
         * 스트림2: OnCompleted
         */
    }




    /// <summary>
    /// 기본적으로 스트림은 Subject에 포함되는 일부 또는 그 자체라고 볼 수 있으며 Subject가 destroy되면 스트림도 파기된다.
    /// 즉, Subject가 파기되지 않고 남아있다면 각각의 스트림들도 살아있다는 뜻
    /// 
    /// 때문에 스트림이 참조하고 있는 오브젝트가 스트림보다 먼저 destroy되면 해당 스트림의 처리는 정상적으로 이루어질 수 없음에도
    /// 여전히 살아남아 NullReferenceExcrption을 일으키거나 메모리릭 등 성능 저하와 치명적인 오류를 발생하는 원인이 될 수 있다.
    /// 
    /// 이런 경우 AddTo 오퍼레이트 사용 -> 해당 인스턴스가 destroy 되는 지를 감시하여 자동으로 Dispose를 호출하여 구독을 중단시킴.
    /// </summary>
    private void Sample_LifeCycle()
    {
        var subject = new Subject<int>();

        subject.Where(x => x > 5)
            .Subscribe(x =>
            {
                CanvasLog.AddTextLine(" ");
            })
            .AddTo(gameObject);
    }
}
