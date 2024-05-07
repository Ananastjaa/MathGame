using UnityEngine;

public class SecondComplication : MonoBehaviour
{
    public static void Complicate()
    {
        Player.instance.rb.gravityScale = 2f; 
        Platform.JumpForce += 3.8f;
    }
}
