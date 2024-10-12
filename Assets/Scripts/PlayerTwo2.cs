using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo2 : BasePlayerController, IAimable, IMoveable, IAttackable
{
    [SerializeField] GameObject explosionPrefab; 
    private Vector3 point;

    public Vector2 Position
    {
        get
        {
            return _aimPosition;
        }
        set
        {
            _aimPosition = value;

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(_aimPosition);
            float distance;

            if (groundPlane.Raycast(ray, out distance))
            {
                point = ray.GetPoint(distance);
                Debug.Log("XZ: " + point);
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
        Debug.Log("Move from " + this.name);
    }

    public void Attack(Vector2 position)
    {
        GameObject explosion = Instantiate(explosionPrefab, point, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(explosion, 2f);
    }
}
