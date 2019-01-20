using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour {

	public float seedRadius = 0.5f;
	public float radialShift = 0.1f;
	public GameObject[] berryBush;

	private Transform plant;
	private ThriveManager TM;
	private GrowthManager GM;



	// Use this for initialization
	void Start () {
		TM = GetComponentInParent<ThriveManager>();
		plant = TM.plant.gameObject.transform;
		GM = TM.gameObject.GetComponentInChildren<GrowthManager>();
	//	seedRadius = GM.GetRadius();
	//	radialShift = GM.GetRadialStep();
	}
	void Update()
	{
	}

	public void Push()
	{
		foreach (GameObject berry in berryBush)
		{
			berry.transform.position = Vector3.MoveTowards(berry.transform.position, plant.position, radialShift);
		}
	}

	public void Pull()
	{
		foreach (GameObject berry in berryBush)
		{
			berry.transform.position = Vector3.MoveTowards(berry.transform.position, plant.position, -1 * radialShift);
		}
	}
}
