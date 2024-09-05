using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UniRx.Triggers;

public class UniRxOperator : MonoBehaviour
{
    private void Start()
    {
        //Used_Where();
        //Used_Select();
        Used_SelectMany();
    }


    /// <summary>
    /// Where 오퍼레이터는 IOobservable에서 흐르는 이벤트들 중에 필요 없는 이벤트를 걸러낼 수 있는 오퍼레이터
    /// </summary>
    private void Used_Where()
    {
        // 마우스 클릭하면 ClickMouse를 출력
        Observable.EveryUpdate()                                    // Update 되는 매 프레임을 스트림으로 보냄
            .Where(_ => Input.GetMouseButtonDown(0))                // Where 오퍼레이터로 GetMouseButtonDown(0)이 true 일 때만 전달
            .Subscribe(_ => CanvasLog.AddTextLine("ClickMouse"));   // 데이터 처리
    }

    private void Used_Select()
    {
        // 마우스 클릭하면 현재 마우스 좌표를 출력
        Observable.EveryUpdate()                                // Udpate 되는 매 프레임을 스트림으로 보냄
            .Where(_ => Input.GetMouseButtonDown(0))            // Where 오퍼레이터로 GetMouseButtonDown(0)이 true 일 때만 전달
            .Select(_ => Input.mousePosition)                   // 전달 받은 값에서 mousePosition을 전달
            .Subscribe(x => CanvasLog.AddTextLine($"{x}"));     // 데이터 처리
    }


    /// <summary>
    /// Select는 기존 스트림에서 값을 바꾸는 처리를 수행하는데 비해
    /// SelectMany는 새로운 스트림을 생성하여 본래의 스트림을 대체시킨다.
    /// </summary>
    private void Used_SelectMany()
    {
        Button button = GetComponentInChildren<Button>();

        button.OnPointerDownAsObservable()                          // button에 Pointer Down 이벤트 발생 Observable
            .SelectMany(_ => button.UpdateAsObservable())           // Update 되는 매 프레임 관찰하는 스트림을 새로 생성하여 대체
            .TakeUntil(button.OnPointerUpAsObservable())            // 중에 button의 Pointer Up이 오면 중단
            .Subscribe(_ => CanvasLog.AddText("pressing..."));  // 데이터 처리
    }




    /*
     * etc
     * 
     * 팩토리 메소드는 Observable 클래스에 팩토리 메소드 디자인 패턴 형태로 구현된 static 메소드들임.
     * 주로 Observable 스트림의 값을 발행하고 컨트롤하는 오퍼레이터들임.
     * 
     * Observable.Create: 직접 Observable 객체를 생성
     * Observable.Return: 1개의 메세지만 전달
     * Observable.Repeat: 메세지의 전달을 반복
     * Observable.Range: 지정된 수치의 메세지를 전달
     * Observable.Timer: 실행 후 몇 초 이후 메세지를 전달
     * Observable.Interval: 실행 후 몇 초 마다 메세지를 전달
     * Observable.Empty: OnCompleted를 즉시 전달
     * Observable.FromEvent: UnityEvent를 Observable로 변환
     * Observable.EveryUpdate: Update 이벤트처럼 매 프레임 메세지를 전달. (OnCompleted가 발행되지 않으므로 평상시에는 UpdateAsObservable 사용을 추천)
     * Observabel.FixedEveryUpdate: FixedUpdate 이벤트처럼 ""
     * 
     * 
     * 
     * 메세지 필터는 스트림에 흐르는 메세지들 중 원하는 메세지들만 걸러내주는 필터의 기능을 하는 오퍼레이터
     * 
     * Where: 조건식이 true인 것만 통과시킴 (다른 언어에서는 filter의 기능)
     * Distinct: 중복된 메세지들 제거
     * DistinctUntilChanged: 값이 변화했을 경우만 통과시킴
     * Throttle / ThrtottleFrame: 지정한 간격 내에 들어온 메세지 중 마지막 메세지만 통과시킴 (Throttle과 ThrottleFrame의 차이는 지정 조건이 시간이냐 프레임이냐)
     * First / FirstOrDefault: 가장 먼저 발생한 메세지만 전달하고 Observable을 완료시킴
     * Take: 지정한 개수만큼 메세지 전달
     * TakeUntil: 인자로 지정한 스트림에 지정한 메세지가 올 때까지 계속 메세지를 전달
     * TakeWhile: 조건이 true인 동안 계속 메세지를 전달
     * Skip: 지정한 개수만큼 메세지를 스킵
     * SkipUntil: 인자로 지정한 스트림에 지정한 메세지가 올 때까지 계속 메세지를 스킵
     * SkipWhile: 조건이 true인 동안 계속 메세지를 스킵
     * 
     * 
     * 
     * Observable의 스트림에 흐르는 메세지를 합성해주는 오퍼레이터
     * 
     * Amb: 복수의 Observable 중에서 가장 빨리 메세지가 온 Observable의 데이터를 선택
     * Zip: 복수의 Observable에 흐르는 메세지들을 합성 - 각 스트림 별로 1개씩 메세지를 뽑아서 합성
     * ZipLatest: 복수의 Observable에 흐르는 메세지들을 합성 - 각 스트림 별로 1개씩 최신의 메세지만을 뽑아서 합성
     * CombineLatest: 복수의 Observable에 흐르는 메세지들을 합성 - 각 스트림 별로 1개씩 최신의 메세지만을 합성하되, 사용된 메세지는 다음 메세지가 도달하지 전까지 버리지않고 재활용
     * WithLatestFrom: 2개의 Observable 중 하나의 주축으로 다른 Observable의 최신 값을 합성.
     * Merge: 복수의 Observable을 1개의 스트림으로 합성
     * Concat: 하나의 Observeable에 OnCompleted가 발생하여 종료되면 다른 Observable로 대체
     * SelectMany: 별도의 Observable을 생성하여 다른 Observable을 대체
     * Observable.Catch: 복수의 Observable을 성공할 때까지 차례로 실행 (Catch(IEnumerable<IObservable<T>>를 사용하여 차례대로 성공할 때까지 1개씩 실행)
     * 
     * 
     * 
     * Observable을 다른 형태로 변환시키는 오퍼레이터
     * 
     * ToReativeProperty: Observable을 ReactiveProperty로 변환
     * ToReadOnlpyReactiveProperty: Observable을 ReadOnlyReactiveProperty로 변환
     * ToYieldInstruction: Observable을 코루틴으로 변환 (OnCompleted가 올 때까지 코루틴 내에서 대기 시킬 수 있다.)
     * 
     * 
     * 
     * Observable상의 메세지의 합성과 연산을 담당하는 오퍼레이터
     * 
     * Scan: 현재 메세지의 값과 이전 메세지 결과를 이용해 누적 처리를 반복 (LINQ에서 Aggregate)
     * Buffer: 메세지를 지정한 개수만큼 묶어서 전달
     * 
     * 
     * 
     * Observable상의 메세지를 변환시키는 오퍼레이터
     * 
     * Select: 현재 스트림에서 처리하는 메세지를 다른 메세지로 대체
     * Cast<T>: 메세지의 형 변환
     * TimeInterval: 이전 메세지로부터 경과시간을 부여
     * TimeStamp: 메세지에 타임 스탬프를 추가
     * AsUnitObservable: 메세지를 Uint 형으로 변환 (Select(_ => Unit.Default)와 동일)
     * 
     * 
     * 
     * Observable에 OnCompleted 전달 시 작동하는 오퍼레이터
     * 
     * Repeat: OnCompleted 전달 시 다시 한 번 Subscribe를 대기 (무한 루프에 주의)
     * RepeatUntilDisable: OnCompleted 전달 시 다시 한 번 Subscribe를 대기하지만 지정한 GameObject가 Disable되면 Repeat가 중지 됨.
     * RepeatUntildestroy: OnCompleted 전달 시 다시 한 번 Subscrive를 대기하지만 지정한 GameObject가 destroy되면 Repeat가 중지 됨.
     * Finally: OnCompleted나 OnError 전달 시 처리
     */
}
