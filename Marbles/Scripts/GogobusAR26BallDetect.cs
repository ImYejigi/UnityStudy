using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GogobusAR26BallDetect : MonoBehaviour
{
    public float detectRange; // 감지 범위
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

        // 와이어 스피어를 그린다.( 그릴 위치, 반지름)
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

}
