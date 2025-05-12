using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] private Material backgroundMaterial;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float baseScrollSpeed = 0.1f;
    [SerializeField] private RawImage backgroundImage; 
    private IPlayerView playerView;
    private Vector2 uvOffset;
    [Inject]
    public void Construct(IPlayerView playerView)
    {
        this.playerView = playerView;
    }

    private void Update()
    {
        backgroundMaterial = backgroundImage != null ? backgroundImage.material : spriteRenderer.material;
        if (playerView == null) return;
        float speedFactor = playerView.Rigidbody2D.linearVelocityX * baseScrollSpeed;

        Vector2 offset = backgroundMaterial.mainTextureOffset;
        offset.x += speedFactor * Time.deltaTime; // Scroll based on movement
        backgroundMaterial.mainTextureOffset = offset;
    }
}
