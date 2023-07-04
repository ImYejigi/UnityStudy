using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DragFish;
using UnityEngine.SceneManagement;


public class DragResult : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Vector2 startPos; // ���� ��ġ
    public float detectRange; // ���� ����
    private Vector2 oringPos; // ���� �� �ʱ�ȭ ��ġ

    public BallGameManager ballGameManager;
    public ShadowDetect shadowDetect;

    public int Health = 5;
    
    public BallState ballState = BallState.None;
    public GameObject Result; // ���� ĭ
    public Vector2 originPos;
    private Vector2 oPos;


    public enum BallState
    {
       None, Standby
    }

    void Start()
    {
        this.gameObject.layer = 3;
        oPos = GetComponent<RectTransform>().position;
    }

    void Update()
    {
    }
    public void OnDrawGizmos()
    {
        //���̾� ���Ǿ �׸���. (�׸� ��ġ, ������)
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    // �巡�� ����
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
        startPos = eventData.position; // ���� ��ġ �ʱ�ȭ

    }

    // �巡�� ����
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position; // ���� ��ġ
        transform.position = currentPos; // ���� ���� ��ġ�� ���� ��ġ�� ����
    }

    // �巡�� ����
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ����ī�޶��� ȭ�鿡�� ���콺 Ŀ���� ��ġ�� ��´�.


    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    IEnumerator OK()
    {
        yield return new WaitForSeconds(1.5f);
        ballState = BallState.None;
        GetComponent<RectTransform>().position = oPos;
        transform.localScale = new Vector3(1f, 1f, 1f);
        BallGameManager.instance.Restart();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {

        int layerMask = 1 << 3; //6�� ���̾�
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);
        int a = ballGameManager.RandomNumberOutput();
        Debug.Log(shadowDetect.ballList[a - 1]);
        switch (ballState)
        {
            case BallState.None:
                    GetComponent<RectTransform>().position = oPos;
                    StartCoroutine(OK());
                break;
           

        }


        if (hits.Length > 0)
        {
          
            bool isSelectBallMatched = false; // ball.name�� ��ġ�ϴ��� ���� �����ϴ� ����

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.name == shadowDetect.ballList[a - 1].name)
                {
                    isSelectBallMatched = true;
                    Debug.Log("����.");
                }
                
            }

            if (isSelectBallMatched)
            {
                shadowDetect.ballList[a - 1].transform.position = new Vector3(shadowDetect.transform.position.x, shadowDetect.transform.position.y, shadowDetect.transform.position.z);
                shadowDetect.ballList[a - 1].transform.localScale = new Vector3(shadowDetect.transform.localScale.x, shadowDetect.transform.localScale.y, shadowDetect.transform.localScale.z);
              ballState =  BallState.Standby;
            }
            if (!isSelectBallMatched)
            {
                ballState = BallState.None;
            }

            
             
        }
    }
}
