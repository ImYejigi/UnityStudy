using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GogobusAR26BallDetect : MonoBehaviour
{
    public float detectRange; // ���� ����
    public List<GameObject> ballList = new List<GameObject>();

    public GameObject shadowDetect;

    public BallGameManager ballGameManager;

    public int correctAnswer;

    public void Start()
    {
        this.gameObject.layer = 8;
    }


    public void Update()
    {


    }


    public void OnDrawGizmos()
    {

        // ���̾� ���Ǿ �׸���.( �׸� ��ġ, ������)
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

}
