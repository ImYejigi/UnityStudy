using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallGameManager : MonoBehaviour
{
    public List<GameObject> numBallSprite = new List<GameObject>(); // ������ �ش��ϴ� ����
    public List<GameObject> movingBallSprite = new List<GameObject>(); // ��� �ȿ��� �����ٴϴ� ����
    public List<GameObject> opBallSprite = new List<GameObject>(); // ������

    public int result; //��� ��
    public int randomBall; //���� ��
    public int Operator; //������

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
    // �ζ� ���� ��÷�ϱ�.
    public int RandomNumberOutput()
    {
        Operator = Random.Range(0, opBallSprite.Count);
        opBallSprite[Operator].gameObject.SetActive(true);
        Debug.Log("�������Դϴ�." + Operator);


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
                                randNum.RemoveAt(1); // �� ��° ���� ����
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
