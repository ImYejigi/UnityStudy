using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBalls : MonoBehaviour
{
    public List<GameObject> numBallSprite = new List<GameObject>(); // 문제에 해당하는 공들
    public List<GameObject> movingBallSprite = new List<GameObject>(); // 기계 안에서 굴러다니는 공들
    public List<GameObject> opBallSprite = new List<GameObject>(); // 연산자

    public int result; //결과 값
    public int randomBall; //랜덤 볼
    public int Operator; //연산자
    // Start is called before the first frame update
    void Start()
    {
        selectBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void selectBall()
    {
        Operator = Random.Range(0, opBallSprite.Count);
        opBallSprite[Operator].gameObject.SetActive(true);
        Debug.Log(Operator);
        if (Operator == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                randomBall = Random.Range(0, numBallSprite.Count);
                result += randomBall;
                numBallSprite[randomBall].gameObject.SetActive(true);
                numBallSprite.RemoveAt(randomBall);
                movingBallSprite.RemoveAt(randomBall);
            }
            Debug.Log(randomBall);
        }
        else
        {
            randomBall = Random.Range(0, numBallSprite.Count);
            result += randomBall;
            numBallSprite[randomBall].gameObject.SetActive(true);
            numBallSprite.RemoveAt(randomBall);
            movingBallSprite.RemoveAt(randomBall);
            Debug.Log(randomBall);
            for (int i = 0; i < 1; i++)
            {
                randomBall = Random.Range(0, numBallSprite.Count);
                result -= randomBall;
                numBallSprite[randomBall].gameObject.SetActive(true);
                numBallSprite.RemoveAt(randomBall);
                movingBallSprite.RemoveAt(randomBall);
            }
            
        }
        Debug.Log(result);




    }
}
