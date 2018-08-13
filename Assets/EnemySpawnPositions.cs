using UnityEngine;

public class EnemySpawnPositions : MonoBehaviour
{
    public BoxCollider2D BoxCollider2D;
    public bool Empty = true;
    public bool IsEmpty()
    {
        return Empty;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Empty = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Empty = true;
    }
}