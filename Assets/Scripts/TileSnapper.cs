using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TileSnapper : MonoBehaviour 
{
	public float movementDistance;

	public bool moveRight;
	public bool moveLeft;
	public bool moveUp;
	public bool moveDown;
	public bool rotate;

    void Update() 
	{
		this.gameObject.name = "Tile";

		if (Application.isEditor && !Application.isPlaying) 
		{
			if (moveRight)
			{
				moveRight = false;

				this.gameObject.transform.position += new Vector3(movementDistance, 0f, 0f);
			}
			else
			if (moveLeft)
			{
				moveLeft = false;

				this.gameObject.transform.position -= new Vector3(movementDistance, 0f, 0f);
			}
			else
			if (moveUp)
			{
				moveUp = false;

				this.gameObject.transform.position += new Vector3(0f, 0f, movementDistance);
			}
			else
			if (moveDown)
			{
				moveDown = false;

				this.gameObject.transform.position -= new Vector3(0f, 0f, movementDistance);
			}
		}
    }
}
