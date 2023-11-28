using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Movement movement { get; private set; }
    public EnemyBase enemyBase { get; private set; }
    public EnemyChase chase { get; private set; }
    public EnemyScatter scatter { get; private set; }
    public EnemyFrightened frightened { get; private set; }
    public EnemyBehavior initialBehavior;
    public Transform target;
    public int points = 200;

    private void Awake(){
        movement = GetComponent<Movement>();
        enemyBase = GetComponent<EnemyBase>();
        chase = GetComponent<EnemyChase>();
        scatter = GetComponent<EnemyScatter>();
        frightened = GetComponent<EnemyFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        frightened.Disable();
        chase.Disable();
        scatter.Enable();
        if(enemyBase != initialBehavior)
        {
            enemyBase.Disable();
        }
        if(initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (frightened.enabled)
            {
                FindObjectOfType<GameManager>().EatEnemy(this);
                Debug.Log("Pacman ate enemy!");
            }
            else
            {
                FindObjectOfType<GameManager>().EatPacman();
                Debug.Log("Pacman eatten by enemy!");
            }
        }
    }

}
