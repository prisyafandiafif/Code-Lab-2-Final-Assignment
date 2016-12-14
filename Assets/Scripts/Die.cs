using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class Die : Creature 
{
	//to save what element is on each side of the die. 
	//index 0 is top
	//index 1 is front
	//index 2 is bottom
	//index 3 is back
	//index 4 is right
	//index 5 is left
	public List<int> elementOnSide = new List<int>();

	public bool isAnimating;

	private GameObject modelGameObject;
	private GameObject anchorPointGameObject;

	private Tweener dieTweener;

	private Vector3 initMousePosition;

	// Use this for initialization
	void Start () 
	{
		initHealthPoint = healthPoint;

		//assign elementID as the first index of elementOnSide
		elementID = elementOnSide[0];

		//get gameobjects
		modelGameObject = this.gameObject.transform.FindChild("Model").gameObject;
		anchorPointGameObject = this.gameObject.transform.FindChild("Anchor Point").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			initMousePosition = Input.mousePosition;
		}

		healthText.text = healthPoint + "/" + initHealthPoint;

		//get keyboard input
		//if rotate to the right	
		if (((Input.GetMouseButtonUp(0) && Input.mousePosition.x - initMousePosition.x > 10f && Mathf.Abs(Input.mousePosition.y - initMousePosition.y) < Mathf.Abs(Input.mousePosition.x - initMousePosition.x)) || Input.GetKey(KeyCode.RightArrow)) && !isAnimating)
		{
			//execute action
			//StartCoroutine(ActionManager.instance.ExecuteAction());

			ActionManager.instance.firstCurrentAction = ActionManager.instance.currentActions[0];

			StartCoroutine(Move(2));
		}
		else
		//if rotate to the front	
		if ((Input.GetMouseButtonUp(0) && Input.mousePosition.y - initMousePosition.y > 10f && Mathf.Abs(Input.mousePosition.y - initMousePosition.y) > Mathf.Abs(Input.mousePosition.x - initMousePosition.x)) || Input.GetKey(KeyCode.UpArrow) && !isAnimating)
		{
			//execute action
			//StartCoroutine(ActionManager.instance.ExecuteAction());

			ActionManager.instance.firstCurrentAction = ActionManager.instance.currentActions[0];

			StartCoroutine(Move(1));
		}
		else
		//if rotate to the back	
		if ((Input.GetMouseButtonUp(0) && Input.mousePosition.y - initMousePosition.y < -10f && Mathf.Abs(Input.mousePosition.y - initMousePosition.y) > Mathf.Abs(Input.mousePosition.x - initMousePosition.x)) || Input.GetKey(KeyCode.DownArrow) && !isAnimating)
		{
			//execute action
			//StartCoroutine(ActionManager.instance.ExecuteAction());

			ActionManager.instance.firstCurrentAction = ActionManager.instance.currentActions[0];

			StartCoroutine(Move(3));
		}
		else
		//if rotate to the keft	
		if ((Input.GetMouseButtonUp(0) && Input.mousePosition.x - initMousePosition.x < -10f && Mathf.Abs(Input.mousePosition.y - initMousePosition.y) < Mathf.Abs(Input.mousePosition.x - initMousePosition.x)) || Input.GetKey(KeyCode.LeftArrow) && !isAnimating)
		{
			//execute action
			//StartCoroutine(ActionManager.instance.ExecuteAction());

			ActionManager.instance.firstCurrentAction = ActionManager.instance.currentActions[0];

			StartCoroutine(Move(4));
		}
	}

	// Rotate the die
	public IEnumerator RotateDie (int direction, float duration)
	{
		isAnimating = true;

		//if rotate to the front
		if (direction == 1)
		{
			dieTweener = HOTween.To
			(
				anchorPointGameObject.transform, 
				duration, 
				new TweenParms()
			        .Prop
					(
						"rotation", 
						new Vector3
						(
							90f,
							0f,
							0f
						),
						true	
					) 
			        .Ease(EaseType.EaseInBack)
					.Delay(0f)
			);
		}
		else
		//if rotate to the right
		if (direction == 2)
		{
			dieTweener = HOTween.To
			(
				anchorPointGameObject.transform, 
				duration, 
				new TweenParms()
			        .Prop
					(
						"rotation", 
						new Vector3
						(
							0f,
							0f,
							-90f
						),
						true	
					) 
			        .Ease(EaseType.EaseInBack)
					.Delay(0f)
			);
		}
		else
		//if rotate to the back
		if (direction == 3)
		{
			dieTweener = HOTween.To
			(
				anchorPointGameObject.transform, 
				duration, 
				new TweenParms()
			        .Prop
					(
						"rotation", 
						new Vector3
						(
							-90f,
							0f,
							0f
						),
						true	
					) 
			        .Ease(EaseType.EaseInBack)
					.Delay(0f)
			);
		}
		else
		//if rotate to the left
		if (direction == 4)
		{
			dieTweener = HOTween.To
			(
				anchorPointGameObject.transform, 
				duration, 
				new TweenParms()
			        .Prop
					(
						"rotation", 
						new Vector3
						(
							0f,
							0f,
							90f
						),
						true	
					) 
			        .Ease(EaseType.EaseInBack)
					.Delay(0f)
			);
		}

		yield return new WaitForSeconds(duration);
	}

	// Move the die.
	// front is 1
	// right is 2
	// back is 3
	// left is 4
	public IEnumerator Move (int direction)
	{
		//store to temp variables
		int tempTop = elementOnSide[0];
		int tempFront = elementOnSide[1];
		int tempRight = elementOnSide[4];

		//move an action ahead
		StartCoroutine(ActionManager.instance.MoveAnActionAhead());

		//move to the front
		if (direction == 1)
		{
			elementOnSide[0] = tempFront;
			elementOnSide[2] = tempFront;
			elementOnSide[1] = tempTop;
			elementOnSide[3] = tempTop;
			elementOnSide[4] = tempRight;
			elementOnSide[5] = tempRight;

			anchorPointGameObject.transform.position += new Vector3(0f, -0.5f, 0.5f);
		}
		else
		//move to the right
		if (direction == 2)
		{
			elementOnSide[0] = tempRight;
			elementOnSide[2] = tempRight;
			elementOnSide[1] = tempFront;
			elementOnSide[3] = tempFront;
			elementOnSide[4] = tempTop;
			elementOnSide[5] = tempTop;

			anchorPointGameObject.transform.position += new Vector3(0.5f, -0.5f, 0f);
		}
		else
		//move to the back
		if (direction == 3)
		{
			elementOnSide[0] = tempFront;
			elementOnSide[2] = tempFront;
			elementOnSide[1] = tempTop;
			elementOnSide[3] = tempTop;
			elementOnSide[4] = tempRight;
			elementOnSide[5] = tempRight;
			
			anchorPointGameObject.transform.position += new Vector3(0f, -0.5f, -0.5f);
		}
		else
		//move to the left
		if (direction == 4)
		{
			elementOnSide[0] = tempRight;
			elementOnSide[2] = tempRight;
			elementOnSide[1] = tempFront;
			elementOnSide[3] = tempFront;
			elementOnSide[4] = tempTop;
			elementOnSide[5] = tempTop;

			anchorPointGameObject.transform.position += new Vector3(-0.5f, -0.5f, 0f);
		}

		//make die model as a child of anchor point
		modelGameObject.transform.parent = anchorPointGameObject.transform;

		//rotate the die
		yield return RotateDie(direction, 1f);

		//unparent die model from anchor point
		modelGameObject.transform.parent = anchorPointGameObject.transform.parent;

		//set anchor point position back to the middle of the die
		anchorPointGameObject.transform.position = modelGameObject.transform.position;

		//set back anchor point rotation to zero
		anchorPointGameObject.transform.eulerAngles = Vector3.zero;

		//assign elementID as the first index of elementOnSide
		elementID = elementOnSide[0];

		//execute action
		StartCoroutine(ActionManager.instance.ExecuteAction());

		//check whether there is a sword or not
		ActionManager.instance.CheckSword();
	}
}
