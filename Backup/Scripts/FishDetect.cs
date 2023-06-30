using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDetect : MonoBehaviour
{
    public float detectRange; // 감지 범위
    public int fishCount;

    public List<GameObject> fishList = new List<GameObject>();

    public GameObject ropePrefab;
    public List<GameObject> ropeList = new List<GameObject>();

    bool ropeSign = true;
    // 기즈모 그리는 함수
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
        //int layerMask = 1 << 6; // 6번 레이어
        //// 스피어 캐스트를 그린다. (그릴 위치, 크기, 방향, 길이, 레이어 마스크)
        //var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);
        //if (fishList.Contains(null))
        //{
        //    fishList.Clear();
        //}
        //fishCount = hits.Length;
        //if(fishList.Count < hits.Length)
        //{
        //    for(int i = 0; i < hits.Length; i++)
        //    {
        //        if (!fishList.Contains(hits[i].transform.gameObject))
        //        {
        //            fishList.Add(hits[i].transform.gameObject);
        //        }
        //    }
        //}
        //if(fishList.Count != hits.Length)
        //{
        //    fishList.Clear();
        //    ReduceRope();
        //    for(int i = 0; i < hits.Length; i++)
        //    {
        //        fishList.Add(hits[i].transform.gameObject);
        //    }
        //}

    }

    // 물고기 추가 함수
    public void AddFish(GameObject fish)
    {
        fishList.Add(fish);
    }

    // 물고기 감소 함수
    public void ReduceFish(GameObject fish)
    {
        ReduceRope();
        for (int i = 0; i < fishList.Count; i++)
        {
            if (fishList[i] == fish)
            {
                fishList.RemoveAt(i);
            }
        }
    }

    public void CancleFish(GameObject fish)
    {
        for (int i = 0; i < fishList.Count; i++)
        {
            if (fishList[i] == fish)
            {
                fishList.RemoveAt(i);
            }
        }
    }


    // 로프 제거
    public void ReduceRope()
    {
        for (int i = 0; i < ropeList.Count; i++)
        {
            Destroy(ropeList[i]);
        }
        ropeList.Clear();
    }
    public void FishInDish()
    {
        int layerMask = 1 << 6;

        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        if (ropeList.Count < fishList.Count - 1)
        {
            CreateRope(fishList.Count - 2, fishList.Count - 1);

        }
        else if (ropeList.Count > fishList.Count - 1)
        {
            ReduceRope();
        }
    }
    public void CreateRope(int a, int b)
    {
        if (ropeList.Count == 0)
        {
            // 로프 오브젝트 생성
            GameObject rope = Instantiate(ropePrefab);
            // 로프 이름 지정
            rope.transform.name = "Rope" + a.ToString() + "_" + b.ToString();
            // 로프의 첫번째 위치를 첫번째 물고기 매듭 위치로 지정
            rope.GetComponent<Rope>().lines.Add(fishList[a].GetComponent<DragFish>().fishSprite[2].transform);
            // 로프의 두번째 위치를 두번째 물고기 매듭 위치로 지정
            rope.GetComponent<Rope>().lines.Add(fishList[b].GetComponent<DragFish>().fishSprite[2].transform);
            ropeList.Add(rope);
        }

        // 로프 개수가 1개 이상일 때 
        else
        {
            // 전체 로프 중에서
            for (int i = 0; i < ropeList.Count; i++)
            {
                //로프 이름이 "Rope[a]_[b]"가 없을 때
                if (ropeList[i].transform.name != "Rope" + a.ToString() + "_" + b.ToString())
                {
                    //로프 사인 true 일 때 
                    if (ropeSign)
                    {
                        // 로프 오브젝트 생성
                        GameObject rope = Instantiate(ropePrefab);
                        // 로프 이름 지정
                        rope.transform.name = "Rope" + a.ToString() + "_" + b.ToString();
                        // 로프의 첫번째 위치를 첫번째 물고기 매듭의 위치로 지정
                        rope.GetComponent<Rope>().lines.Add(fishList[a].GetComponent<DragFish>().fishSprite[2].transform);
                        // 로프의 두번째 위치를 두번째 물고기 매듭의 위치로 지정
                        rope.GetComponent<Rope>().lines.Add(fishList[b].GetComponent<DragFish>().fishSprite[2].transform);
                        // 로프 배열에 로프 추가
                        ropeList.Add(rope);
                        StartCoroutine(RopeSignReset()); // 로프 사인 true 지연 호출
                        ropeSign = false; // 로프 사인 false
                    }
                }
            }

        }
    }
    IEnumerator RopeSignReset()
    {
        yield return new WaitForSeconds(0.2f);
        ropeSign = true;
    }
    public void FishReset()
    {
        fishList.Clear();
    }
    public void RopeRebuild()
    {
        StartCoroutine(RopeRebuildStart());
    }
    IEnumerator RopeRebuildStart()
    {
        yield return new WaitForSeconds(0.2f);


        for (int i = 0; i < fishList.Count - 1; i++)
        {
            // 로프 오브젝트 생성
            GameObject rope = Instantiate(ropePrefab);
            // 로프 이름 지정
            rope.transform.name = "Rope" + i.ToString() + "_" + (i + 1).ToString();
            // 로프의 첫번째 위치를 첫번째 물고기 매듭의 위치로 지정
            rope.GetComponent<Rope>().lines.Add(fishList[i].GetComponent<DragFish>().fishSprite[2].transform);
            // 로프의 두번째 위치를 두번째 물고기 매듭의 위치로 지정
            rope.GetComponent<Rope>().lines.Add(fishList[i + 1].GetComponent<DragFish>().fishSprite[2].transform);
            // 로프 배열에 로프 추가
            ropeList.Add(rope);

        }
    }
}
