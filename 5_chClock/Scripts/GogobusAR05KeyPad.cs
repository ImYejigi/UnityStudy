using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GogobusAR05KeyPad : MonoBehaviour
{
    public MaterialsChange materialsChange;
    public ClockAR clock;
    public GameObject key;
    public GameClearController gameClearController;
    public string text;
    public bool state = false; //false �̸� HourText ����
    public List<GameObject> list = new();

    public void ChangeTextHour()
    {
        Debug.Log("�Լ� ���� " + state);
        if (state == true)
        {
            Debug.Log("state == true");
            clock.inputTextMinute.text += text;
        }
        else
        {
            Debug.Log("state == false");
            clock.inputTextHour.text += text;
        }
    }

    public void RemoveTextHour()
    {
        if (state == true)
        {
            clock.inputTextMinute.text = "";
        }
        else
        {
            clock.inputTextHour.text = "";
        }
    }

    public void Check()
    {
        // false �� ��
        if (key.GetComponent<GogobusAR05KeyPad>().state == false)
        {
            if (clock.ReturnHour() == clock.inputTextHour.text)
            {
                Debug.Log(" ���ƿ� ");

                foreach (var item in list)
                {
                    item.GetComponent<GogobusAR05KeyPad>().state = true;
                }
            }
            else
            {
                Debug.Log(" �޶�� ");
                clock.inputTextHour.text = "";
                StartCoroutine(TextDelay());
            }
        }
        // true �� ��
        else
        {
            if (clock.ReturnMinute() == clock.inputTextMinute.text)
            {
                Debug.Log(" ���ƿ� ");
                gameClearController.UpdateClearCount();
                //gameclear
                StartCoroutine(NextStage());
            }
            else
            {
                Debug.Log(" �޶�� ");
                clock.inputTextMinute.text = "";
                StartCoroutine(TextDelay());
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
}
