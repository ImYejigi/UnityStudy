using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DragFish;


public class DragResult : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Vector2 startPos; // 시작 위치
    public float detectRange; // 감지 범위
    private Vector2 oringPos; // 숫자 공 초기화 위치
    public enum BallState
    {
        None, Standby
    }
    public BallState ballState = BallState.None;
    public GameObject Result; // 정답 칸


    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnDrawGizmos()
    {
        //와이어 스피어를 그린다. (그릴 위치, 반지름)
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    // Update is called once per frame
    void Update()
    {

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


    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        int layerMask = 1 << 8; //6번 레이어
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        if (hits.Length > 0)
        {

            switch (hits[0].transform.name)
            {
                case "ResultShadow":
                    switch (ballState)
                    {
                        case BallState.None:
                            transform.position = new Vector3(Result.transform.position.x, Result.transform.position.y, transform.position.z);
                            transform.localScale = new Vector3(Result.transform.localScale.x, Result.transform.localScale.y, Result.transform.localScale.z);
                            break;

                        case BallState.Standby:
                            break;
                    }
                    break;


            }
        }





    }
}
