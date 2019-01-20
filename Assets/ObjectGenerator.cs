using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {

																																							/*
 ______________________________________________________________________________
|--------------------------------INSTANTIATIONS--------------------------------|
|______________________________________________________________________________|
																																							*/

																		// the PREFAB to instantiate
																	public GameObject prefab;

																		// the TRANSFORM of the GENERATE center
																	public Transform generateAtLocation;

																		// repeat GENERATE prefab?
																	public bool generateContinuously = true;

																		// DISTANCE around LOCATION to GENERATE
																	public float radiusOfGeneration = 0.0f;

																		// VARIABILITY of spawn POSITION
																	public float positionRandomness = 0.0f;

																		// TIME (SECONDS) before first OBJECT spawn
																	public float initialDelay = 0.0f;

																		// TIME (SECONDS) between spawns
																	public float generationInterval = 1.0f;




																																							/*
 ______________________________________________________________________________
|----------------------------------FUNCTIONS-----------------------------------|
|______________________________________________________________________________|
																																							*/

	// 
  //   // STARTs the CREATE PREFAB FUNCTIONS with input DELAY and INTERVAL times
	// void Start ()
	// {
	// 		InvokeRepeating("singleCreate", initialDelay, generationInterval);
	// 	}
	// }
	//
	//
	// // the single PREFAB generate FUNCTION
	// void singleCreate()
	// {
	// 	for (int i = 0; i < seedAmount; i++)
	// 	{
	//
	//
	// 	// finds a position RADIUS DISTANCE from GENERATEATLOCATIONs Location
	// 	var fromObject = generateAtLocation.position + (Random.onUnitSphere * radiusOfGeneration);
	//
	// 	// generates whole POSITION and ROTATION VARIABLES (IDENTITY: 0 for each)
	// 	var prefabPosition = new Vector3(fromObject.x, fromObject.y, fromObject.z);
	// 	var prefabRotation = Quaternion.identity;
	//
	// 	// GENERATES the PREFAB at POSITION : above (X, Y, Z) and ROTATION
	// 	Instantiate (prefab, prefabPosition, prefabRotation);
	// 	}
	// }
}
