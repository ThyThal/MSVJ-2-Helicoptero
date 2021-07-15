using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public float health = 100;
    public bool onFire = false;

    private void Update()
    {
        if (onFire)
            health -= 10 * Time.deltaTime;

        if (health <= 0) Destroy(gameObject);
    }
}
