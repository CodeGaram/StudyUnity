using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// IObserver에 OnNext(), OnError(), OnComplete가 선언되어 있음.        Observer: 관찰자
/// IObservable에 Subscribe()가 선언되어 있음.                          Observable: 관찰 가능한
/// 
/// IObserver가 OnNext()를 통해 IObservable의 Subscribe에 이벤트를 전달하는 개념.
/// 
/// 스트림에 이벤트를 흘려보내기 위해서 우선 관찰 가능한 객체(Observable)를 생성.
/// Observable 객체의 생성 방법은 여러가지 있음
/// 1. Subject 시리즈를 사용
/// 2. ReactiveProperty 시리즈를 사용
/// 3. Factory Method 시리즈를 사용
/// 4. UniRx.Triggers 시리즈를 사용
/// 5. Coroutine을 변환하여 사용
/// 6. UGUI 이벤트를 변환하여 사용
/// 7. 그 외 UniRX가 제공하는 기능들
/// </summary>
public class UniRxObservable : MonoBehaviour
{
    void Start()
    {
        //Used_Subject();
        //Used_ReactiveProperty();
        //Used_FactoryMethod();
        //Used_Triggers();
        //Used_Coroutine();
        //Used_UGUI();
        UsedOtherMethod();
    }

    /// <summary>
    /// Subject<T>:             가장 단순한 형태이지만 가장 자주 사용. OnNext로 이벤트를 전달.
    /// BehaviorSubject<T>:     마지막으로 전달한 이벤트를 캐싱하고 이후에 Subscribe 될 때 그 값을 전달.
    /// ReplaySubject<T>:       과거에 전달한 모든 이벤트를 캐싱하고 이후에 Subscribe 될 때 그 값을 모두 모아 전달.
    /// AsyncSubject<T>:        OnNext() 사용 시 즉시 전달하지 않고 내부에 캐싱한 후 OnCompleted가 실행되면 마지막 OnNext()를 전달.
    /// </summary>
    private void Used_Subject()
    {
        
    }




    public IntReactiveProperty IntReactiveProperty;

    /// <summary>
    /// ReactiveProperty는 int나 float과 같은 일반 변수 자료형에 Subject의 기능을 붙인 것.
    /// 
    /// Inspectable ReactiveProperty를 사용하여 유니티 인스펙터에 출력시킬 수 있음.
    /// [Serializeable] public class IntReactiveProperty : ReactiveProperty<int>의 형태이기 때문에 똑같이 사용 가능.
    /// </summary>
    private void Used_ReactiveProperty()
    {
        var rp = new ReactiveProperty<int>(10);     // 초기 값 지정 가능

        // .Value를 사용하면 일반 변수처럼 대입하거나 값을 읽을 수 있음.
        rp.Value = 5;
        var currentValue = rp.Value;

        // Subscribe도 사용할 수 있음.
        rp.Subscribe(x => CanvasLog.AddTextLine(x));

        // 값을 수정하면 즉시 OnNext()가 발생하여 Subscribe로 통보.
        rp.Value = 10;

        /* 결과
         * 5
         * 10
         */
    }




    /// <summary>
    /// 팩토리 메소드의 경우 Subject 기반의 방법들에 비해 복잡한 스트림을 쉽게 만들 수 있는 장점.
    /// 하지만 UniRx 자체에 간편한 생성 기능 방법이 다양하기 때문에 자주 사용하지는 않음.
    /// 
    /// 대표적으로 Observable.Create, Observable.Start, Observable.Timer/TimerFrame 등이 있음
    /// 
    /// 참고: https://reactivex.io/documentation/operators.html#creating
    /// </summary>
    private void Used_FactoryMethod()
    {

    }




    /// <summary>
    /// using UniRx.Triggers를 추가해야 함
    /// 
    /// Update 콜백을 대체하는 가장 편리한 방법
    /// Unity의 콜백 이벤트를 UniRx의 IObservable로 변환하여 제공하며, GameObject에 귀속되어 있어 Destroy시점에서 Dispose되고 OnCompleted를 자동으로 발행하기 때문에 수명 관리를 해줄 필요가 없음.
    /// 
    /// UpdateAsObservable()은 ObservableUpdateTrigger 클래스에 속해있으며, UpdateAsObservable()을 호출하면 Object에 자동으로 ObservableUpdateTrigger가 AddComponent된다.
    /// 
    /// 참고: https://github.com/neuecc/UniRx/wiki/UniRx.Triggers
    /// </summary>
    private void Used_Triggers()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => CanvasLog.AddTextLine("Update"));
    }




    /// <summary>
    /// UniRx로 복잡한 스트림을 처리하기 위해 많은 오퍼레이터들을 중첩해서 사용하는 것보다
    /// 적절히 코루틴을 병용해 절차적으로 구현하면 심플한 구조를 만들 수 있는 경우가 많음.
    /// 
    /// Coroutine에서 IObservable로 변환하려면 Observable.FromCoroutine을 이용.
    /// </summary>
    private void Used_Coroutine()
    {
        Observable.FromCoroutine<int>(observer => GameTimerCoroutine(observer, 60))
            .Subscribe(t => CanvasLog.AddText(t + " "));
    }

    // 0이 될 때까지 1초마다 카운트다운하는 코루틴 함수
    private IEnumerator GameTimerCoroutine(IObserver<int> observer, int initialCount)
    {
        var count = 100;
        while(count > 0)
        {
            observer.OnNext(count--);

            yield return new WaitForSeconds(1);
        }

        observer.OnNext(0);
        observer.OnCompleted();
    }




    /// <summary>
    /// UGUI의 컴포넌트 기능을 가져올 수 있음.
    /// 
    /// 위의 ReactiveProperty와 조합하면 MVP(Model-View-Persenter) 패턴을 매우 쉽게 적용할 수 있음.
    /// </summary>
    private void Used_UGUI()
    {
        // UGUI의 기본 이벤트와 동일한 이름의 Observable이 구현되어 있음.
        var button = GetComponentInChildren<Button>();
        button.OnClickAsObservable().Subscribe(_ => CanvasLog.AddTextLine("Button OnClick!"));   // Button:OnClick()에 해당하는 Observable 생성 함수

        var inputField = GetComponentInChildren<InputField>();
        inputField.OnValueChangedAsObservable().Subscribe(text => CanvasLog.AddTextLine($"InputField changed to {text}"));
        inputField.OnEndEditAsObservable().Subscribe(text => CanvasLog.AddTextLine($"InputField edit end to {text}"));

        var slider = GetComponentInChildren<Slider>();
        slider.OnValueChangedAsObservable().Subscribe(value => CanvasLog.AddText(value));
    }






    private void UsedOtherMethod()
    {
        /*
         * ObservableWWW
         * 
         * Unity의 WWW를 스트림으로 취급할 수 있도록 만든 것
         * 코루틴을 사용하지 않고 비동기 다운로드 처리를 할 수 있어 매우 유용했던 기능
         * 
         * 유니티에서 WWW가 Deprecate 되면서 잘 사용하지 않음
         */


        Observable.NextFrame()                                                  // 다음 프레임에 메세지를 전달하는 Observable을 생성
            .Subscribe(_ => CanvasLog.AddTextLine("다음 프레임에 실행된다"));

        Observable
            .EveryUpdate()                              // 매 프레임 호출
            .Subscribe(x => CanvasLog.AddText(x + " "));  // x == Time.countFrame
        /*
         * Observable.EveryUpdate()는 static 메소드이므로 MonoBehaviour 상속 클래스가 아닌 외부에서도 호출 가능
         * 
         * UniRx.Triggers의 UpdateAsObservable과 다르게 특정 GameObject에 종속되어 있지 않기 때문에
         * Dispose 시점을 수동으로 지정해줘야 하므로 Subscribe 또는 AddTo 오퍼레이터를 사용해 관리해야 함.
         * 
         * 일반적으로 특정 오브젝트에 종속되지 않는 상황
         * 예를 들어 FPS 출력, 마우스 클릭 체크, 네트워크 체크 등의 경우에 유용
         * 
         * EveryUpdate, EveryFixedUpdate, EveryEndOfFrame, EveryLateUpdate, EveryAfterUpdate, EveryGameObjectUpdate 등
         */
    }
}
