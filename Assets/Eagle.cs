using System;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public SpriteRenderer View;

    public Sprite DieView;
    public event EventHandler OnDie;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Bullet")) return;
        View.sprite = DieView;
        if (OnDie != null)
        {
            OnDie(this, null);
        }
    }

}
