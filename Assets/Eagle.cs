using System;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public SpriteRenderer View;

    public LayerMask ExploderMask;

    public Sprite DieView;
    public event EventHandler OnDie;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsInLayerMask(other.gameObject.layer, ExploderMask.value)) return;
        View.sprite = DieView;
        if (OnDie != null)
        {
            OnDie(this, null);
        }
    }

    public static bool IsInLayerMask(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
}
