using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour {

																																							/*
 ______________________________________________________________________________
|--------------------------------INSTANTIATIONS--------------------------------|
|______________________________________________________________________________|
																																							*/

																		// the PREFAB to instantiate
																	public GameObject seedPrefab;

																		// the TRANSFORM of the GENERATE center
																	public Transform generateAtLocation;

																		// DISTANCE around LOCATION to GENERATE
																	public float radiusOfGeneration = 0.5f;

																		// VARIABILITY of spawn POSITION
																	public float positionRandomness = 0.0f;

																	public GameObject[] berries;
																	public int seedCount;


																	public ThriveManager TM;
																	public GrowthManager GM;


																																							/*
 ______________________________________________________________________________
|----------------------------------FUNCTIONS-----------------------------------|
|______________________________________________________________________________|
																																							*/


    // STARTs the CREATE PREFAB FUNCTIONS with input DELAY and INTERVAL times
	void Start ()
	{
		TM = GetComponentInParent<ThriveManager>();
		GM = TM.gameObject.GetComponentInChildren<GrowthManager>();
		radiusOfGeneration = GM.GetRadius();

	}

	public void CarryoverSettings(GameObject prefab, Transform loc, GameObject[] berryList, int emails)
	{
		seedPrefab = prefab;
		generateAtLocation = loc;
		berries = berryList;
		seedCount = emails;
	}


	// the single PREFAB generate FUNCTION
	public void SpawnSeed()
	{
		radiusOfGeneration = GM.GetRadius();

		if (seedCount < TM.maxEmailCount)
		{
			// finds a position RADIUS DISTANCE from GENERATEATLOCATIONs Location
			var fromObject = generateAtLocation.position + (Random.onUnitSphere * radiusOfGeneration);

			float xPosition = Random.Range(-1 * positionRandomness, positionRandomness);
			float yPosition = Random.Range(-1 * positionRandomness, positionRandomness);
			float zPosition = Random.Range(-1 * positionRandomness, positionRandomness);
			// RANDOMIZES a FLOAT for each component of POSITION between + or - positionRandomness

			// generates whole POSITION and ROTATION VARIABLES (IDENTITY: 0 for each)
			var prefabPosition = new Vector3(fromObject.x, fromObject.y, fromObject.z);

			if (positionRandomness > 0)
			{
				prefabPosition = new Vector3(xPosition, yPosition, zPosition);
			}

			var prefabRotation = Quaternion.identity;

			// GENERATES the PREFAB at POSITION : above (X, Y, Z) and ROTATION
			RadialSeedPush();
			berries[seedCount] = Instantiate (seedPrefab, prefabPosition, prefabRotation);
			seedCount++;
		}
	}
	public void DespawnSeed()
	{
		RadialSeedPull();
		Destroy(berries[seedCount]);
		//THIS IS NOT YET COMPLETE, SINCE WE DO NOT KNOW YET WHICH SEED TO DESPAWN
		seedCount--;
	}

	public void RadialSeedPush()
	{
		for (int i = 0; i < seedCount; i++)
		{
			berries[i].transform.position = Vector3.MoveTowards(berries[i].transform.position, generateAtLocation.position, GM.GetRadialStep());
		}
	}
	public void RadialSeedPull()
	{
		for (int i = 0; i < seedCount; i++)
		{
			berries[i].transform.position = Vector3.MoveTowards(berries[i].transform.position, generateAtLocation.position, -1 * GM.GetRadialStep());
		}
	}

}
