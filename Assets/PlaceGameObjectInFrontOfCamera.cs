using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGameObjectInFrontOfCamera : MonoBehaviour {

    public GameObject TargetGameObject;
    public float Distance = 1f;

	public void PlaceGO()
    {
        TargetGameObject.transform.position = Helpers.GetFrontOfObject(Camera.main.transform, Distance);
        TargetGameObject.transform.LookAt(Camera.main.transform);
    }
}
