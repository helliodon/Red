using R3;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IInputHandler
{
    public ReactiveProperty<float> MoveDirection { get; } = new ReactiveProperty<float>(0f);
    public ReactiveProperty<bool> JumpPressed { get; } = new ReactiveProperty<bool>(false);

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button jumpButton;

    private bool isLeftPressed;
    private bool isRightPressed;
    private void Start()
    {
        AddEventTrigger(leftButton.gameObject, EventTriggerType.PointerDown, _ => isLeftPressed = true);
        AddEventTrigger(leftButton.gameObject, EventTriggerType.PointerUp, _ => isLeftPressed = false);

        AddEventTrigger(rightButton.gameObject, EventTriggerType.PointerDown, _ => isRightPressed = true);
        AddEventTrigger(rightButton.gameObject, EventTriggerType.PointerUp, _ => isRightPressed = false);

        AddEventTrigger(jumpButton.gameObject, EventTriggerType.PointerDown, _ =>  JumpPressed.Value = true);
        AddEventTrigger(jumpButton.gameObject, EventTriggerType.PointerUp, _ => JumpPressed.Value = false);

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (isLeftPressed)
                MoveDirection.Value = -1f;
            else if (isRightPressed)
                MoveDirection.Value = 1f;
            else
                MoveDirection.Value = 0f;
        }).AddTo(this);
    }
    private void AddEventTrigger(GameObject obj, EventTriggerType type, System.Action<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((data) => action(data));
        trigger.triggers.Add(entry);
    }
}
