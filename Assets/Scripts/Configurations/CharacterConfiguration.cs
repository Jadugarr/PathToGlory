using System;
using UnityEngine;

namespace SemoGames.PTG.Configurations
{
    [Serializable]
    [CreateAssetMenu(fileName ="CharacterConfiguration", menuName = "Configurations/CharacterConfiguration")]
    public class CharacterConfiguration : ScriptableObject
    {
        [SerializeField]
        public GameObject EnemyTemplate;

        [SerializeField]
        public GameObject PlayerTemplate;
    }
}
