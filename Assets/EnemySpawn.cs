using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject obj;
    public float time = 5f;
    public Transform[] point;

    public int Max = 3;
    public int count = 1;

    void Start()
    {
        InvokeRepeating("Create", time, time);
    }

    void Update()
    {

    }
    
    void Create()
    {
        if (count >= 3) return;

        count++;
        Instantiate(obj, point[Random.Range(0, point.Length)]);
    }
}
