using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public List<GameObject> fishList = new List<GameObject>(); // 접시에 담긴 물고기

    private int fishCount; // 물고기 수
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 접시에 담긴 물고기
    public void FishListUpdate()
    {
        if(fishCount > 0) // 접시에 담긴 물고기가 1개 이상일 경우
        {
            if(fishList.Count != fishCount) // 물고기 리스트의 개수와 물고기 수가 다를 경우
            {
                fishList.Clear(); // 물고기 리스트 초기화

            }
        }
    }
}
