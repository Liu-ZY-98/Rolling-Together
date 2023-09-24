using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    public Vector3 Movement;

    public float Speed;

    private Vector3 targetPosition;

    private bool triggerMove;

    public void Awake()
    {
        targetPosition = transform.position + Movement;
        triggerMove = false;
    }

    public void Update()
    {
        if (triggerMove)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (Vector3.Distance(transform.position, targetPosition) < 0.0001f)
            {
                triggerMove = false;
            }
        }
    }

    public void Move()
    {
        triggerMove = true;
    }
}
