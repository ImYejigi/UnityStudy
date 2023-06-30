using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragFish : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static Vector2 startPos; // ���� ��ġ
    public CatFishManager catFishManager;


    //����� ����
    public enum FishState
    {
        None, RedDish, BlueDish
    }
    public FishState fishState = FishState.None;
    public List<Image> fishSprite = new List<Image>(); // ����� �̹���
    private Vector2 oringPos; // ����� �ʱ�ȭ ��ġ
    public List<Transform> fishParent = new List<Transform>(); // ������� �θ�׷�

    public float detectRange; // ���� ����


    public GameObject dish; // ����


    void Start()
    {
        oringPos = GetComponent<RectTransform>().position; // ����� ��ġ �ʱ�ȭ 

    }
    void Update()
    {
        int layerMask = 1 << 7; //7�� ���̾�
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        // ���ð� �����Ǿ��� �� 
        if (hits.Length > 0)
        {
            //������ ������Ʈ�� �̸�
            switch (hits[0].transform.name)
            {

                // �������� ������
                case "FishDetect_Red":
                    {
                        switch (fishState)
                        {
                            case FishState.None:
                                {
                                    dish = hits[0].transform.gameObject;
                                    dish.GetComponent<FishDetect>().AddFish(gameObject);
                                    fishState = FishState.RedDish; // ����� ���¸� ���� ���� ���·�
                                    break;
                                }

                            case FishState.BlueDish:
                                {
                                    // ���ÿ� ��ϵǾ� �ִ� ����� (�ڱ� �ڽ�) �� ����
                                    dish.GetComponent<FishDetect>().CancleFish(gameObject);
                                    hits[0].transform.GetComponent<FishDetect>().AddFish(gameObject);
                                    dish = hits[0].transform.gameObject;
                                    fishState = FishState.RedDish;
                                    break;
                                }
                        }
                        break;
                    }

                // �Ķ������� ������
                case "FishDetect_Blue":
                    {
                        switch (fishState)
                        {
                            case FishState.None:
                                {
                                    dish = hits[0].transform.gameObject;
                                    dish.GetComponent<FishDetect>().AddFish(gameObject);
                                    fishState = FishState.BlueDish; // ����� ���¸� ���� ���� ���·�
                                    break;
                                }

                            case FishState.RedDish:
                                {
                                    // ���ÿ� ��ϵǾ� �ִ� ����� (�ڱ� �ڽ�) �� ����
                                    dish.GetComponent<FishDetect>().CancleFish(gameObject);
                                    hits[0].transform.GetComponent<FishDetect>().AddFish(gameObject);
                                    dish = hits[0].transform.gameObject;
                                    fishState = FishState.BlueDish;
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
        // ���ð� �������� �ʾ��� �� 
        else
        {
            switch (fishState)
            {
                case FishState.RedDish:
                    {
                        // dish.GetComponent<FishDetect>().CancleFish(gameObject);
                        fishState = FishState.None; // ������� ���¸� None ���·�
                        break;
                    }
                case FishState.BlueDish:
                    {
                        // dish.GetComponent<FishDetect>().CancleFish(gameObject);
                        fishState = FishState.None;
                        break;
                    }
            }
        }
    }

    //������ Ŭ�� �ٿ�
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        catFishManager.FishDragStart(gameObject);
    }


    // ������ Ŭ�� ��
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        switch (fishState)
        {
            case FishState.None:
                {
                    if (dish)
                    {
                        // ���ÿ� ��ϵ� ����� (�ڽ���) ����
                        dish.GetComponent<FishDetect>().ReduceFish(gameObject);
                        // ������ ���� �����Ѵ�.
                        dish.GetComponent<FishDetect>().ReduceRope();
                        // 
                        dish.GetComponent<FishDetect>().RopeRebuild();
                        dish = null;
                    }
                    fishSprite[0].enabled = true;
                    fishSprite[1].enabled = false;
                    fishSprite[2].enabled = false;
                    break;
                }
            case FishState.RedDish:
                {
                    catFishManager.FishDragEnd();
                    fishSprite[0].enabled = false;
                    fishSprite[1].enabled = true;
                    fishSprite[2].enabled = true;
                    break;
                }
            case FishState.BlueDish:
                {
                    catFishManager.FishDragEnd();
                    fishSprite[0].enabled = false;
                    fishSprite[1].enabled = true;
                    fishSprite[2].enabled = true;
                    break;
                }
        }
    }


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position; // ���� ��ġ �ʱ�ȭ

    }

    // �巡�� ����
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position; // ���� ��ġ
        transform.position = currentPos; // ������� ��ġ�� ���� ��ġ�� ����
    }

    // �巡�� ����
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ����ī�޶��� ȭ�鿡�� ���콺 Ŀ���� ��ġ�� ��´�.


    }


    public void FishReset()
    {
        GetComponent<RectTransform>().position = oringPos;
        fishSprite[0].enabled = true;
        fishSprite[1].enabled = false;
        fishSprite[2].enabled = false;
        dish = null;
    }
}
