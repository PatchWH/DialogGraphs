using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStringVariable : MonoBehaviour
{
    public PWH.ScriptableArcitecture.StringVariable stringVar;
    public TMPro.TMP_InputField inputField;

    private void Start()
    {
        ValueChanged(inputField.text);
    }

    public void ValueChanged(string newValue)
    {
        stringVar.Value = newValue;
    }
}
