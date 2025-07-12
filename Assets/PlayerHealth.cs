using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 100;
    Vector3 posRespawn;

    bool bDamage;
    public Image imgDamage;

    void Start()
    {
        posRespawn = transform.position;
    }

    void Update()
    {
        if (bDamage)
        {
            imgDamage.color = new Color(1, 0, 0, 1);
        }
        else
        {
            imgDamage.color = Color.Lerp(imgDamage.color, Color.clear, Time.deltaTime);
        }

        bDamage = false;
    }

    public void Respawn()
    {
        hp = 100;
        transform.position = posRespawn;
        GetComponent<Animator>().SetTrigger("Respawn");

        GetComponent<PlayerController>().enabled = true;
        GetComponent<PlayerShooting>().enabled = true;
    }

    public void Damage(int amount)
    {
        if (hp <= 0) return;

        hp -= amount;
        bDamage = true;
        if (hp <= 0)
        {
            GetComponent<Animator>().SetTrigger("Death");

            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerShooting>().enabled = false;
            Invoke("Respawn", 3);
        }
    }
}
