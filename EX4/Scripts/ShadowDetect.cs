using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShadowDetect : MonoBehaviour
{

    public float detectRange; // ���� ����
    public List<GameObject> ballList = new List<GameObject>();

    public GameObject shadowDetect;

    public BallGameManager ballGameManager;

    public void Start()
    {
        this.gameObject.layer = 0;
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
