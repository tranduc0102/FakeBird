using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float positionXDespawn = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init(DataBullet data)
    {
        damage = data.damage;
        if (TryGetComponent(out SpriteRenderer sr))
        {
            sr.sprite = data.sprite;
        }
    }
    void Update()
    {
        rb.velocity = Vector3.right * speed * Time.deltaTime;
        if (positionXDespawn <= transform.position.x)
        {
            PoolingManager.Despawn(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            if (other.TryGetComponent(out Wall wall))
            {
                wall.TakeDamge(damage);
            }
           PoolingManager.Despawn(gameObject);
        }
    }
}
