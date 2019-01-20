using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSeedSpawner : MonoBehaviour {

																																							/*
 ______________________________________________________________________________
|--------------------------------INSTANTIATIONS--------------------------------|
|______________________________________________________________________________|
																																							*/

																		// the PREFAB to instantiate
																	public GameObject seedPrefab;

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

																	private GameObject[] berries;
																	private int emailCount;
																	private int seedCount = 0;


																	private ThriveManager TM;
																	private GrowthManager GM;


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

		emailCount = TM.emailsInInbox;

			// if GENERATECONTINUOUSLY is FALSE then SINGLECREATE() PREFEAB
		if (!generateContinuously)
		{

				// INVOKE()s the SINGLECREATE() FUNCTION after INITIALDELAY seconds
			Invoke ("singleCreate", initialDelay);
		}

			// if GENERATECONTINUOUSLY is TRUE then REPEAT command SINGLECREATE()
		else
		{
			berries = new GameObject[TM.maxEmailCount];


				// INVOKE()s "singleCreate()" FUNCTION every GENERATIONINTERVAL seconds
				// after first WAITING for INITIALDELAY seconds
			InvokeRepeating("singleCreate", initialDelay, generationInterval);

				//----BELOW IS INDENTICAL EFFECT CODE AS A PARALLEL COROUTINE----//

				// begins REPEATCREATE() as an independent parallel process
			// StartCoroutine (repeatingCreate());
		}
	}


	// the single PREFAB generate FUNCTION
	void singleCreate()
	{
		if (seedCount < emailCount && seedCount <= TM.maxEmailCount)
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
			berries[seedCount] = Instantiate (seedPrefab, prefabPosition, prefabRotation);
			seedCount++;
		}
		else
		{
			SeedSpawner spawner = gameObject.AddComponent<SeedSpawner>() as SeedSpawner;
			spawner.CarryoverSettings(seedPrefab, generateAtLocation, berries, emailCount);
			Destroy(this);
		}
		//NOT SURE ABOUT THIS
	}
					// alternative to REPEAT CREATE FUNCTION using COROUTINES
					/*
					IEnumerator repeatingCreate()
					{
							// REPEAT this script perpetually
						while (true){

								// make this REPEAT FUNCTION wait GENERATION INTERVAL seconds per CYCLE
							yield return new WaitForSeconds(generationInterval);
						}
					}
					*/
}
