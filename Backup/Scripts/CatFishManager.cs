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

    // 물고기 리스트
    public List<GameObject> fishList = new List<GameObject>();

    public int totalFishCount; // 전체 물고기의 개수
    [SerializeField]
    private int redFishCount; // 빨강접시에 필요한 물고기 개수
    [SerializeField]
    private int blueFishCount; // 파랑접시에 필요한 물고기 개수

    public FishDetect redDish; // 빨강 접시
    public FishDetect blueDish; // 파랑 접시

    public Text redFishText; // 빨강접시 물고기 텍스트
    public Text blueFishText; // 파랑접시 물고기 텍스트
    public Text resultText; // 결과 텍스트


    public Text startText; // 시작 텍스트
     // 준비 텍스트

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
            startText.text = "시작!";
        }
    }
    // 물고기 결과
    void FishResult()
    {
        resultText.gameObject.SetActive(true);
        // 빨강 물고기와 빨강 접시 수가 같고, 파랑접시 물고기 수와 파랑접시 수가 같을 때 
        if (redDish.fishCount == redFishCount && blueDish.fishCount == blueFishCount)
        {
            resultText.text = "성공!";
            SuccessGame();
        }
        else
        {
            resultText.text = "실패..";
        }
        StartCoroutine(FishReset());
    }
    // 초기화
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
    // 빨강접시와 파랑접시에 무작위 개수를 지정하는 함수
    void RandomFishCount()
    {
        // 빨강접시에 필요한 물고기의 개수를 무작위로 지정
        redFishCount = Random.Range(1, totalFishCount);
        // 빨강접시에 필요한 물고기의 개수를 텍스트로 표시
        redFishText.text = redFishCount.ToString();

        // 파랑접시에 필요한 물고기의 개수를 무작위로 지정(전체 개수 - 빨강 개수)
        blueFishCount = totalFishCount - redFishCount;
        // 파랑접시에 필요한 물고기의 개수를 텍스트로 표시
        blueFishText.text = blueFishCount.ToString();

    }
    void SuccessGame()
    {
        gameClearController.UpdateClearCount();
    }

    //물고기를 드랙 시작할 때 선택한 물고기만 움직이게 하는 함수
    public void FishDragStart(GameObject fish)
    {
        //// 전체 물고기 리스트 중에서
        //for (int i = 0; i < fishList.Count; i++)
        //{
        //    // 선택한 물고기가 아니라면
        //    if (fishList[i] != fish)
        //    {
        //        // 해당 물고기의 Image컴포넌트를 사용 중지한다.
        //        fishList[i].GetComponent<Image>().enabled = false;
        //    }
        //}
    }

    //물고기 드래그가 끝났을 때의 함수
    public void FishDragEnd()
    {

        //전체 물고기
        for (int i = 0; i < fishList.Count; i++)
        {
            // 모든 물고기의 Image 컴포넌트를 사용한다
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
