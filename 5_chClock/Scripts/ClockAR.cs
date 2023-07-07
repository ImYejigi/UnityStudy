using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ClockAR : MonoBehaviour
{
    public ARRaycastManager raycastManager;//AR레이캐스트 매니저
    public GameObject arPrefab;//AR프리팹
    private GameObject arObject;//생성되는 AR오브젝트 


    public GameObject hourHand;
    public GameObject minuteHand;
    public int randomHour;
    public int randomMinute;
    public InputField inputTextHour;
    public InputField inputTextMinute;
    private int inputValue;

    public int minValue = 0;
    public int maxValue = 59;

    [Range(0, 12)]
    public int hourTempCount;
    [Range(0,60)]
    public int minuteTempCount;
    // Start is called before the first frame update
    void Start()
    {
        RandomClock();
    }

    void RandomClock()
    {
        randomHour = Random.Range(1, 13);
        randomMinute = Random.Range(0, 60);
        hourHand.transform.rotation = Quaternion.Euler(0, 0, randomHour * -30f);
        minuteHand.transform.rotation = Quaternion.Euler(0, 0, randomMinute * -6f);
        ResultSet();
    }

    // Update is called once per frame
    void Update()
    {
        
        ResultSet();
    }
    void ResultSet()
    {
        OnInputValueChanged();

        
        
        if (randomHour == int.Parse(inputTextHour.text))
        {
            Debug.Log("정답!");
        }
        if (randomMinute == int.Parse(inputTextMinute.text))
        {
            Debug.Log("정답!");
        }
    }
    void OnInputValueChanged()
    {
        
        if (int.Parse(inputTextHour.text) >= hourTempCount)
        {
            inputTextHour.text = 1.ToString();
        }
        if (int.Parse(inputTextMinute.text) >= minuteTempCount)
        {
            inputTextMinute.text = 0.ToString();
        }

    }
}
