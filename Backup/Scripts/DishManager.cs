using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public List<GameObject> fishList = new List<GameObject>(); // ���ÿ� ��� �����

    private int fishCount; // ����� ��
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���ÿ� ��� �����
    public void FishListUpdate()
    {
        if(fishCount > 0) // ���ÿ� ��� ����Ⱑ 1�� �̻��� ���
        {
            if(fishList.Count != fishCount) // ����� ����Ʈ�� ������ ����� ���� �ٸ� ���
            {
                fishList.Clear(); // ����� ����Ʈ �ʱ�ȭ

            }
        }
    }
}
