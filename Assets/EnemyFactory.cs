using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

public class EnemyFactory : MonoBehaviour
{
    public Enemy EnemyPrefab;
    public List<Enemy> Enemies = new List<Enemy>();
    public LevelModel Level;
    public List<EnemySpawnPositions> SpawnPositions;
    public Transform RootGameObject;
    public IPlayerProvider PlayerProvider;
    public IEagleProvider EagleProvider;

    private IEnumerator _enemySpawner;
    private Random _random;

    public void Run(LevelModel levelModel, IPlayerProvider playerProvider, IEagleProvider eagleProvider)
    {
        _random = new System.Random();
        Level = levelModel;
        PlayerProvider = playerProvider;
        EagleProvider = eagleProvider;
        _enemySpawner = SpawnEnemy(Level.Enemies, Level.Period);
        StartCoroutine(_enemySpawner);
    }

    public void Stop()
    {
        if (_enemySpawner != null)
        {
            StopCoroutine(_enemySpawner);
        }
    }

    private IEnumerator SpawnEnemy(List<EnemyModel> levelEnemies, float spawnPeriod)
    {
        List<EnemyModel> enemyModels  = new List<EnemyModel>(levelEnemies);
        while (enemyModels.Count>0)
        {
            yield return new WaitForSeconds(spawnPeriod);
            var enemyModel = enemyModels[0];
            var enemySpawnPositionses = SpawnPositions.Where(_=>_.IsEmpty()).ToList();
            if (enemySpawnPositionses.Count > 0)
            {
                var enemySpawnPosition = enemySpawnPositionses[_random.Next(0, enemySpawnPositionses.Count)];
                var enemy = Instantiate(EnemyPrefab, RootGameObject);
                enemy.Init(enemyModel);
                enemy.SetPlayerAndEagle(PlayerProvider.GetPlayer(), EagleProvider.GetEagle());
                enemy.transform.position = enemySpawnPosition.transform.position;
                Enemies.Add(enemy);
                enemy.OnExplosion += (sender, args) => { Enemies.Remove(enemy); };

                enemyModels.Remove(enemyModel);
            }
        }
    }
}