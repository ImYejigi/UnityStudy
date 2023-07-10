using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClockARManager : MonoBehaviour, IPointerUpHandler
{
    public GameObject hourHand;
    public GameObject minuteHand;
    public int minValue = 0;
    public int maxValue = 59;
    public InputField inputTextHour;
    public InputField inputTextMinute;








    [Range(0, 12)]
    public int hourTempCount;
    [Range(0, 60)]
    public int minuteTempCount;

    // Start is called before the first frame update
    void Start()
    {
        inputTextHour.text = Random.Range(1, 13).ToString();
        inputTextMinute.text = Random.Range(0, 59).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {

    }
}
