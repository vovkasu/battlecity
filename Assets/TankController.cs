using System;
using System.Collections.Generic;
using UnityEngine;


public class TankController : MonoBehaviour
{

    public SpriteRenderer View;

    public List<DirectionParams> Directions;

    public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    DirectionParams currentDirection = null;

        foreach (var directionParam in Directions)
	    {
	        var axisValue = Input.GetAxis(directionParam.Axis.ToString());
	        if (Mathf.Abs(axisValue) > Mathf.Abs(directionParam.AxisValue) &&
	            Math.Sign(axisValue) == Math.Sign(directionParam.AxisValue))
	        {
	            currentDirection = directionParam;
                break;
	        }
	    }

	    if (currentDirection != null)
	    {
	        if (View.sprite != currentDirection.Sprite)
	        {
	            View.sprite = currentDirection.Sprite;
	        }
            transform.Translate(currentDirection.MoveDirection* Speed*Time.deltaTime);
	    }
    }
}
