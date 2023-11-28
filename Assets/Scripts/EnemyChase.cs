using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBehavior
{
    private void OnDisable()
    {
        enemy.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled && !this.enemy.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach(Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (enemy.target.position - newPosition).sqrMagnitude;

                if(distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }
            enemy.movement.SetDirection(direction);

        }
    }

}
