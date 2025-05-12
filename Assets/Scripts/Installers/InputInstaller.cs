using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private GameObject inputButtonsPrefab;

    public override void InstallBindings()
    {
        var inputUI = Container.InstantiatePrefab(inputButtonsPrefab);
        Container.Bind<InputHandler>().FromComponentOn(inputUI).AsSingle().NonLazy();
        var input = inputUI.GetComponent<IInputHandler>();
        Container.Bind<IInputHandler>().FromInstance(input).AsSingle().NonLazy();
    }
}