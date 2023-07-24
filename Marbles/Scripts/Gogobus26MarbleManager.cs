using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Gogobus26MarbleManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;//AR����ĳ��Ʈ �Ŵ���
    public GameObject arPrefab;//AR������
    private GameObject arObject;//�����Ǵ� AR������Ʈ 


    // Update is called once per frame
    void Update()
    {
        CreateARObject();
    }
    //��ġ�� �������� AR������Ʈ�� �����ϴ� �Լ�
    void CreateARObject()
    {
        //�޴��� ��ġ ������ 0������ Ŭ �� 
        if (Input.touchCount > 0)
        {
            //���� ���� ��ġ�Ǵ� ��ġ ����(0��)�� touch�� ��ȯ�Ѵ�
            Touch touch = Input.GetTouch(0);
            //�浹���� ����Ʈ 
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            //��ġ�� �Ͼ ������ ���̸� �߻��Ѵ�.(���� �߻� ��ġ, �浹 ����, Ʈ��ŷ Ÿ��)
            if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
            {
                //���� ���� ��ġ�� ������ PoseŸ�Կ� ������
                Pose hitPose = hits[0].pose;
                //������AR������Ʈ�� ���� ��
                if (!arObject)
                {
                    //AR ����Ʈ ����
                    var points = raycastManager.GetComponent<ARPointCloudManager>().trackables;
                    foreach (var pts in points)
                    {
                        pts.gameObject.SetActive(false);
                    }
                    raycastManager.GetComponent<ARPointCloudManager>().enabled = false;
                    //AR �÷��� ����
                    var planes = raycastManager.GetComponent<ARPlaneManager>().trackables;
                    foreach (var pls in planes)
                    {
                        pls.gameObject.SetActive(false);
                    }
                    raycastManager.GetComponent<ARPlaneManager>().enabled = false;
                    //AR�������� �����Ͽ� ù��° ��ġ ������ ��ġ�� ȸ�������� �ʱ�ȭ�Ѵ�
                    //arObject = Instantiate(arPrefab, hitPose.position, hitPose.rotation);
                    arPrefab.SetActive(true);
                    arPrefab.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    arObject = arPrefab;
                }
                //������ AR������Ʈ�� ���� ���
                else
                {
                    //��ġ�� �������� ������ AR������Ʈ�� ���ġ�Ѵ�
                    //arObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }
        }


    }
}
