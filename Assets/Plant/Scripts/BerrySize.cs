using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerrySize : MonoBehaviour {

    [Range(1f, 3f)]
    public float SizeMultiplier = 1f;

    private Vector3 InitialScale;

    public void Start()
    {
        InitialScale = transform.localScale;
        UpdateSize();
    }

    void UpdateSize()
    {
        transform.localScale = InitialScale * SizeMultiplier;
    }

}
