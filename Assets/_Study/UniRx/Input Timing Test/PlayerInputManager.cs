
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeGaram.Study.UniRx
{
    public class PlayerInputManager : MonoBehaviour
    {
        PlayerInput playerInput;
        [SerializeField] Timer timer;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            InputActionMap inputActionMap = playerInput.actions.FindActionMap("Player");
            InputAction inputAction = inputActionMap.FindAction("Attack");

            inputAction.started += OnMouseClicked_InputAction;

            OnMouseClicked_UniRx();
        }

        public void OnMouseClicked_InputAction(InputAction.CallbackContext context)
        {
            timer.OutputTimer("Event Callback");
        }

        public void OnMouseClicked_UniRx()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => timer.OutputTimer("UniRx"));
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                timer.OutputTimer("Input");
            }
        }

        public void OnMouseClicked_UnityEvent(InputAction.CallbackContext context)
        {
            if(context.started == true)
                timer.OutputTimer("Unity Event");
        }
    }
}