using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDetect : MonoBehaviour
{


    public float detectRange; // ���� ����
    public List<GameObject> ballList = new List<GameObject>();
    public void OnDrawGizmos()
    {

        // ���̾� ���Ǿ �׸���.( �׸� ��ġ, ������)
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
