using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "TankModel", menuName = "BattleCity/TankModel", order = 2)]
    public class EnemyModel : ScriptableObject
    {
        public float Speed;
        public float BulletSpeed;

        public List<TankView> Views;
    }

    [Serializable]
    public class TankView
    {
        public string Name;
        public Sprite Sprite;
    }


}