using UnityEngine;

public class Falling : MonoBehaviour
{
    private EndOfTheGame _endScript;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _endScript = collision.gameObject.GetComponent<EndOfTheGame>();
            StartCoroutine(_endScript.EndGame("TU\r\nZAUDĒJI"));
            Player.instance.rb.GetComponent<Collider2D>().enabled = false;
            Timer.Delta = 0;
        }
    }
}
