using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClockARManager : MonoBehaviour
{
    public MaterialsChange materialsChange;
    public GameClearController gameClearController;
    public int minValue = 0;
    public int maxValue = 59;
    public InputField inputTextHour;
    public InputField inputTextMinute;

    [Range(0, 12)]
    public int hourTempCount;
    [Range(0, 60)]
    public int minuteTempCount;

    private GameObject timePin;

    public Transform target;
    private int hourtime = 12;
    private int minutetime;
    public List<Transform> timeAngle = new List<Transform>();
    public List<float> angleDist = new List<float>();
    public List<int> minuteRandom = new List<int>();

    private int minIndex;
    // Start is called before the first frame update
    void Start()
    {
        materialsChange.SadExpression();
        minuteRandom.Add(0);
        

        for (int i = 1; i < 12; i++)
        {
            int j = i * 5;
            minuteRandom.Add(j);
        }

        inputTextHour.text = UnityEngine.Random.Range(1, 13).ToString();
        inputTextMinute.text = minuteRandom[UnityEngine.Random.Range(0, 12)].ToString(); 

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.transform.GetComponent<TimePinDummy>())
                {
                    timePin = hit.transform.GetComponent<TimePinDummy>().timePin;
                    hit.transform.gameObject.SetActive(false);
                }
                else
                {
                    timePin = hit.transform.gameObject;
                }
            }
        }

        if(Input.GetMouseButton(0))
        {
            float posZ = Camera.main.WorldToScreenPoint(target.position).z;
            Vector3 touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, posZ);
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(touchPos);
            target.position = dragPos;

            if(timePin)
            {
                for (int i = 0; i < angleDist.Count; i++)
                {
                    angleDist[i] = Vector3.Distance(target.transform.position, timeAngle[i].position);
                }
                float minValue = angleDist.Min();
                minIndex = angleDist.IndexOf(minValue);

                Vector3 realationPos = new Vector3(timeAngle[minIndex].position.x, timeAngle[minIndex].position.y, 0) - new Vector3(timePin.transform.position.x, timePin.transform.position.y, 0);
                Quaternion rotation = Quaternion.LookRotation(realationPos);
                timePin.transform.rotation = rotation;

                //Vector3 relationpos = new Vector3(target.position.x, target.position.y, 0) - new Vector3(timePin.transform.position.x, timePin.transform.position.y, 0);
                //Quaternion rotation = Quaternion.LookRotation(relationpos);
                //timePin.transform.rotation = rotation;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(timePin);
            
            if(timePin.name == "HourCol")
            {
                
                hourtime = minIndex;
                if (minIndex == 0)
                {
                    hourtime += 12;
                }
                Debug.Log(hourtime);
            }
            if(timePin.name == "MinuteCol")
            {
                
                minutetime = minuteRandom[minIndex];
                Debug.Log(minutetime);
            }
            if(hourtime.ToString() == inputTextHour.text && minutetime.ToString() == inputTextMinute.text)
            {
                materialsChange.WinkExpression();
                Success();
            }
            timePin.GetComponent<BoxCollider>().enabled = true;
            timePin = null;
        }

       
    }

    void Success()
    {
        gameClearController.UpdateClearCount();
    }

    
}
