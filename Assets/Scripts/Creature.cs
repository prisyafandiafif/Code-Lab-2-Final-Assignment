using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Creature : MonoBehaviour 
{
	//health point of each creature
	public int healthPoint;
	//element ID of each creature. 1 is red, 2 is green, 3 is blue
	public int elementID;
	//damage that will be dealt to the opponent
	public int damage;
	//text of health
	public Text healthText;

	protected int initHealthPoint;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
