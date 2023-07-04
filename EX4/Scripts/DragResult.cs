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
    public static Vector2 startPos; // 시작 위치
    public float detectRange; // 감지 범위
    private Vector2 oringPos; // 숫자 공 초기화 위치

    public BallGameManager ballGameManager;
    public ShadowDetect shadowDetect;

    public int Health = 5;
    
    public BallState ballState = BallState.None;
    public GameObject Result; // 정답 칸
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
        //와이어 스피어를 그린다. (그릴 위치, 반지름)
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    // 드래그 시작
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
        startPos = eventData.position; // 시작 위치 초기화

    }

    // 드래그 도중
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position; // 현재 위치
        transform.position = currentPos; // 숫자 공의 위치를 현재 위치로 지정
    }

    // 드래그 종료
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 메인카메라의 화면에서 마우스 커서의 위치를 담는다.


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

        int layerMask = 1 << 3; //6번 레이어
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
          
            bool isSelectBallMatched = false; // ball.name과 일치하는지 여부 저장하는 변수

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.name == shadowDetect.ballList[a - 1].name)
                {
                    isSelectBallMatched = true;
                    Debug.Log("정답.");
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
