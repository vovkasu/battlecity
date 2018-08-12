using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Transform PlayerSpawnPosition;
    public TankController TankControllerPrefab;
    public Eagle Eagle;
    public Transform GameRooTransform;

    private void Start()
    {
        var tankController = Instantiate(TankControllerPrefab, GameRooTransform);
        tankController.transform.position = PlayerSpawnPosition.position;

        Eagle.OnDie -= GameOver;
        tankController.OnExplosion -= GameOver;

        Eagle.OnDie += GameOver;
        tankController.OnExplosion += GameOver;
    }

    private void GameOver(object sender, EventArgs eventArgs)
    {
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
