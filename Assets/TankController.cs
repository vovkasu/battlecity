using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TankController : MonoBehaviour
{
    public SpriteRenderer View;
    public List<DirectionParams> Directions;
    public DirectionParams LastDirection;

    public float Speed;

    public Bullet CurrentBullet;
    public Bullet BulletPrefab;

    public event EventHandler OnExplosion;

    // Use this for initialization
    void Start ()
    {
        LastDirection = Directions.FirstOrDefault(_ => _.DirectionName == "Up");
    }
	
	// Update is called once per frame
	void Update () {
	    DirectionParams currentDirection = null;

	    if (Input.GetButtonDown("Fire1") && CurrentBullet == null)
	    {
	        CurrentBullet = Fire();
	    }

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
	        LastDirection = currentDirection;
	        if (View.sprite != currentDirection.Sprite)
	        {
	            View.sprite = currentDirection.Sprite;
	        }
            transform.Translate(currentDirection.MoveDirection* Speed*Time.deltaTime);
	    }
    }

    private Bullet Fire()
    {
        var bullet = Instantiate(BulletPrefab, transform.parent);
        bullet.Fire(LastDirection, 1);
        return bullet;
    }

    public void FinishExplosionAnimation()
    {
        Destroy(gameObject);
        if (OnExplosion != null)
        {
            OnExplosion(this, null);
        }
    }
}
