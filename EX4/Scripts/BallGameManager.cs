using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallGameManager : MonoBehaviour
{
    public List<GameObject> numBallSprite = new List<GameObject>(); // 문제에 해당하는 공들
    public List<GameObject> movingBallSprite = new List<GameObject>(); // 기계 안에서 굴러다니는 공들
    public List<GameObject> opBallSprite = new List<GameObject>(); // 연산자

    public int result; //결과 값
    public int randomBall; //랜덤 볼
    public int Operator; //연산자

    int numOutput;
    public List<int> randNum = new List<int>();

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
        for (int i = 0; i<10 ; i++)
        {
            numBallSprite[i].SetActive(false);
            movingBallSprite[i].SetActive(true);
            
        }
        randNum.Clear();
        RandomNumberOutput();
        
    }
    // 로또 숫자 추첨하기.
    public int RandomNumberOutput()
    {
        Operator = Random.Range(0, opBallSprite.Count);
        opBallSprite[Operator].gameObject.SetActive(true);
        Debug.Log("연산자입니다." + Operator);


        while (randNum.Count < 2)
        {
            
            numOutput = Random.Range(0, numBallSprite.Count);

            if (randNum.Contains(numOutput))
            {
                continue;
            }
            randNum.Add(numOutput);
            numBallSprite[numOutput].gameObject.SetActive(true);
            numBallSprite[numOutput].transform.position = new Vector3(-317, -463, 0);
            movingBallSprite[numOutput].gameObject.SetActive(false);
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
                                numBallSprite[numOutput].transform.position = new Vector3(-309, -463, 0);
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

    // Update is called once per frame
    void Update()
    {

    }

}
