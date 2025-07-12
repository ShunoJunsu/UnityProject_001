using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    int hp = 100;

    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void Damage(int amount)
    {
        if (hp <= 0) return;

        hp -= amount;
        if (hp <= 0)
        {
            GetComponent<Animator>().SetTrigger("EnemyDeath");
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<EnemyMoveFollow>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            Destroy(gameObject, 2);
            gameManager.GetComponent<EnemySpawn>().count--;
        }
    }
}
