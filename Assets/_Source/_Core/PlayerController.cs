using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var direction = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = Vector2.right * (direction * movementSpeed);
    }
}
