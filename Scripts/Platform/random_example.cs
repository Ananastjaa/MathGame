using UnityEngine;
using UnityEngine.UI;

enum Type
{
    multiplication = '*',
    division = ':',
    addition = '+',
    differnce = '-'
}

public class random_example : MonoBehaviour
{
    [SerializeField] private Text example;
    [SerializeField] private Text answerR;
    [SerializeField] private Text answerW1;
    [SerializeField] private Text answerW2;

    private int _answer;

    private void Start()
    {
        GetExample();
        GetAnswers();
    }
    private string NewExampleText(int a, int b, char type)
    {
        string str = a.ToString() + " " + type + " " + b.ToString();
        return str;
    }

    private void GetExample()
    {
        var operations = Operations.OperationList;
        var operation = operations[Random.Range(0, operations.Count)];

        if (operation == (char)Type.multiplication || operation == (char)Type.division) CreateAndSetExample(2, 10, operation);
        else CreateAndSetExample(3, 50, operation);
    }

    private void CreateAndSetExample(int min, int max, char operation)
    {
        int a = 0;
        int b = 0;

        while (a == 0 | a == A_B.first | a == A_B.second)
        {
            a = Random.Range(min, max + 1);
        }
        b = Random.Range(min, max + 1);

        if (operation == (char)Type.multiplication || operation == (char)Type.division) _answer = a * b;
        else _answer = a + b;

        A_B.first = a;
        A_B.second = b;

        if (operation == (char)Type.division || operation == (char)Type.differnce) (_answer, a) = (a, _answer);
        example.text = NewExampleText(a, b, operation);
    }



    private void GetAnswers()
    {
        answerR.text = _answer.ToString();

        //////////////////////////////////////////////////////////////////////////////

        int x = Random.Range(_answer - 10, _answer + 11);

        while (x == _answer | x <= 1)
        {
            x = Random.Range(_answer - 10, _answer + 11);
        }

        answerW1.text = x.ToString();

        ///////////////////////////////////////////////////////////////////////////////

        int y = Random.Range(_answer - 10, _answer + 11);

        while (y == _answer | y == x | y <= 1)
        {
            y = Random.Range(_answer - 10, _answer + 11);
        }

        answerW2.text = y.ToString();
    }
}
