using UnityEngine;

public class Platform : MonoBehaviour
{
    public static float JumpForce;

    private void Start()
    {
        if (ScoreManager.Score == 0) JumpForce = 9.3f;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Player.instance.rb.velocity.y <= 0)
        {
            Player.instance.rb.velocity = Vector2.up * JumpForce;
            FirstComplication.CheckAndComplicate(gameObject);
        }
    }
}
