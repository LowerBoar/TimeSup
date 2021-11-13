using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player target;

    void Start()
    {
        
    }

    void Update()
    {
        if (target == null) {
            return;
        }

        transform.rotation = Math.LookAt2D(transform.position, target.transform.position);
    }

    public void SetTarget(Player newTarget)
    {
        target = newTarget;
    }
}
