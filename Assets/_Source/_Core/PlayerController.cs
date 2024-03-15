using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private DialogueManager dialogueManager;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (dialogueManager.inDialogue)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        var direction = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = Vector2.right * (direction * movementSpeed);
    }
}