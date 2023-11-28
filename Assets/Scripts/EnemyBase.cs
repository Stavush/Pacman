using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : EnemyBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine("ExitTransition");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            enemy.movement.SetDirection(-enemy.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        enemy.movement.SetDirection(Vector2.up, true);
        enemy.movement.rigidbody.isKinematic = true;
        enemy.movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, inside.position, elapsed / duration);
            newPosition.z = position.z;
            enemy.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(inside.position, outside.position, elapsed / duration);
            newPosition.z = position.z;
            enemy.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }


        enemy.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        enemy.movement.rigidbody.isKinematic = false;
        enemy.movement.enabled = true;
    }

}
