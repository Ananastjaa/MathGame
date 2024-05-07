using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Operations : MonoBehaviour
{
    public static Operations Instance;
    public static List<char> OperationList;

    [SerializeField] private Toggle _multiplication, _division, _addition, _differnce;
    private static string _onMult = "Mul", _onDiv = "Div", _onAdd = "Add", _onDiff = "Diff";

    private void Start()
    {
        if(Instance == null) Instance = this;

        if(PlayerPrefs.GetInt(_onAdd) == 1) _addition.isOn = true;
        if (PlayerPrefs.GetInt(_onDiv) == 1) _division.isOn = true;
        if (PlayerPrefs.GetInt(_onDiff) == 1) _differnce.isOn = true;
        if (PlayerPrefs.GetInt(_onMult) == 2) _multiplication.isOn = false;
    }

    private void OnEnable()
    {
        Scene_settings_0.GameStarted += UpdateOperations;
    }

    private void OnDisable()
    {
        Scene_settings_0.GameStarted -= UpdateOperations;
    }

    public void UpdateOperations()
    {
        PlayerPrefs.SetInt(_onAdd, 2);
        PlayerPrefs.SetInt(_onMult, 2);
        PlayerPrefs.SetInt(_onDiv, 2);
        PlayerPrefs.SetInt(_onDiff, 2);

        OperationList = new List<char>();
        if (_multiplication.isOn)
        {
            OperationList.Add('*');
            PlayerPrefs.SetInt(_onMult, 1);
        }
        if (_division.isOn)
        {
            OperationList.Add(':');
            PlayerPrefs.SetInt(_onDiv, 1);
        }
        if (_addition.isOn)
        {
            OperationList.Add('+');
            PlayerPrefs.SetInt(_onAdd, 1);
        }
        if (_differnce.isOn)
        {
            OperationList.Add('-');
            PlayerPrefs.SetInt(_onDiff, 1);
        }
    }

    public bool AllBoxesOff()
    {
        return !_multiplication.isOn && !_differnce.isOn && !_division.isOn && !_addition.isOn;
    }
}
