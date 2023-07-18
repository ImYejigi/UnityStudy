using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GogobusAR26BallDetect : MonoBehaviour
{
    //

    public GameClearController gameClearController;



    public void OnTriggerExit(Collider other)
    {
        //other.GetComponent<Gogobus26State>().ChangeState();
        if (other.transform.name != "MainBall")
        {
            
          
        }

        
        
    }

    public void ResultSet()
    {
      
    }
}
