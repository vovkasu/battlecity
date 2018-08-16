using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : TankController
{
    public float FirePeriod = 1f;

    public List<ActionsStrategyBase> Strategies = new List<ActionsStrategyBase>();
    public int StrategyIndex;

    private DirectionParams _currentDirection;
    private bool _inited;
    private TankController _player;
    private Eagle _eagle;
    public bool IsStuck;
    private PositionsHistory _positionsHistory;

    public void Init(EnemyModel enemyModel)
    {
        View.sprite = enemyModel.Views.Find(_ => _.Name == "Up").Sprite;
        Speed = enemyModel.Speed;
        BulletSpeed = enemyModel.BulletSpeed;

        foreach (var directionParams in Directions)
        {
            directionParams.Sprite = enemyModel.Views.Find(_ => _.Name == directionParams.DirectionName).Sprite;
        }

        InitCurrentStrategy();
        StartCoroutine(TryFire(FirePeriod));
        DelayChangeStrategy();
        _currentDirection = SelectDirection();
        _inited = true;

        _positionsHistory = new PositionsHistory(7);
        _positionsHistory.AddPosition(transform.position);
        StartCoroutine(UpdateHistory(0.15f));
    }

    private IEnumerator UpdateHistory(float period)
    {
        while (true)
        {
            yield return new WaitForSeconds(period);
            _positionsHistory.AddPosition(transform.position);

            if (_positionsHistory.Count() > 5 && _positionsHistory.GetMaxDistance(transform.position) < 0.2f)
            {
                IsStuck = true;
            }
        }
    }

    public void SetPlayerAndEagle(TankController player, Eagle eagle)
    {
        _player = player;
        _eagle = eagle;
    }

    private void InitCurrentStrategy()
    {
        GetCurrentStrategy().Init(_player, _eagle);
    }

    private void DelayChangeStrategy()
    {
        StartCoroutine(ChangeStrategy(GetCurrentStrategy().StrategyDuration));
    }

    private IEnumerator ChangeStrategy(float strategyDuration)
    {
        yield return new WaitForSeconds(strategyDuration);
        StrategyIndex++;
        InitCurrentStrategy();
        DelayChangeStrategy();
    }

    private ActionsStrategyBase GetCurrentStrategy()
    {
        return Strategies[StrategyIndex%Strategies.Count];
    }

    private DirectionParams SelectDirection()
    {
        IsStuck = false;
        var possibleDirections = new List<DirectionParams>(Directions);
        possibleDirections.Remove(_currentDirection);
        return GetCurrentStrategy().SelectDirection(possibleDirections, transform);
    }

    private IEnumerator TryFire(float firePeriod)
    {
        while (true)
        {
            yield return new WaitForSeconds(firePeriod);
            CurrentBullet = Fire();
        }
    }

    public override void Update()
    {
        if(!_inited) return;

        if (IsStuck)
        {
            _currentDirection = SelectDirection();
        }

        if (_currentDirection != null)
        {
            LastDirection = _currentDirection;
            if (View.sprite != _currentDirection.Sprite)
            {
                View.sprite = _currentDirection.Sprite;
            }
            transform.Translate(_currentDirection.MoveDirection * Speed * Time.deltaTime);
        }
    }
}