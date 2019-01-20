using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBerry : MonoBehaviour {

    public bool GoldBerryActive = false;
    [Space(5)]
    public Renderer TargetRenderer;
    public Material GoldMaterial;

    private void OnEnable()
    {
        if(GoldBerryActive)
        {
            TargetRenderer.sharedMaterial = GoldMaterial;
        }
    }
}
