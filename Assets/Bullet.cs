using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public Vector2 MoveDirection;
    public SpriteRenderer View;
    public Animator ExplosionAnimation;
    public int Power;
    public bool NeedMove;

    public void Fire(DirectionParams direction, int power)
    {
        NeedMove = true;
        MoveDirection = direction.MoveDirection;
        View.transform.eulerAngles = new Vector3(0, 0, direction.BulletZRotation);
        transform.position = direction.BulletPosition.position;
        Power = power;
    }

    void Update ()
    {
        if (NeedMove)
        {
            transform.Translate(MoveDirection*Time.deltaTime*Speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D", other.gameObject);
        ExplosionAnimation.enabled = true;
        NeedMove = false;
    }

    public void FinishExplosionAnimation()
    {
        Destroy(gameObject);
    }
}
