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
        // Calculate the initial direction once
        Vector3 initialShootDirection = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Vector3 currentPos = ShootPoint.position;

        Ray ray;
        RaycastHit hit;

        for (int i = 0; i < 10; i++)
        {
            // Use the initial direction for the ray
            ray = new Ray(currentPos, initialShootDirection);

            if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Shootable")))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.Damage(20);
                    Debug.Log("Damaged 20!");
                    
                    // Show the line up to the hit point before returning
                    line.enabled = true;
                    line.SetPosition(0, ShootPoint.position); // Start from the actual shoot point
                    line.SetPosition(1, hit.point);
                    return;
                }

                line.enabled = true;
                line.SetPosition(0, currentPos);
                line.SetPosition(1, hit.point);
                currentPos = hit.point; // Update currentPos to the hit point for subsequent checks (if you want the ray to stop there)
                                        // If the ray should continue past a non-enemy "shootable" object, remove this line.
            }
            else
            {
                line.enabled = true;
                line.SetPosition(0, currentPos);
                line.SetPosition(1, currentPos + initialShootDirection * 10); // Use initial direction
                currentPos = currentPos + initialShootDirection * 10; // Update currentPos for the next step
            }
            await Task.Delay(100);
        }
    }
}
