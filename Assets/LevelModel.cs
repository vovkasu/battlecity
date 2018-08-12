using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "BattleCity/Level", order = 1)]
public class LevelModel : ScriptableObject
{
    public List<EnemyModel> Enemies = new List<EnemyModel>();
}