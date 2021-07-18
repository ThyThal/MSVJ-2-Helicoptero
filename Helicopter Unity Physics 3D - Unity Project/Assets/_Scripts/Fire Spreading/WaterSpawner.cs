using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    [SerializeField] GameObject water;
    [SerializeField] float spawnRate = 0.05f;
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            Instantiate(water, transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
