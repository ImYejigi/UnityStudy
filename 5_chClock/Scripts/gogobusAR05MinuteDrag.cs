using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class gogobus : MonoBehaviour
{
    public GameObject hourHand;
    public GameObject minuteHand;
    public int minValue = 0;
    public int maxValue = 59;
    public InputField inputTextHour;
    public InputField inputTextMinute;

    private int randomNum;
    [SerializeField]
    private GameObject timePin;
    [SerializeField]
    private Vector3 touchPos;
    public Transform target;

    public List<Transform> timeAngle = new List<Transform>();
    public List<float> angleDist = new List<float>();
    public List<int> minuteRandom = new List<int>();

    private int minIndex;
    // Start is called before the first frame update
    void Start()
    {
        randomNum = UnityEngine.Random.Range(0, 59);
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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.GetComponent<TimePinDummy>())
                {
                    timePin = hit.transform.GetComponent<TimePinDummy>().timePin;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            float posZ = Camera.main.WorldToScreenPoint(target.position).z;
            Vector3 touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, posZ);
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(touchPos);
            target.position = dragPos;

            if (timePin)
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
    }

}
