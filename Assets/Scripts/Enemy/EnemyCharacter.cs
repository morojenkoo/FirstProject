using System.Collections;
using FirstProject.Enemy;
using FirstProject.States;
using UnityEngine;
namespace FirstProject {
    [RequireComponent(typeof(EnemyDirectionController), typeof(EnemyAIController))]
    public class EnemyCharacter : BaseCharacter
    {
        [SerializeField] 
        private float _runAwayHealth = 30f;

        public static int СurrentEnemyCount = 0;
        public float GetRunAwayHealth()
        {
            return _runAwayHealth;
        }

        public override void Death()
        {
            base.Death();
            StartCoroutine(DeathCoroutineWithSpawnDelay());
            
        }

        private IEnumerator DeathCoroutineWithSpawnDelay()
        {
            yield return StartCoroutine(base.DeathCoroutine());
            СurrentEnemyCount--;
        }
    }
}