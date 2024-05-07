using System;
using UnityEngine;

public class RightAnswer : MonoBehaviour
{
    public static Action ScoreUdated;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Player.instance.rb.velocity.y <= 0)
        {
            ScoreManager.Score += 50;
            Coins.CoinCount += 5;
            ScoreUdated?.Invoke();
            Destroy(gameObject);
        }
    }
}
