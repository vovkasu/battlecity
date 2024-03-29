﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : MonoBehaviour
{
    public Tilemap Tilemap;

    public LayerMask ExploderMask;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!TankController.IsInLayerMask(other.gameObject.layer, ExploderMask)) return;

        Vector3 hitPosition = Vector3.zero;
        foreach (var hit in other.contacts)
        {
            hitPosition.x = hit.point.x + 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y + 0.01f * hit.normal.y;
            var cellPosition = Tilemap.WorldToCell(hitPosition);
            Debug.Log(cellPosition);
            Tilemap.SetTile(cellPosition, null);
        }

    }
}