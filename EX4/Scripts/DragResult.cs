using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DragFish;


public class DragResult : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static Vector2 startPos; // ���� ��ġ
    public float detectRange; // ���� ����
    private Vector2 oringPos; // ���� �� �ʱ�ȭ ��ġ
    public enum BallState
    {
        None, Standby
    }
    public BallState ballState = BallState.None;
    public GameObject Result; // ���� ĭ


    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnDrawGizmos()
    {
        //���̾� ���Ǿ �׸���. (�׸� ��ġ, ������)
        Gizmos.DrawWireSphere(transform.position, detectRange);
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


    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }


    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        int layerMask = 1 << 8; //6�� ���̾�
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
