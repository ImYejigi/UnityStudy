using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatFishManager : MonoBehaviour
{
    public GameClearController gameClearController;
    public enum CatFishState
    {
        Stanby, Play, Result
    }
    public CatFishState catFishState = CatFishState.Stanby;

    // ����� ����Ʈ
    public List<GameObject> fishList = new List<GameObject>();

    public int totalFishCount; // ��ü ������� ����
    [SerializeField]
    private int redFishCount; // �������ÿ� �ʿ��� ����� ����
    [SerializeField]
    private int blueFishCount; // �Ķ����ÿ� �ʿ��� ����� ����

    public FishDetect redDish; // ���� ����
    public FishDetect blueDish; // �Ķ� ����

    public Text redFishText; // �������� ����� �ؽ�Ʈ
    public Text blueFishText; // �Ķ����� ����� �ؽ�Ʈ
    public Text resultText; // ��� �ؽ�Ʈ


    public Text startText; // ���� �ؽ�Ʈ
     // �غ� �ؽ�Ʈ

    // Start is called before the first frame update
    void Start()
    {
        RandomFishCount();
        StartCoroutine(GameState());
        
    }
   IEnumerator GameState()
    {
        yield return new WaitForSeconds(3.0f);
        catFishState = CatFishState.Play;
        StartMessage();
        yield return new WaitForSeconds(1.5f);
        startText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
        switch (catFishState)
        {
            case CatFishState.Play:
                {
                    if (redDish.fishCount + blueDish.fishCount == totalFishCount)
                    {


                        catFishState = CatFishState.Result;
                    }
                    break;
                }
               
        }
    }

    void StartMessage()
    {
        if(catFishState == CatFishState.Play)
        {
            startText.text = "����!";
        }
    }
    // ����� ���
    void FishResult()
    {
        resultText.gameObject.SetActive(true);
        // ���� ������ ���� ���� ���� ����, �Ķ����� ����� ���� �Ķ����� ���� ���� �� 
        if (redDish.fishCount == redFishCount && blueDish.fishCount == blueFishCount)
        {
            resultText.text = "����!";
            SuccessGame();
        }
        else
        {
            resultText.text = "����..";
        }
        StartCoroutine(FishReset());
    }
    // �ʱ�ȭ
    IEnumerator FishReset()
    {
        yield return new WaitForSeconds(3.0f);
        RandomFishCount();
        for(int i = 0; i < fishList.Count; i++)
        {
            fishList[i].SendMessage("FishReset");
        }
        resultText.text = "";
        resultText.gameObject.SetActive(false);
        catFishState = CatFishState.Play;
       

    }
    // �������ÿ� �Ķ����ÿ� ������ ������ �����ϴ� �Լ�
    void RandomFishCount()
    {
        // �������ÿ� �ʿ��� ������� ������ �������� ����
        redFishCount = Random.Range(1, totalFishCount);
        // �������ÿ� �ʿ��� ������� ������ �ؽ�Ʈ�� ǥ��
        redFishText.text = redFishCount.ToString();

        // �Ķ����ÿ� �ʿ��� ������� ������ �������� ����(��ü ���� - ���� ����)
        blueFishCount = totalFishCount - redFishCount;
        // �Ķ����ÿ� �ʿ��� ������� ������ �ؽ�Ʈ�� ǥ��
        blueFishText.text = blueFishCount.ToString();

    }
    void SuccessGame()
    {
        gameClearController.UpdateClearCount();
    }

    //����⸦ �巢 ������ �� ������ ����⸸ �����̰� �ϴ� �Լ�
    public void FishDragStart(GameObject fish)
    {
        //// ��ü ����� ����Ʈ �߿���
        //for (int i = 0; i < fishList.Count; i++)
        //{
        //    // ������ ����Ⱑ �ƴ϶��
        //    if (fishList[i] != fish)
        //    {
        //        // �ش� ������� Image������Ʈ�� ��� �����Ѵ�.
        //        fishList[i].GetComponent<Image>().enabled = false;
        //    }
        //}
    }

    //����� �巡�װ� ������ ���� �Լ�
    public void FishDragEnd()
    {

        //��ü �����
        for (int i = 0; i < fishList.Count; i++)
        {
            // ��� ������� Image ������Ʈ�� ����Ѵ�
            fishList[i].GetComponent<Image>().enabled = true;
        }


        redDish.FishInDish();
        blueDish.FishInDish();
        if (redDish.fishCount + blueDish.fishCount == totalFishCount)
        {
            FishResult();
        }
        
    }
}
