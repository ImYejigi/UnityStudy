using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsChange : MonoBehaviour
{
    public Material[] mats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SmileExpression()
    {
        GetComponent<MeshRenderer>().material = mats[0];
    }
    public void SadExpression()
    {
        GetComponent<MeshRenderer>().material = mats[1];
    }
    public void WinkExpression()
    {
        GetComponent<MeshRenderer>().material = mats[2];
    }
}
