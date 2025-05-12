using R3.Triggers;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField]
    private CircleCollider2D collider;
    [SerializeField]
    private Rigidbody2D rigidBody;

    public Rigidbody2D Rigidbody2D => rigidBody;
    public CircleCollider2D CircleCollider2D => collider;

    public Transform Transform => transform;
}
