using UnityEngine;

namespace FirstProject
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }
        private readonly PlayerCharacter _player;
        private readonly Transform _agentTransform;
        private readonly Collider[] _colliders = new Collider[10];
        private readonly float _viewRadius;
        public EnemyTarget(Transform agent, PlayerCharacter player, float viewRadius)
        {
            _agentTransform = agent;
            _player = player;
            _viewRadius = viewRadius;
        }

        public void FindClosest()
        {
            float minDistance = float.MaxValue;
            var count = FindAllTargets(LayerUtils.PickUpsMask | LayerUtils.EnemyMask);
            //count+= FindAllTargets(LayerUtils.PickUpsMask | LayerUtils.PlayerMask);
            for (int i = 0; i < count; i++)
            {
                var go = _colliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;
                var dist = DistanceFromAgentTo(go);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    Closest = go;
                }
            }

            if (_player != null && DistanceFromAgentTo(_player.gameObject) < minDistance)
                Closest = _player.gameObject;
        }
        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
                return DistanceFromAgentTo(Closest);
            return 0;
        }

        private int FindAllTargets(int LayerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(
                _agentTransform.position, _viewRadius, _colliders, LayerMask);
            return size;
        }
        private float DistanceFromAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;
    }
}