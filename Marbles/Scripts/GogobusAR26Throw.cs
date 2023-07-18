using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GogobusAR26Throw : MonoBehaviour 
{
    //드래그 사인
    bool dragging = false;
    //카메라와의 거리
    float distance;
    //던지는 속도
    public float throwSpeed;
    //높이 속도
    public float archSpeed;
    //드래그 속도
    public float dragSpeed;
    //초기 포지션 값
     Vector3 oldBallVector;
    //포지션 값 측정
     Vector3 defaultBallVector;
    //초기 회전값
    Vector3 initRot;

    //
    //마우스 드래그 포지션 (드래그 방향, 마우스 시작 지점, 마우스 종료 지점)
    public Vector2 direction, startPos, endPos;
    //클릭 시간 (클릭 시작 시간, 클릭 종료 시간, 시작부터 종료까지 걸린 시간)
    public float touchTimeStart, touchTimeFinish, timeInterval;
    //던지기 X,Y 속도
    public float throwForceXandY = 1.0f;
    //던지기 Z속도
    public float throwForceZ = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        //초기 회전값
        initRot = transform.rotation.eulerAngles;
        oldBallVector = this.gameObject.transform.position;
        Debug.Log(oldBallVector);
    }

    // Update is called once per frame
    void Update()
    {
        //드래그할때 물체가 쫓아오게 만든다.
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = Vector3.Lerp
                (this.transform.position, rayPoint, dragSpeed * Time.deltaTime);
        }

        //바닥을 0이하로 내려갈 경우 그 자리에서 멈추게 한다.
        if (transform.position.y < 0)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = Quaternion.Euler
                (90.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.position = new Vector3(transform.position.x, 0.0085f, transform.position.z);
        }
    }

    //마우스를 눌렀을 때(터치를 했을 때) 작동하는 함수
    public void OnMouseDown()
    {
        //터치 시간 시작
        touchTimeStart = Time.time;
        //메인카메라의 거리를 측정
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        //마우스 시작 위치
        startPos = Input.mousePosition;
        //드래그 사인 true
        dragging = true;
    }

    //마우스 버튼을 떼었을 때(터치를 떼었을 때) 작동하는 함수
    public void OnMouseUp()
    {
        //터치 종료 시간
        touchTimeFinish = Time.time;
        //터치 시작과 종료 사이의 시간을 측정
        timeInterval = touchTimeFinish - touchTimeStart;
        //마우스 종료 위치
        endPos = Input.mousePosition;
        //물체를 던질 방향
        direction = startPos - endPos;

        //isKinematic Off
        this.GetComponent<Rigidbody>().isKinematic = false;
        //전방 회전
        this.GetComponent<Rigidbody>().velocity += this.transform.forward * throwSpeed;
        //높이 회전
        this.GetComponent<Rigidbody>().velocity += this.transform.forward * archSpeed;
        //던지는 세기
        this.GetComponent<Rigidbody>().AddForce
            (-direction.x * throwForceXandY, -direction.y * throwForceXandY, throwForceZ / timeInterval);

        //부모 Null
        this.transform.parent = null;

        //드래그 사인 False
        dragging = false;
    }

    public void ShootingBall() 
    {
        
    }
}
