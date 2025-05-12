using R3;

public interface IInputHandler
{
    public ReactiveProperty<float> MoveDirection { get; }
    public ReactiveProperty<bool> JumpPressed { get; }
}