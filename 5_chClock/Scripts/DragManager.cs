using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static DragResult;

public class DragManager : MonoBehaviour ,IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler

{
    public static Vector2 startPos; // 시작 위치
    public float detectRange; // 감지 범위
    public Vector2 originPos; // 숫자 공 초기화 위치
    private Vector2 oPos;

    // Start is called before the first frame update
    void Start()
    {
        oPos = GetComponent<RectTransform>().position;
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
        Debug.Log("도착");
    }
    // 	포인터를 놓을 때 전송됩니다.
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        
    }
}
