using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrowthManager : MonoBehaviour {

	public float plantSize = 3.0f;
	public float heightScale = 0.03f;
	public UnityEvent onSizeChanges;

	// public GameObject cleanInboxEffect;

	private float sizeIncrement = 0.1f;
	private Plant plant;
	private ThriveManager TM;
	// private SeedSpawner sSpawn;
	private float plantRadius;
	private AudioSource audi;
	private float startPitch = 1.6f;
	private float pitchIncrement = 0.13f;
	private SeedManager SM;




	//Debug.Log(Mathf.Log(6, 2));

	// Use this for initialization
	void Start () {
		TM = GetComponentInParent<ThriveManager>();
		SM = TM.gameObject.GetComponentInChildren<SeedManager>();
		plantSize = TM.emailsInInbox;
		plant = TM.plant;
		plantRadius = 1f;
		audi = GetComponent<AudioSource>();
		audi.pitch = startPitch;

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public float GetRadius()
	{
		return plantRadius;
	}
	public float GetRadialStep()
	{
		return Mathf.Pow(sizeIncrement, 3f);
	}


	public void GrowPlant()
	{
		plantSize++;
		plant.gameObject.transform.localScale += new Vector3(sizeIncrement,sizeIncrement,sizeIncrement);
		plant.gameObject.transform.Translate(Vector3.up * heightScale, Space.World);
		onSizeChanges.Invoke();
		plantRadius += Mathf.Pow(sizeIncrement, 3f);
		SM.Push();
		audi.pitch -= pitchIncrement;
		audi.Play();
	}

	public void ShrinkPlant()
	{
		plantSize--;
		plant.gameObject.transform.localScale -= new Vector3(sizeIncrement,sizeIncrement,sizeIncrement);
		plant.gameObject.transform.Translate(Vector3.down * heightScale, Space.World);
		onSizeChanges.Invoke();
		plantRadius -= Mathf.Pow(sizeIncrement, 3f);
		SM.Pull();
		audi.pitch += pitchIncrement;
		audi.Play();
	}


}
