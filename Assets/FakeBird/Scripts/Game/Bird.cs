using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float forceJump;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private RuntimeAnimatorController[] _runtimeAnimatorControllers;
    public float maxHeight;
    public float minHeight;

    private void Awake()
    {
        _animator.runtimeAnimatorController = _runtimeAnimatorControllers[PlayerPrefs.GetInt("IDSelectPlayer", 0)];
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    { 
        if (transform.position.y > maxHeight)
        {
            transform.position = new Vector2(transform.position.x, maxHeight);
        }
        if(transform.position.y < minHeight)
        {
            transform.position = new Vector2(transform.position.x, minHeight);
        }
        if(GameManager.Instance.mode != ModeGame.Play) return;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * forceJump;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            GameManager.Instance.mode = ModeGame.EndGame;
            UIManager.Instance.UILose();
        }
    }
}
