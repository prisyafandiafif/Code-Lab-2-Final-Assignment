using UnityEngine;
using UnityEngine.UI;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using System.Collections;
using System.Collections.Generic;

public class ActionManager : MonoBehaviour 
{
	public static ActionManager instance;
	
	//list of current actions
	public List<int> currentActions = new List<int>(); 

	public Image actionBig;
	public Image action1;
	public Image action2;
	public Image action3;
	public Image action4;
	public Image action5;
	public Image action6;

	public Sprite emptyIcon;
	public Sprite monster1Icon;
	public Sprite monster2Icon;
	public Sprite monster3Icon;
	public Sprite resetIcon;

	//list of actions that will be repeated
	//0 is empty
	//1 is monster 1 will attack
 	//2 is monster 2 will attack
	//3 is monster 3 will attack
	//4 is reset all weapon
	public List<int> actionsPattern = new List<int>();

	public GameObject selectedMonster;

	public GameObject loseScreen;
	public GameObject winScreen;

	//save the last index of action in actionsPattern;
	private int lastIdx = 6;

	private Tweener actionTweenerBig;
	private Tweener actionTweener1Position;
	private Tweener actionTweener1Scale;
	private Tweener actionTweener2;
	private Tweener actionTweener3;
	private Tweener actionTweener4;
	private Tweener actionTweener5;
	private Tweener actionTweener6;

	private Die dieScript;
	private Enemy monster1Script;
	private Enemy monster2Script;
	private Enemy monster3Script;

	[HideInInspector] public int firstCurrentAction;

	// Use this for initialization
	void Awake ()
	{
		instance = this;
	}

	void Start () 
	{
		Time.timeScale = 2f;

		//init actions
		for (int i = 0; i < 7; i++)
		{
			currentActions[i] = actionsPattern[i];
			PutActionSprite(actionsPattern[i], i);
		}

		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 20; j++)
			{
				actionsPattern.Add(actionsPattern[j]);
			}
		}

		dieScript = GameObject.Find("Die").GetComponent<Die>();

		monster1Script = GameObject.Find("Monster1").GetComponent<Enemy>();
		monster2Script = GameObject.Find("Monster2").GetComponent<Enemy>();
		monster3Script = GameObject.Find("Monster3").GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (dieScript.healthPoint <= 0)
		{
			dieScript.isAnimating = true;

			loseScreen.SetActive(true);
		}
		else
		if (monster1Script.healthPoint <= 0 && monster2Script.healthPoint <= 0 && monster3Script.healthPoint <= 0)
		{
			dieScript.isAnimating = true;

			winScreen.SetActive(true);
		}

		if (monster1Script.healthPoint <= 0)
		{
			monster1Script.gameObject.SetActive(false);
			monster1Script.healthText.gameObject.SetActive(false);

			//remove monster 1 action pattern
			for (int i = 0; i < actionsPattern.Count; i++)
			{
				if (actionsPattern[i] == 1)
				{
					//change it to empty
					actionsPattern[i] = 0;
				}

				//init actions
				int idx = 0;

				for (int j = lastIdx - 6; j < lastIdx; j++)
				{
					currentActions[idx] = actionsPattern[j];
					PutActionSprite(actionsPattern[j], idx);
					idx += 1;
				}

				//put the arrow in an active monster
				GameObject monsters = GameObject.Find("Monsters");

				for (int k = 0; k < monsters.transform.childCount; k++)
				{
					if (monsters.transform.GetChild(k).gameObject.activeSelf)
					{
						monsters.transform.GetChild(k).gameObject.transform.FindChild("Arrow").gameObject.SetActive(true);

						selectedMonster = monsters.transform.GetChild(k).gameObject;

						break;
					}
				}
			}
		}

		if (monster2Script.healthPoint <= 0 && monster2Script.gameObject.activeSelf)
		{
			monster2Script.gameObject.SetActive(false);
			monster2Script.healthText.gameObject.SetActive(false);

			//remove monster 2 action pattern
			for (int i = 0; i < actionsPattern.Count; i++)
			{
				if (actionsPattern[i] == 2)
				{
					//change it to empty
					actionsPattern[i] = 0;
				}

				//init actions
				int idx = 0;

				for (int j = lastIdx - 6; j < lastIdx; j++)
				{
					currentActions[idx] = actionsPattern[j];
					PutActionSprite(actionsPattern[j], idx);
					idx += 1;
				}

				//put the arrow in an active monster
				GameObject monsters = GameObject.Find("Monsters");

				for (int k = 0; k < monsters.transform.childCount; k++)
				{
					if (monsters.transform.GetChild(k).gameObject.activeSelf)
					{
						monsters.transform.GetChild(k).gameObject.transform.FindChild("Arrow").gameObject.SetActive(true);

						selectedMonster = monsters.transform.GetChild(k).gameObject;
	
						break;
					}
				}
			}
		}

		if (monster3Script.healthPoint <= 0)
		{
			monster3Script.gameObject.SetActive(false);
			monster3Script.healthText.gameObject.SetActive(false);

			//remove monster 3 action pattern
			for (int i = 0; i < actionsPattern.Count; i++)
			{
				if (actionsPattern[i] == 3)
				{
					//change it to empty
					actionsPattern[i] = 0;
				}

				//init actions
				int idx = 0;

				for (int j = lastIdx - 6; j < lastIdx; j++)
				{
					currentActions[idx] = actionsPattern[j];
					PutActionSprite(actionsPattern[j], idx);
					idx += 1;
				}

				//put the arrow in an active monster
				GameObject monsters = GameObject.Find("Monsters");

				for (int k = 0; k < monsters.transform.childCount; k++)
				{
					if (monsters.transform.GetChild(k).gameObject.activeSelf)
					{
						monsters.transform.GetChild(k).gameObject.transform.FindChild("Arrow").gameObject.SetActive(true);

						selectedMonster = monsters.transform.GetChild(k).gameObject;

						break;
					}
				}
			}
		}
	}

	// Check if a sword icon exists or not
	public void CheckSword ()
	{
		GameObject tiles = GameObject.Find("Tiles");	

		GameObject tileOfDie = null;

		for (int i = 0; i < tiles.transform.childCount; i++)
		{
			if (dieScript.gameObject.transform.FindChild("Model").gameObject.transform.position.x < tiles.transform.GetChild(i).gameObject.transform.position.x + 0.1f
			&& dieScript.gameObject.transform.FindChild("Model").gameObject.transform.position.x > tiles.transform.GetChild(i).gameObject.transform.position.x - 0.1f
			&& dieScript.gameObject.transform.FindChild("Model").gameObject.transform.position.z < tiles.transform.GetChild(i).gameObject.transform.position.z + 0.1f
			&& dieScript.gameObject.transform.FindChild("Model").gameObject.transform.position.z > tiles.transform.GetChild(i).gameObject.transform.position.z - 0.1f)
			{
				tileOfDie = tiles.transform.GetChild(i).gameObject;

				break;
			}
		}

		Debug.Log("Tile: " + tileOfDie.transform.position);

		if (tileOfDie.transform.childCount > 0 && tileOfDie.transform.FindChild("Sword").gameObject.activeSelf)
		{
			tileOfDie.transform.FindChild("Sword").gameObject.SetActive(false);

			//attack the selected monster
			StartCoroutine(Attack(dieScript.gameObject, selectedMonster));
		}
	}

	// Attack a creature
	public IEnumerator Attack (GameObject source, GameObject target)
	{
		//if attacker is stronger in element
		if (CheckElement(source, target) == 1)
		{
			Debug.Log("Attack " + target.name);

			target.GetComponent<Creature>().healthPoint -= 2 * source.GetComponent<Creature>().damage;
		} 
		else
		//if attacker is equal in element
		if (CheckElement(source, target) == 0)
		{
			Debug.Log("Attack " + target.name);

			target.GetComponent<Creature>().healthPoint -= source.GetComponent<Creature>().damage;
		} 
		else
		//if attacker is weaker in element
		if (CheckElement(source, target) == -1)
		{
			Debug.Log("Attack " + target.name);

			target.GetComponent<Creature>().healthPoint -= 0;
		} 

		yield return new WaitForSeconds(1f);
	}

	// Compare elements
	// 1 is strong
	// 2 is equal
	// 3 is weaker 
	public int CheckElement (GameObject source, GameObject target)
	{
		if (source.GetComponent<Creature>().elementID == 1 && target.GetComponent<Creature>().elementID == 2)
		{
			return 1;
		}
		else
		if (source.GetComponent<Creature>().elementID == 1 && target.GetComponent<Creature>().elementID == 3)
		{
			return -1;
		}
		else
		if (source.GetComponent<Creature>().elementID == 1 && target.GetComponent<Creature>().elementID == 1)
		{
			return 0;
		}
		else
		if (source.GetComponent<Creature>().elementID == 2 && target.GetComponent<Creature>().elementID == 3)
		{
			return 1;
		}
		else
		if (source.GetComponent<Creature>().elementID == 2 && target.GetComponent<Creature>().elementID == 1)
		{
			return -1;
		}
		else
		if (source.GetComponent<Creature>().elementID == 2 && target.GetComponent<Creature>().elementID == 2)
		{
			return 0;
		}
		else
		if (source.GetComponent<Creature>().elementID == 3 && target.GetComponent<Creature>().elementID == 1)
		{
			return 1;
		}
		else
		if (source.GetComponent<Creature>().elementID == 3 && target.GetComponent<Creature>().elementID == 2)
		{
			return -1;
		}
		else
		if (source.GetComponent<Creature>().elementID == 3 && target.GetComponent<Creature>().elementID == 3)
		{
			return 0;
		}

		return 2;
	}

	// Execute action
	public IEnumerator ExecuteAction ()
	{
		//if monster 1 attack
		if (firstCurrentAction == 1)
		{
			//if monster 1 is stronger than die
			if (CheckElement(monster1Script.gameObject, dieScript.gameObject) == 1)
			{
				dieScript.healthPoint -= 2 * monster1Script.damage;
			}
			else
			//if monster 1 is stronger than die
			if (CheckElement(monster1Script.gameObject, dieScript.gameObject) == 0)
			{
				dieScript.healthPoint -= monster1Script.damage;
			}
			else
			//if monster 1 is stronger than die
			if (CheckElement(monster1Script.gameObject, dieScript.gameObject) == -1)
			{
				dieScript.healthPoint -= 0;
			}
		}
		else
		//if monster 2 attack
		if (firstCurrentAction == 2)
		{
			//if monster 2 is stronger than die
			if (CheckElement(monster2Script.gameObject, dieScript.gameObject) == 1)
			{
				dieScript.healthPoint -= 2 * monster2Script.damage;
			}
			else
			//if monster 2 is stronger than die
			if (CheckElement(monster2Script.gameObject, dieScript.gameObject) == 0)
			{
				dieScript.healthPoint -= monster2Script.damage;
			}
			else
			//if monster 2 is stronger than die
			if (CheckElement(monster2Script.gameObject, dieScript.gameObject) == -1)
			{
				dieScript.healthPoint -= 0;
			}
		}
		else
		//if monster 3 attack
		if (firstCurrentAction == 3)
		{
			//if monster 3 is stronger than die
			if (CheckElement(monster3Script.gameObject, dieScript.gameObject) == 1)
			{
				dieScript.healthPoint -= 2 * monster3Script.damage;
			}
			else
			//if monster 3 is stronger than die
			if (CheckElement(monster3Script.gameObject, dieScript.gameObject) == 0)
			{
				dieScript.healthPoint -= monster3Script.damage;
			}
			else
			//if monster 3 is stronger than die
			if (CheckElement(monster3Script.gameObject, dieScript.gameObject) == -1)
			{
				dieScript.healthPoint -= 0;
			}
		}
		else
		//if reset
		if (firstCurrentAction == 4)
		{
			GameObject tiles = GameObject.Find("Tiles");

			for (int i = 0; i < tiles.transform.childCount; i++)
			{
				if (tiles.transform.GetChild(i).gameObject.transform.childCount > 0)
				{
					tiles.transform.GetChild(i).gameObject.transform.FindChild("Sword").gameObject.SetActive(true);
				}
			}
		}

		yield return new WaitForSeconds(1f);
	}

	// Move actions ahead
	public IEnumerator MoveAnActionAhead ()
	{
		yield return new WaitForSeconds(1f);

		//save init position
		Vector2[] initActionIconPosition = new Vector2[7];

		initActionIconPosition[0] = actionBig.gameObject.GetComponent<RectTransform>().anchoredPosition;
		initActionIconPosition[1] = action1.gameObject.GetComponent<RectTransform>().anchoredPosition;
		initActionIconPosition[2] = action2.gameObject.GetComponent<RectTransform>().anchoredPosition;
		initActionIconPosition[3] = action3.gameObject.GetComponent<RectTransform>().anchoredPosition;
		initActionIconPosition[4] = action4.gameObject.GetComponent<RectTransform>().anchoredPosition;
		initActionIconPosition[5] = action5.gameObject.GetComponent<RectTransform>().anchoredPosition;
		initActionIconPosition[6] = action6.gameObject.GetComponent<RectTransform>().anchoredPosition;

		actionTweenerBig = HOTween.To
		(
			actionBig.gameObject.GetComponent<RectTransform>(), 
			0.5f, 
			new TweenParms()
		        .Prop
				(
					"sizeDelta", 
					new Vector2
					(
						0f,
						0f
					),
					false	
				) 
		        .Ease(EaseType.EaseOutExpo)
				.Delay(0f)
		);

		actionTweener1Position = HOTween.To
		(
			action1.gameObject.GetComponent<RectTransform>(), 
			0.5f, 
			new TweenParms()
		        .Prop
				(
					"anchoredPosition", 
					new Vector2
					(
						150f,
						-26.68f
					),
					true	
				) 
		        .Ease(EaseType.EaseOutExpo)
				.Delay(0f)
		);
		
		actionTweener1Scale = HOTween.To
		(
			action1.gameObject.GetComponent<RectTransform>(), 
			0.5f, 
			new TweenParms()
		        .Prop
				(
					"sizeDelta", 
					new Vector2
					(
						180f,
						180
					),
					false	
				) 
		        .Ease(EaseType.EaseOutExpo)
				.Delay(0f)
		);

		actionTweener2 = HOTween.To
		(
			action2.gameObject.GetComponent<RectTransform>(), 
			0.5f, 
			new TweenParms()
		        .Prop
				(
					"anchoredPosition", 
					new Vector2
					(
						120f,
						0f
					),
					true	
				) 
		        .Ease(EaseType.EaseOutExpo)
				.Delay(0f)
		);

		actionTweener3 = HOTween.To
		(
			action3.gameObject.GetComponent<RectTransform>(), 
			0.5f, 
			new TweenParms()
		        .Prop
				(
					"anchoredPosition", 
					new Vector2
					(
						120f,
						0f
					),
					true	
				) 
		        .Ease(EaseType.EaseOutExpo)
				.Delay(0f)
		);

		actionTweener4 = HOTween.To
		(
			action4.gameObject.GetComponent<RectTransform>(), 
			0.5f, 
			new TweenParms()
		        .Prop
				(
					"anchoredPosition", 
					new Vector2
					(
						120f,
						0f
					),
					true	
				) 
		        .Ease(EaseType.EaseOutExpo)
				.Delay(0f)
		);

		actionTweener5 = HOTween.To
		(
			action5.gameObject.GetComponent<RectTransform>(), 
			0.5f, 
			new TweenParms()
		        .Prop
				(
					"anchoredPosition", 
					new Vector2
					(
						120f,
						0f
					),
					true	
				) 
		        .Ease(EaseType.EaseOutExpo)
				.Delay(0f)
		);

		actionTweener6 = HOTween.To
		(
			action6.gameObject.GetComponent<RectTransform>(), 
			0.5f, 
			new TweenParms()
		        .Prop
				(
					"anchoredPosition", 
					new Vector2
					(
						120f,
						0f
					),
					true	
				) 
		        .Ease(EaseType.EaseOutExpo)
				.Delay(0f)
		);

		yield return new WaitForSeconds(0.5f);

		lastIdx = lastIdx + 1;

		int idx = 0;

		for (int i = lastIdx - 6; i < lastIdx; i++)
		{
			currentActions[idx] = actionsPattern[i];
			PutActionSprite(actionsPattern[i], idx);
			idx += 1;
		}

		//reset position and scale of each action
		actionBig.gameObject.GetComponent<RectTransform>().anchoredPosition = initActionIconPosition[0];
		actionBig.gameObject.GetComponent<RectTransform>().sizeDelta =  new Vector2(180f, 180f);
		action1.gameObject.GetComponent<RectTransform>().anchoredPosition = initActionIconPosition[1];
		action1.gameObject.GetComponent<RectTransform>().sizeDelta =  new Vector2(120f, 120f);
		action2.gameObject.GetComponent<RectTransform>().anchoredPosition = initActionIconPosition[2];
		action2.gameObject.GetComponent<RectTransform>().sizeDelta =  new Vector2(120f, 120f);
		action3.gameObject.GetComponent<RectTransform>().anchoredPosition = initActionIconPosition[3];
		action3.gameObject.GetComponent<RectTransform>().sizeDelta =  new Vector2(120f, 120f);
		action4.gameObject.GetComponent<RectTransform>().anchoredPosition = initActionIconPosition[4];
		action4.gameObject.GetComponent<RectTransform>().sizeDelta =  new Vector2(120f, 120f);
		action5.gameObject.GetComponent<RectTransform>().anchoredPosition = initActionIconPosition[5];
		action5.gameObject.GetComponent<RectTransform>().sizeDelta =  new Vector2(120f, 120f);
		action6.gameObject.GetComponent<RectTransform>().anchoredPosition = initActionIconPosition[6];
		action6.gameObject.GetComponent<RectTransform>().sizeDelta =  new Vector2(120f, 120f);

		dieScript.isAnimating = false;
	}

	public void PutActionSprite (int actionsPatternID, int currentActionID)
	{
		//if empty icon
		if (actionsPatternID == 0)
		{
			if (currentActionID == 0)
			{
				actionBig.sprite = emptyIcon;
			}
			else
			if (currentActionID == 1)
			{
				action1.sprite = emptyIcon;
			}
			else
			if (currentActionID == 2)
			{
				action2.sprite = emptyIcon;
			}
			else
			if (currentActionID == 3)
			{
				action3.sprite = emptyIcon;
			}
			else
			if (currentActionID == 4)
			{
				action4.sprite = emptyIcon;
			}
			else
			if (currentActionID == 5)
			{
				action5.sprite = emptyIcon;
			}
			else
			if (currentActionID == 6)
			{
				action6.sprite = emptyIcon;
			}
		}
		else
		//if monster 1 icon
		if (actionsPatternID == 1)
		{
			if (currentActionID == 0)
			{
				actionBig.sprite = monster1Icon;
			}
			else
			if (currentActionID == 1)
			{
				action1.sprite = monster1Icon;
			}
			else
			if (currentActionID == 2)
			{
				action2.sprite = monster1Icon;
			}
			else
			if (currentActionID == 3)
			{
				action3.sprite = monster1Icon;
			}
			else
			if (currentActionID == 4)
			{
				action4.sprite = monster1Icon;
			}
			else
			if (currentActionID == 5)
			{
				action5.sprite = monster1Icon;
			}
			else
			if (currentActionID == 6)
			{
				action6.sprite = monster1Icon;
			}
		}
		else
		//if monster 2 icon
		if (actionsPatternID == 2)
		{
			if (currentActionID == 0)
			{
				actionBig.sprite = monster2Icon;
			}
			else
			if (currentActionID == 1)
			{
				action1.sprite = monster2Icon;
			}
			else
			if (currentActionID == 2)
			{
				action2.sprite = monster2Icon;
			}
			else
			if (currentActionID == 3)
			{
				action3.sprite = monster2Icon;
			}
			else
			if (currentActionID == 4)
			{
				action4.sprite = monster2Icon;
			}
			else
			if (currentActionID == 5)
			{
				action5.sprite = monster2Icon;
			}
			else
			if (currentActionID == 6)
			{
				action6.sprite = monster2Icon;
			}
		}
		else
		//if monster 3 icon
		if (actionsPatternID == 3)
		{
			if (currentActionID == 0)
			{
				actionBig.sprite = monster3Icon;
			}
			else
			if (currentActionID == 1)
			{
				action1.sprite = monster3Icon;
			}
			else
			if (currentActionID == 2)
			{
				action2.sprite = monster3Icon;
			}
			else
			if (currentActionID == 3)
			{
				action3.sprite = monster3Icon;
			}
			else
			if (currentActionID == 4)
			{
				action4.sprite = monster3Icon;
			}
			else
			if (currentActionID == 5)
			{
				action5.sprite = monster3Icon;
			}
			else
			if (currentActionID == 6)
			{
				action6.sprite = monster3Icon;
			}
		}
		else
		//if reset icon
		if (actionsPatternID == 4)
		{
			if (currentActionID == 0)
			{
				actionBig.sprite = resetIcon;
			}
			else
			if (currentActionID == 1)
			{
				action1.sprite = resetIcon;
			}
			else
			if (currentActionID == 2)
			{
				action2.sprite = resetIcon;
			}
			else
			if (currentActionID == 3)
			{
				action3.sprite = resetIcon;
			}
			else
			if (currentActionID == 4)
			{
				action4.sprite = resetIcon;
			}
			else
			if (currentActionID == 5)
			{
				action5.sprite = resetIcon;
			}
			else
			if (currentActionID == 6)
			{
				action6.sprite = resetIcon;
			}
		}
	}

	public void OnRetryButtonClicked ()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}
}
