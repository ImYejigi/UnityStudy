using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ClockAR : MonoBehaviour
{
    public ARRaycastManager raycastManager;//AR����ĳ��Ʈ �Ŵ���
    public GameObject arPrefab;//AR������
    private GameObject arObject;//�����Ǵ� AR������Ʈ 

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
    // Start is called before the first frame update
    void Start()
    {
        RandomClock();
    }

    void RandomClock()
    {
        randomHour = Random.Range(1, 13);
        randomMinute = Random.Range(0, 60);
        hourHand.transform.rotation = Quaternion.Euler(0, 0, (randomHour * -30f) + (randomMinute * -0.5f ));
        minuteHand.transform.rotation = Quaternion.Euler(0, 0, randomMinute * -6f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void ResultSet()
    {

        OnInputValueChanged();

        if (randomHour == int.Parse(inputTextHour.text) && randomMinute == int.Parse(inputTextMinute.text))
        {
            Debug.Log("����!");
           StartCoroutine(NextStage());

        }
        else if(randomHour == int.Parse(inputTextHour.text))
        {
            Debug.Log("�ð� ����!");
            correctText.text = "�ð� ����!";
            StartCoroutine(TextDelay());
        }
        else if (randomMinute == int.Parse(inputTextMinute.text))
        {
            Debug.Log("�� ����!");
            correctText.text = "�� ����!";
            StartCoroutine(TextDelay());
        }
        else
        {
            Debug.Log("�ٽ��غ���!");
            correctText.text = "�ٽ��غ���!";
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
        correctText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        correctText.gameObject.SetActive(false);
    }
    IEnumerator NextStage()
    {
        correctText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        gameClearController.UpdateClearCount();
    }
}
