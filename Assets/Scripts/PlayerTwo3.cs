using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo3 : BasePlayerController, IAimable, IMoveable, IAttackable
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float dashSpeed = 15f; 
    [SerializeField] float minDistance = 1.0f; 
    private Vector3 targetPosition;
    private bool canMove = true; 

    public Vector2 Position
    {
        get { return _aimPosition; }
        set
        {
            _aimPosition = value;

            Plane groundPlane = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(_aimPosition);
            float distance;

            if (groundPlane.Raycast(ray, out distance))
            {
                targetPosition = ray.GetPoint(distance);

                if (Vector3.Distance(transform.position, targetPosition) > minDistance)
                {
                    canMove = true;
                }
                else
                {
                    canMove = false; 
                }

                MoveTowardsTarget();
            }

            Debug.Log("Aim from " + this.name);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Child Awake");
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log("Child Start");
    }

    public void Move(Vector2 direction)
    {
        
    }

    public void Attack(Vector2 position)
    {
        
    }

    private void MoveTowardsTarget()
    {
        if (canMove)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Embestida
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            if (distanceToTarget > minDistance)
            {
                transform.position += direction * dashSpeed * Time.deltaTime; 
            }
        }
    }

}
