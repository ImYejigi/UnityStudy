using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragFish : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static Vector2 startPos; // 시작 위치
    public CatFishManager catFishManager;


    //물고기 상태
    public enum FishState
    {
        None, RedDish, BlueDish
    }
    public FishState fishState = FishState.None;
    public List<Image> fishSprite = new List<Image>(); // 물고기 이미지
    private Vector2 oringPos; // 물고기 초기화 위치
    public List<Transform> fishParent = new List<Transform>(); // 물고기의 부모그룹

    public float detectRange; // 감지 범위


    public GameObject dish; // 접시


    void Start()
    {
        oringPos = GetComponent<RectTransform>().position; // 물고기 위치 초기화 

    }
    void Update()
    {
        int layerMask = 1 << 7; //7번 레이어
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        // 접시가 감지되었을 때 
        if (hits.Length > 0)
        {
            //감지된 오브젝트의 이름
            switch (hits[0].transform.name)
            {

                // 빨강접시 감지기
                case "FishDetect_Red":
                    {
                        switch (fishState)
                        {
                            case FishState.None:
                                {
                                    dish = hits[0].transform.gameObject;
                                    dish.GetComponent<FishDetect>().AddFish(gameObject);
                                    fishState = FishState.RedDish; // 물고기 상태를 빨강 접시 상태로
                                    break;
                                }

                            case FishState.BlueDish:
                                {
                                    // 접시에 등록되어 있던 물고기 (자기 자신) 을 제거
                                    dish.GetComponent<FishDetect>().CancleFish(gameObject);
                                    hits[0].transform.GetComponent<FishDetect>().AddFish(gameObject);
                                    dish = hits[0].transform.gameObject;
                                    fishState = FishState.RedDish;
                                    break;
                                }
                        }
                        break;
                    }

                // 파랑접시의 감지기
                case "FishDetect_Blue":
                    {
                        switch (fishState)
                        {
                            case FishState.None:
                                {
                                    dish = hits[0].transform.gameObject;
                                    dish.GetComponent<FishDetect>().AddFish(gameObject);
                                    fishState = FishState.BlueDish; // 물고기 상태를 빨강 접시 상태로
                                    break;
                                }

                            case FishState.RedDish:
                                {
                                    // 접시에 등록되어 있던 물고기 (자기 자신) 을 제거
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
        // 접시가 감지되지 않았을 때 
        else
        {
            switch (fishState)
            {
                case FishState.RedDish:
                    {
                        // dish.GetComponent<FishDetect>().CancleFish(gameObject);
                        fishState = FishState.None; // 물고기의 상태를 None 상태로
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

    //포인터 클릭 다운
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        catFishManager.FishDragStart(gameObject);
    }


    // 포인터 클릭 업
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        switch (fishState)
        {
            case FishState.None:
                {
                    if (dish)
                    {
                        // 접시에 등록된 물고기 (자신을) 뺀다
                        dish.GetComponent<FishDetect>().ReduceFish(gameObject);
                        // 로프를 전부 제거한다.
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
        startPos = eventData.position; // 시작 위치 초기화

    }

    // 드래그 도중
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position; // 현재 위치
        transform.position = currentPos; // 물고기의 위치를 현재 위치로 지정
    }

    // 드래그 종료
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 메인카메라의 화면에서 마우스 커서의 위치를 담는다.


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
