using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Gogobus26MarbleManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;//AR레이캐스트 매니저
    public GameObject arPrefab;//AR프리팹
    private GameObject arObject;//생성되는 AR오브젝트 


    // Update is called once per frame
    void Update()
    {
        CreateARObject();
    }
    //터치한 지점에서 AR오브젝트를 생성하는 함수
    void CreateARObject()
    {
        //휴대폰 터치 개수가 0개보다 클 때 
        if (Input.touchCount > 0)
        {
            //가장 먼저 터치되는 터치 정보(0번)을 touch에 반환한다
            Touch touch = Input.GetTouch(0);
            //충돌정보 리스트 
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            //터치가 일어난 지점에 레이를 발사한다.(레이 발사 위치, 충돌 정보, 트래킹 타입)
            if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
            {
                //가장 먼저 터치한 지점을 Pose타입에 저장함
                Pose hitPose = hits[0].pose;
                //생성된AR오브젝트가 없을 때
                if (!arObject)
                {
                    //AR 포인트 숨김
                    var points = raycastManager.GetComponent<ARPointCloudManager>().trackables;
                    foreach (var pts in points)
                    {
                        pts.gameObject.SetActive(false);
                    }
                    raycastManager.GetComponent<ARPointCloudManager>().enabled = false;
                    //AR 플레인 숨김
                    var planes = raycastManager.GetComponent<ARPlaneManager>().trackables;
                    foreach (var pls in planes)
                    {
                        pls.gameObject.SetActive(false);
                    }
                    raycastManager.GetComponent<ARPlaneManager>().enabled = false;
                    //AR프리팹을 생성하여 첫번째 터치 지점의 위치와 회전값으로 초기화한다
                    //arObject = Instantiate(arPrefab, hitPose.position, hitPose.rotation);
                    arPrefab.SetActive(true);
                    arPrefab.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    arObject = arPrefab;
                }
                //생성된 AR오브젝트가 있을 경우
                else
                {
                    //터치한 지점으로 생성된 AR오브젝트를 재배치한다
                    //arObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }
        }


    }
}
