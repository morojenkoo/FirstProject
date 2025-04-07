using FirstProject.Enemy;
using FirstProject.States;
using UnityEngine;
namespace FirstProject {
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAIController))]
    public class EnemyCharacter : BaseCharacter
    {
        [SerializeField] 
        private float _runAwayHealth = 30f;
        public float GetRunAwayHealth()
        {
            return _runAwayHealth;
        }
    }
}