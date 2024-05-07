using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Rigidbody2D rb;

    [SerializeField] private Transform _cameraTransform;

    private float _moveInput;
    private float _speed = 4.5f;

    private bool _facingRight = true;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        _moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(_moveInput * _speed, rb.velocity.y);
        Flip();
    }

    void Flip()
    {
        if (_moveInput < 0 & _facingRight == true | _moveInput > 0 & _facingRight == false)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;

            _facingRight = !_facingRight;
        }
    }
}
