using UnityEngine;

public class EnemyScatter : EnemyBehavior
{
    private void OnDisable()
    {
        enemy.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && enabled && !enemy.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections.Count > 1 && node.availableDirections[index] == -enemy.movement.direction)
            {
                index++;
                if(index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            enemy.movement.SetDirection(node.availableDirections[index]);
        }
    }

}
