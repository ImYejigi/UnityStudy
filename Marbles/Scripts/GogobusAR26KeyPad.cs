using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GogobusAR26KeyPad : MonoBehaviour
{
    public InputField resultInputFiled;
    public GameObject key;
    public string text;

    public void ChangeText()
    {
        resultInputFiled.text += text;
    }
    public void RemoveText()
    {
        resultInputFiled.text = "";
    }

}
