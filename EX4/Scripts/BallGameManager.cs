using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using static DragResult;
using UnityEngine.UI;
using Unity.VisualScripting;

public class BallGameManager : MonoBehaviour
{
    public List<GameObject> numBallSprite = new List<GameObject>(); // 문제에 해당하는 공들
    public List<GameObject> movingBallSprite = new List<GameObject>(); // 기계 안에서 굴러다니는 공들
    public List<GameObject> opBallSprite = new List<GameObject>(); // 연산자

    public int result; //결과 값
    public int randomBall; //랜덤 볼
    public int Operator; //연산자
    public Text ClearText; // 종료 텍스트
    private Vector3 startBallPos = new Vector3(-3, -533, 0);
    private Vector3 firstBallPos = new Vector3(150, 340, 0);
    private Vector3 operBallPos = new Vector3(470, 340, 0);
    private Vector3 SecondBallpos = new Vector3(790, 340, 0);
    int numOutput;
    public List<int> randNum = new List<int>();

    public int ballGameCount = 0;

    public List<RectTransform> questionBall = new List<RectTransform>();
    public Transform ballStartPos;

    //public enum ShadowBallState
    //{
    //    One,Two,None
    //} 
    // Start is called before the first frame update
    void Start()
    {

        RandomNumberOutput();
    }

    public static BallGameManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void Restart()
    {
        opBallSprite[0].SetActive(false);
        opBallSprite[1].SetActive(false);
        Debug.Log(Operator);
        opBallSprite[Operator].transform.localPosition = startPos;
        for (int i = 0; i<10 ; i++)
        {
            numBallSprite[i].SetActive(false);
            movingBallSprite[i].SetActive(true);
            
        }
        randNum.Clear();
        BallGameNext();
        
    }
    // 로또 숫자 추첨하기.
    public int RandomNumberOutput()
    {
        Operator = Random.Range(0, opBallSprite.Count);


        
        while (randNum.Count < 2)
        {
            
            numOutput = Random.Range(0, numBallSprite.Count);

            if (randNum.Contains(numOutput))
            {
                continue;
            }

            randNum.Add(numOutput);
            StartCoroutine(MathStart());

            result = numOutput + randNum[0];

            switch (Operator)
            {
                case 0:
                    {
                        if (randNum.Count == 2)
                        {
                            result = numOutput + randNum[0];
                           
                                if (result >= 10)
                                {
                                    randNum.RemoveAt(1); // 두 번째 숫자 제거
                                    numBallSprite[numOutput].gameObject.SetActive(false);
                                    movingBallSprite[numOutput].gameObject.SetActive(true);


                                }      
                        }
                        break;
                    }
                case 1:
                    {
                        if (randNum.Count == 2)
                        {
                                result = numOutput - randNum[0];
                        }
                        break;
                    }
            }

        }
        
        return Mathf.Abs(result);
    }


    IEnumerator MathStart()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MoveTo(opBallSprite[Operator].gameObject, operBallPos));
        if (numOutput > randNum[0])
        {
            StartCoroutine(MoveTo(numBallSprite[numOutput].gameObject, firstBallPos));
            StartCoroutine(MoveTo(numBallSprite[randNum[0]].gameObject, SecondBallpos));
        }
        else
        {
            StartCoroutine(MoveTo(numBallSprite[numOutput].gameObject, SecondBallpos));
            StartCoroutine(MoveTo(numBallSprite[randNum[0]].gameObject, firstBallPos));
        }

        opBallSprite[Operator].gameObject.SetActive(true);
        numBallSprite[numOutput].gameObject.SetActive(true);
        numBallSprite[randNum[0]].gameObject.SetActive(true);
        movingBallSprite[numOutput].gameObject.SetActive(false);
        movingBallSprite[randNum[0]].gameObject.SetActive(false);
    }

    IEnumerator MoveTo(GameObject a, Vector3 toPos)
    {
        float count = 0;
       
        while (true)
        {
            Vector3 wasPos = a.transform.position;
            count += Time.deltaTime;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);
            a.transform.localScale = Vector3.Lerp(a.transform.localScale, new Vector3(2, 2, 2), count);

            if(count >= 1)
            {
                a.transform.position = toPos;
                break;

            }

            yield return null;
        }
    }

// Update is called once per frame


void Update()
    {
        
    }

    public void BallGameNext()
    {
        ballGameCount++;
        RandomNumberOutput();
        if (ballGameCount <= 5) 
        {
            CapsuleBallReset();
            Debug.Log("게임 남은 횟수 : " + ballGameCount);
            
            
        }
        else
        {
            CapsuleBallReset();
            ClearText.gameObject.SetActive(true);
        }
    }

    public void CapsuleBallReset()
    {
        for(int i = 0; i < questionBall.Count; i++)
        {
            questionBall[i].position = ballStartPos.GetComponent<RectTransform>().position;
            questionBall[i].localScale = Vector3.one;
            
        }
    }

}
