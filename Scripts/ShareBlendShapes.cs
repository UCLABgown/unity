using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareBlendShapes : MonoBehaviour
{
    public SkinnedMeshRenderer[] arr;
    private SkinnedMeshRenderer me;
    public int shapeCount = 13;
    // Start is called before the first frame update
    void Start()
    {
        me = gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (var r in arr){
            for(int i = 0; i<shapeCount ; i++){
                r.SetBlendShapeWeight(i,me.GetBlendShapeWeight(i));
            }
        }
        
    }
}
