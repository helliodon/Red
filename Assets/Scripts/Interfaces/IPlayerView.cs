using System.Numerics;
using UnityEngine;

public interface IPlayerView
{
    public Rigidbody2D Rigidbody2D { get; }
    public CircleCollider2D CircleCollider2D { get; }

    public Transform Transform { get; }
}