using UnityEngine;

public class PlayerTwo : BasePlayerController, IAimable, IMoveable, IAttackable
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    [SerializeField] float bulletSpeed = 10f;

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
        Debug.Log("Attacking towards: " + point);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector3 direction = point - firePoint.position;

        direction.y = 0f; 

        direction = direction.normalized;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction * bulletSpeed;

        Debug.Log("Bullet velocity set to: " + rb.velocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point, 0.2f); //referencia 
    }
}
