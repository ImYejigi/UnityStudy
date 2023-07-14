using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GogobusAR26BallDetect : MonoBehaviour
{
    public GameClearController gameClearController;
    public InputField marbleResult;
    public int objCount = 15;

    public void OnTriggerExit(Collider other)
    {
        objCount--;
        Debug.Log(objCount);
    }
    public void ResultSet()
    {
        Debug.Log(objCount);
        if(objCount.ToString() == marbleResult.text)
        {
            gameClearController.UpdateClearCount();
        }
    }
}
