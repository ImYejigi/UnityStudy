using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static DragResult;

public class DragManager : MonoBehaviour ,IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler

{
    public static Vector2 startPos; // ���� ��ġ
    public float detectRange; // ���� ����
    public Vector2 originPos; // ���� �� �ʱ�ȭ ��ġ
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
        Debug.Log("����");
    }
    // 	�����͸� ���� �� ���۵˴ϴ�.
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        
    }
}
