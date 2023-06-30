using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDetect : MonoBehaviour
{


    public float detectRange; // 감지 범위
    public List<GameObject> ballList = new List<GameObject>();
    public void OnDrawGizmos()
    {

        // 와이어 스피어를 그린다.( 그릴 위치, 반지름)
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
