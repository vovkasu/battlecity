using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IPlayerProvider, IEagleProvider
{
    public Transform PlayerSpawnPosition;
    public TankController TankControllerPrefab;
    public Eagle Eagle;
    public Transform GameRooTransform;
    public EnemyFactory EnemyFactory;

    public LevelModel Level;
    private TankController _tankController;

    private void Start()
    {
        _tankController = Instantiate(TankControllerPrefab, GameRooTransform);
        _tankController.transform.position = PlayerSpawnPosition.position;

        Eagle.OnDie -= GameOver;
        _tankController.OnExplosion -= GameOver;

        Eagle.OnDie += GameOver;
        _tankController.OnExplosion += GameOver;

        EnemyFactory.Run(Level, this, this);
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

    public TankController GetPlayer()
    {
        return _tankController;
    }

    public Eagle GetEagle()
    {
        return Eagle;
    }
}

public interface IEagleProvider
{
    Eagle GetEagle();
}

public interface IPlayerProvider
{
    TankController GetPlayer();
}
