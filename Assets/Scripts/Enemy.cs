using UnityEngine;
using System.Collections;

public class Enemy : Creature
 {

	// Use this for initialization
	void Start () 
	{
		initHealthPoint = healthPoint;
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthText.text = healthPoint + "/" + initHealthPoint;
	}

	void OnMouseUpAsButton ()
	{
		for (int i = 0; i < 3; i++)
		{
			this.gameObject.transform.parent.gameObject.transform.GetChild(i).gameObject.transform.FindChild("Arrow").gameObject.SetActive(false);
		}

		ActionManager.instance.selectedMonster = this.gameObject;
	
		this.gameObject.transform.FindChild("Arrow").gameObject.SetActive(true);
	}
}
