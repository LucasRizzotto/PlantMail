using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ThriveManager : MonoBehaviour {

	public Plant plant;
	public UnityEvent onInboxEmptied;

	public int INBOXStartSize = 3;
	public int emailsInInbox = 3;
	public bool inboxIsEmpty = false;

	private GrowthManager GM;

	public int maxEmailCount = 20;

	// Use this for initialization
	void Start () {
		//plant = GetComponentInChildren<Plant>();
		GM = GetComponentInChildren<GrowthManager>();

		ChangeNumberOfEmails(INBOXStartSize);


	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			AddEmail();
		}
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			RemoveEmail();
		}
	}

	public void ChangeNumberOfEmails (int number)
	{
		if (emailsInInbox == number)
		{
			return;
		}
		else
		{
			if (emailsInInbox < number)
			{
				while(emailsInInbox != number)
				{
					AddEmail();
				}
			}
			else
			{
				while(emailsInInbox != number)
				{
					RemoveEmail();
				}
			}
		}
	}

	public void AddEmail()
	{
		if (inboxIsEmpty && emailsInInbox == 0)
		{
			inboxIsEmpty = false;
		}

		GM.GrowPlant();
		emailsInInbox++;
	}
	public void RemoveEmail()
	{
		GM.ShrinkPlant();
		emailsInInbox--;

		if (!inboxIsEmpty && emailsInInbox == 0)
		{
			InboxEmpty();
		}
	}
	public void InboxEmpty()
	{
		inboxIsEmpty = true;
		onInboxEmptied.Invoke();
	}
}
