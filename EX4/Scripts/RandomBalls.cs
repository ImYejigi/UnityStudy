using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBalls : MonoBehaviour
{
    public List<GameObject> numBallSprite = new List<GameObject>(); // ������ �ش��ϴ� ����
    public List<GameObject> movingBallSprite = new List<GameObject>(); // ��� �ȿ��� �����ٴϴ� ����
    public List<GameObject> opBallSprite = new List<GameObject>(); // ������

    public int result; //��� ��
    public int randomBall; //���� ��
    public int Operator; //������
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
