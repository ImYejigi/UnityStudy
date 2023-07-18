using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GogobusAR26Throw : MonoBehaviour 
{
    //�巡�� ����
    bool dragging = false;
    //ī�޶���� �Ÿ�
    float distance;
    //������ �ӵ�
    public float throwSpeed;
    //���� �ӵ�
    public float archSpeed;
    //�巡�� �ӵ�
    public float dragSpeed;
    //�ʱ� ������ ��
     Vector3 oldBallVector;
    //������ �� ����
     Vector3 defaultBallVector;
    //�ʱ� ȸ����
    Vector3 initRot;

    //
    //���콺 �巡�� ������ (�巡�� ����, ���콺 ���� ����, ���콺 ���� ����)
    public Vector2 direction, startPos, endPos;
    //Ŭ�� �ð� (Ŭ�� ���� �ð�, Ŭ�� ���� �ð�, ���ۺ��� ������� �ɸ� �ð�)
    public float touchTimeStart, touchTimeFinish, timeInterval;
    //������ X,Y �ӵ�
    public float throwForceXandY = 1.0f;
    //������ Z�ӵ�
    public float throwForceZ = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        //�ʱ� ȸ����
        initRot = transform.rotation.eulerAngles;
        oldBallVector = this.gameObject.transform.position;
        Debug.Log(oldBallVector);
    }

    // Update is called once per frame
    void Update()
    {
        //�巡���Ҷ� ��ü�� �Ѿƿ��� �����.
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = Vector3.Lerp
                (this.transform.position, rayPoint, dragSpeed * Time.deltaTime);
        }

        //�ٴ��� 0���Ϸ� ������ ��� �� �ڸ����� ���߰� �Ѵ�.
        if (transform.position.y < 0)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = Quaternion.Euler
                (90.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.position = new Vector3(transform.position.x, 0.0085f, transform.position.z);
        }
    }

    //���콺�� ������ ��(��ġ�� ���� ��) �۵��ϴ� �Լ�
    public void OnMouseDown()
    {
        //��ġ �ð� ����
        touchTimeStart = Time.time;
        //����ī�޶��� �Ÿ��� ����
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        //���콺 ���� ��ġ
        startPos = Input.mousePosition;
        //�巡�� ���� true
        dragging = true;
    }

    //���콺 ��ư�� ������ ��(��ġ�� ������ ��) �۵��ϴ� �Լ�
    public void OnMouseUp()
    {
        //��ġ ���� �ð�
        touchTimeFinish = Time.time;
        //��ġ ���۰� ���� ������ �ð��� ����
        timeInterval = touchTimeFinish - touchTimeStart;
        //���콺 ���� ��ġ
        endPos = Input.mousePosition;
        //��ü�� ���� ����
        direction = startPos - endPos;

        //isKinematic Off
        this.GetComponent<Rigidbody>().isKinematic = false;
        //���� ȸ��
        this.GetComponent<Rigidbody>().velocity += this.transform.forward * throwSpeed;
        //���� ȸ��
        this.GetComponent<Rigidbody>().velocity += this.transform.forward * archSpeed;
        //������ ����
        this.GetComponent<Rigidbody>().AddForce
            (-direction.x * throwForceXandY, -direction.y * throwForceXandY, throwForceZ / timeInterval);

        //�θ� Null
        this.transform.parent = null;

        //�巡�� ���� False
        dragging = false;
    }

    public void ShootingBall() 
    {
        
    }
}
