using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GogobusAR26Triangle : MonoBehaviour
{
    public GameClearController gameClearController;
    public List<GameObject> allMarbles = new List<GameObject>();
    public List<GameObject> marbles = new List<GameObject>();
    public InputField marbleResult;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(marbles.Count != allMarbles.Count)
        {
            
            if (!marbles.Contains(other.gameObject))
            {
                marbles.Add(other.gameObject);
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        marbles.Clear();
    }


    public void ClearGame()
    {
        if(marbles.Count().ToString() == marbleResult.text)
        {
            gameClearController.UpdateClearCount();
        }
    }
}
