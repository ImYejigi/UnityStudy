using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ClockAR : MonoBehaviour
{
    public GameObject arPrefab;//AR프리팹
    private GameObject arObject;//생성되는 AR오브젝트 
    public MaterialsChange materialsChange;

    public GameClearController gameClearController;

    public GameObject hourHand;
    public GameObject minuteHand;
    public int randomHour;
    public int randomMinute;
    public InputField inputTextHour;
    public InputField inputTextMinute;
    public Text correctText;

    public int minValue = 0;
    public int maxValue = 59;
    [Range(0, 12)]
    public int hourTempCount;
    [Range(0,60)]
    public int minuteTempCount;

    public List<int> minuteRandom = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        RandomClock();
        materialsChange.SmileExpression();
    }

    void RandomClock()
    {
        for (int i = 1; i < 12; i++)
        {
            int j = i * 5;
            minuteRandom.Add(j);
        }

        randomHour = Random.Range(1, 13);
        randomMinute = minuteRandom[Random.Range(1, 12)];
        hourHand.transform.rotation = Quaternion.Euler(0, 0, randomHour * -30f);
        minuteHand.transform.rotation = Quaternion.Euler(0, 0, randomMinute * -6f);
    }

    //
    public string ReturnHour()
    {
        return randomHour.ToString();
    }

    public string ReturnMinute()
    {
        return randomMinute.ToString();
    }

    



    //
    public void ResultSet()
    {

        OnInputValueChanged();

        if (randomHour == int.Parse(inputTextHour.text) && randomMinute == int.Parse(inputTextMinute.text))
        {
            Debug.Log("정답!");
           StartCoroutine(NextStage());

        }
        else if(randomHour == int.Parse(inputTextHour.text))
        {
            Debug.Log("시간 정답!");
            StartCoroutine(TextDelay());
        }
        else if (randomMinute == int.Parse(inputTextMinute.text))
        {
            Debug.Log("분 정답!");
            StartCoroutine(TextDelay());
        }
        else
        {
            Debug.Log("다시해봐요!");
            StartCoroutine(TextDelay());
        }
    }
    void OnInputValueChanged()
    {
            if (int.Parse(inputTextHour.text) > hourTempCount)
            {
                inputTextHour.text = 0.ToString();
            }

            if (int.Parse(inputTextMinute.text) >= minuteTempCount)
            {
                inputTextMinute.text = 0.ToString();
            }
    }

    IEnumerator TextDelay()
    {
        
        materialsChange.SadExpression();
        yield return new WaitForSeconds(1.5f);
        materialsChange.SmileExpression();
    }
    IEnumerator NextStage()
    {
        materialsChange.WinkExpression();
        yield return new WaitForSeconds(1.5f);
        gameClearController.UpdateClearCount();
    }
}
