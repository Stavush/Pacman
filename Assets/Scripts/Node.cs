using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public List<Vector2> availableDirections {  get; private set; }
    public LayerMask obstacleLayer;

    private void Start()
    {
        availableDirections = new List<Vector2>();

        CheckAvailableDirections(Vector2.up);
        CheckAvailableDirections(Vector2.left);
        CheckAvailableDirections(Vector2.right);
        CheckAvailableDirections(Vector2.down);
    }

    private void CheckAvailableDirections(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, obstacleLayer);
        if (hit.collider == null)
        {
            this.availableDirections.Add(direction);
        }
    }
}
