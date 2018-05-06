using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{

    public SpriteRenderer View;

    public Sprite LeftDirection;
    public Sprite UpDirection;
    public Sprite RightDirection;
    public Sprite DownDirection;

    public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 direction = Vector2.zero;
	    Sprite sprite = null;
	    if (Input.GetAxis("Vertical") > 0.5)
	    {
            direction = Vector2.up;
	        sprite = UpDirection;
	    }
        else if (Input.GetAxis("Vertical") < -0.5)
	    {
            direction = Vector2.down;
            sprite = DownDirection;
        }
        else if (Input.GetAxis("Horizontal") > 0.5)
        {
            direction = Vector2.right;
            sprite = RightDirection;
        }
        else if (Input.GetAxis("Horizontal") < -0.5)
        {
            direction = Vector2.left;
            sprite = LeftDirection;
        }
	    if (sprite != null)
	    {
	        if (View.sprite != sprite)
	        {
	            View.sprite = sprite;
	        }
            transform.Translate(direction*Speed*Time.deltaTime);
	    }
    }
}
