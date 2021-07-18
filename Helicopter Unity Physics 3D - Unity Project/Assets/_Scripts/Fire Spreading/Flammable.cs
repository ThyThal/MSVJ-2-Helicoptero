using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    public float health = 100;
    [Range(0.01f, 2)] public float combustionRate = 1;

    private bool _onFire = false;
    private bool _isWet = false;
    private FireScript _fireScript = null;

    public bool OnFire { get => _onFire; }
    public bool IsWet { get => _isWet; }

    private void Update()
    {
        if (_onFire)
        {
            if (health <= 0)
                Destroy(gameObject);

            health -= 10 * combustionRate * Time.deltaTime;
        }
    }

    public void Ignite(FireScript fire)
    {
        if (_isWet) return;

        _onFire = true;
        this._fireScript = fire;
    }

    public void Extinguish()
    {
        if (!_onFire) return;

        if (_fireScript != null)
        {
            Destroy(_fireScript.gameObject);
            _fireScript = null;
        }
        
        _onFire = false;
        _isWet = true;
    }

    private void OnDrawGizmos()
    {
        if (_onFire)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawSphere(transform.position, 0.55f);
    }
}
