using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : TankController
{
    public float StuckCheckingPeriod = 0.2f;
    public float StuckCheckingPath = 0.4f;
    public float FirePeriod = 1f;

    public List<ActionsStrategyBase> Strategies = new List<ActionsStrategyBase>();
    public int StrategyIndex;

    private List<DirectionParams> _possibleDirections;
    private DirectionParams _currentDirection;
    private DateTime _selectDirectionAt;
    private Vector3 _selectDirectionPosition;
    private bool _checkOnStuck;
    private bool _inited;
    private TankController _player;
    private Eagle _eagle;

    public void Init(EnemyModel enemyModel)
    {
        View.sprite = enemyModel.Views.Find(_ => _.Name == "Up").Sprite;
        Speed = enemyModel.Speed;
        BulletSpeed = enemyModel.BulletSpeed;

        foreach (var directionParams in Directions)
        {
            directionParams.Sprite = enemyModel.Views.Find(_ => _.Name == directionParams.DirectionName).Sprite;
        }

        _possibleDirections = new List<DirectionParams>(Directions);

        InitCurrentStrategy();
        StartCoroutine(TryFire(FirePeriod));
        DelayChangeStrategy();
        _currentDirection = SelectDirection();
        _inited = true;
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
    }

    private ActionsStrategyBase GetCurrentStrategy()
    {
        return Strategies[StrategyIndex%Strategies.Count];
    }

    private DirectionParams SelectDirection()
    {
        _selectDirectionAt = DateTime.Now;
        _selectDirectionPosition = transform.position;
        _checkOnStuck = true;
        return GetCurrentStrategy().SelectDirection(_possibleDirections, transform);
    }

    private IEnumerator TryFire(float firePeriod)
    {
        yield return new WaitForSeconds(firePeriod);
        Fire();
    }

    public override void Update()
    {
        if(!_inited) return;

        if (IsStuck())
        {
            _possibleDirections.Remove(_currentDirection);
            if (_possibleDirections.Count == 0)
            {
                _possibleDirections = new List<DirectionParams>(Directions);
            }
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

    private bool IsStuck()
    {
        if (!_checkOnStuck) return false;

        if ((DateTime.Now - _selectDirectionAt).TotalSeconds > StuckCheckingPeriod)
        {
            if (Vector2.Distance(transform.position, _selectDirectionPosition) < StuckCheckingPath)
            {
                return true;
            }

            _possibleDirections = new List<DirectionParams>(Directions);
            _checkOnStuck = false;
        }
        return false;

    }
}