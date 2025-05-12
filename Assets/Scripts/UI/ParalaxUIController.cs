using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ParalaxUIController : MonoBehaviour
{
    [SerializeField] private bool followCamera;

    [SerializeField] private Canvas canvas;


    private IPlayerView playerView;
    [SerializeField] private float xOffset;
    [SerializeField] private float maxDistance;

    private bool allowFollow = true;
    private Transform cameraTransform;
    private float cameraFollowSpeed = 0.5f;
    private bool useTween = false;

    [Inject]
    public void Construct(IPlayerView playerView)
    {
        this.playerView = playerView;
        Init();
    }
    private void Init()
    {
        cameraTransform = Camera.main.transform;
        canvas.worldCamera = Camera.main;
        transform.SetParent(cameraTransform);
        transform.localPosition = Vector3.zero;
    }
    private void Update()
    {
        if (useTween)
        {
            if (allowFollow)
            {
                if (Mathf.Abs(cameraTransform.position.x - (playerView.Transform.position.x + xOffset)) >= maxDistance)
                {
                    allowFollow = false;
                    Sequence sequence = DOTween.Sequence();
                    sequence.Join(cameraTransform.DOMoveX(playerView.Transform.position.x + xOffset, cameraFollowSpeed));

                    sequence.AppendCallback(() => { allowFollow = true; });
                }
            }
        }
        else if(playerView != null)
            cameraTransform.position = new(playerView.Transform.position.x + xOffset, cameraTransform.position.y, cameraTransform.position.z);
    }
}
