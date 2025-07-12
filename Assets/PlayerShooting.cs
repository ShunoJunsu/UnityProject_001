using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerShooting : MonoBehaviour
{
    float timer;
    LineRenderer line;
    [SerializeField]
    Transform ShootPoint;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        //ShootPoint = transform.Find("ShootPoint");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timer >= 0.2f)
        {
            timer = 0;
            Shoot();
        }
        if (timer >= 0.1f)
        {
            line.enabled = false;
        }
    }

    async void Shoot()
    {
        Ray ray;
        RaycastHit hit;
        Vector3 currentPos = ShootPoint.position;

        for (int i = 0; i < 10; i++)
        {
            ray = new Ray(currentPos, Camera.main.ScreenPointToRay(Input.mousePosition).direction);
            if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Shootable")))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.Damage(20);
                    Debug.Log("Damaged 20!");
                    return;
                }

                line.enabled = true;
                line.SetPosition(0, currentPos);
                line.SetPosition(1, hit.point);
            }
            else
            {
                line.enabled = true;
                line.SetPosition(0, currentPos);
                line.SetPosition(1, currentPos + ray.direction * 10);
                currentPos = currentPos + ray.direction * 10;
            }
            await Task.Delay(100);
        }
    }
}
