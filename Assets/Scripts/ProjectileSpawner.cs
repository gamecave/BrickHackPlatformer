using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    float xCoord = 10;
    float timer = 0.5f;

    [SerializeField] GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            SpawnProjectile();
            timer = 0.5f;
        }
    }

    /// <summary>
    /// Spawns a projectile at a random location every 0.5 seconds
    /// </summary>
    void SpawnProjectile()
    {
        float x = Random.Range(-xCoord, xCoord);
        float y = 4f;

        Instantiate(projectile, new Vector2(x, y), this.transform.rotation);
    }
}
