using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_change : MonoBehaviour
{
    public static int firstChange;
    public static int secondChange;
    public static int thirdChange;
    public static int firstComplication;
    public static int secondComplication;
    public static int endGame;
    public static int level;

    public void Level(int levelNumber)
    {
        if (levelNumber == 1)
        {
            level = 1;
            firstChange = 1000000;
            secondChange = 1000000;
            thirdChange = 1000000;
            firstComplication = 10000000;
            secondComplication = 1000000;
            endGame = 1000;
        }
        else if (levelNumber == 2)
        {
            level = 2;
            firstChange = 750; 
            secondChange = 1000000; 
            thirdChange = 1000000;
            firstComplication = 850;
            secondComplication = 1000000;
            endGame = 2000;
        }
        else
        {
            level = 3;
            firstChange = 550; 
            secondChange = 1500; 
            thirdChange = 2300;
            firstComplication = 1000;
            secondComplication = 2000; 
            endGame = 3000;
        }

        SceneManager.LoadScene(1);
    }
}

