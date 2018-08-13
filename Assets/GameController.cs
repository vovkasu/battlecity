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
    public EnemyFactory EnemyFactory;

    public LevelModel Level;

    private void Start()
    {
        var tankController = Instantiate(TankControllerPrefab, GameRooTransform);
        tankController.transform.position = PlayerSpawnPosition.position;

        Eagle.OnDie -= GameOver;
        tankController.OnExplosion -= GameOver;

        Eagle.OnDie += GameOver;
        tankController.OnExplosion += GameOver;

        EnemyFactory.Run(Level);
    }

    private void GameOver(object sender, EventArgs eventArgs)
    {
        EnemyFactory.Stop();
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
