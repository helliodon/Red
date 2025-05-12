using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private GameObject paralaxLevelUIPrefab;
    [SerializeField] private GameObject playerBallPrefab;

    public override void InstallBindings()
    {
        InitPlayer();
        InitParalax();
    }
    
    private void InitPlayer()
    {
        var playerObject = Container.InstantiatePrefab(playerBallPrefab);
        Container.Bind<PlayerView>().FromComponentOn(playerObject).AsSingle().NonLazy();

        var playerView = playerObject.GetComponent<IPlayerView>();
        Container.Bind<IPlayerView>().FromInstance(playerView).AsSingle().NonLazy();

        Container.Bind<PlayerPresenter>().AsSingle().NonLazy();
    }
    private void InitParalax()
    {
        Container.InstantiatePrefab(paralaxLevelUIPrefab);
    }
}