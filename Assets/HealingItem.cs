using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public AudioClip clipHeal;

    public Rigidbody myRigid;

    void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player collides to the item!");
            other.gameObject.GetComponent<PlayerHealth>().hp += 30;
            GetComponent<AudioSource>().PlayOneShot(clipHeal);
            Destroy(gameObject, 2);
            myRigid.AddForce(Vector3.up * 500);
        }
    }
}
