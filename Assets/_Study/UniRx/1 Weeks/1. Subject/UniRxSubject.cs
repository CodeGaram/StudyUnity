using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


/// <summary>
/// Subject<T> 형태로 원하는 자료형을 Subject 타입의 인스턴스로 생성하면 UniRx의 스트림을 사용할 수 있다.
/// </summary>
public class UniRxSubject : MonoBehaviour
{
    void Start()
    {
        //Example_Subject();
        ChangeEventToSubject();
    }

    /// <summary>
    /// string을 스트림 객체로서 사용하기 위해 Subject 타입으로 생성.
    /// 
    /// 객체 하나에 Subscribe를 3번 선언한 후 OnNext()로 메세지를 전달하면 그 때 Subscribe에 지정한 람다 함수가 작동.
    /// </summary>
    private void Example_Subject()
    {
        Subject<string> subject = new Subject<string>();    //Subject 타입의 string 객체 생성

        // 메세지 발생 시 3회 출력
        subject.Subscribe(msg => CanvasLog.AddTextLine("Subscribe1: " + msg));
        subject.Subscribe(msg => CanvasLog.AddTextLine("Subscribe2: " + msg));
        subject.Subscribe(msg => CanvasLog.AddTextLine("Subscribe3: " + msg));

        // 이벤트 메세지 전달
        subject.OnNext("Hello");
        subject.OnNext("Hi");

        /* 결과
         * Subscribe1: Hello
         * Subscribe2: Hello
         * Subscribe3: Hello
         * 
         * Subscribe1: Hi
         * Subscribe2: Hi
         * Subscribe3: Hi
         */
    }

    private void ChangeEventToSubject()
    {
        TimerView_Event();
        TimerView_UniRx();
        TimerClass();
    }

    public delegate void TimerEventHandler(int time);   // 이벤트 핸들러
    public event TimerEventHandler eventTimer;          // 이벤트 핸들러 Delegate에 보낼 이벤트

    public Subject<int> subjectTimer = new Subject<int>();          // Subject

    private void TimerClass()
    {
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        int time = 1;
        while(time <= 100)
        {
            eventTimer(time);                       // Event
            subjectTimer.OnNext(time);              // Subject

            time++;

            yield return new WaitForSeconds(1);
        }
    }

    private void TimerView_Event()
    {
        eventTimer += x =>
        {
            CanvasLog.AddTextLine("Event: " + x.ToString());
        };
    }

    private void TimerView_UniRx()
    {
        subjectTimer.Subscribe(x =>
        {
            CanvasLog.AddTextLine("Subscribe: " + x.ToString());
        });
    }
}
