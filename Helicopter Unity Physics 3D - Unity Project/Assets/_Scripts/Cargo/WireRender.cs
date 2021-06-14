using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRender : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform[] transforms;

    private bool canRender;
    private Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[transforms.Length];
        if (positions.Length >= 2)
        {
            canRender = true;
        }
        else
        {
            canRender = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canRender)
        {
            for (int i = 0; i < transforms.Length; i++)
            {
                positions[i] = transforms[i].position;
            }
            line.SetPositions(positions);
        }
    }
}
