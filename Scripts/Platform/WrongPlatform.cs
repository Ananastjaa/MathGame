using UnityEngine;

public class WrongPlatform : MonoBehaviour
{
    [SerializeField] private EndOfTheGame _endObject;
    [SerializeField] private Animator _anim;

    private EndOfTheGame _endScript;

    private void OnEnable()
    {
        ScoreManager.PlayerWin += DisableFall;
    }

    private void OnDisable()
    {
        ScoreManager.PlayerWin -= DisableFall;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Player.instance.rb.velocity.y <= 0)
        {
            Player.instance.rb.GetComponent<Collider2D>().enabled = false;
      
            if (ScoreManager.Score < Level_change.secondChange)
            {
                _anim.SetTrigger("Lose");
            }
            else
            {
                _anim.SetTrigger("LoseKosmos");
            }

            collision.gameObject.GetComponent<Animator>().SetTrigger("LoseTrigger");
            Timer.Delta = 0;
            _endScript = collision.gameObject.GetComponent<EndOfTheGame>();
            StartCoroutine(_endScript.EndGame("TU\r\nZAUDĒJI"));
        }
    }

    private void DisableFall()
    {
        var script = gameObject.GetComponent<WrongPlatform>();
        Destroy(script);
    }
}
