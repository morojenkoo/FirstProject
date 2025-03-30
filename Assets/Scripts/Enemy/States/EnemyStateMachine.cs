using System.Collections.Generic;
using FirstProject.FSM;

namespace FirstProject.States
{
    public class EnemyStateMachine: BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5f;
        public EnemyStateMachine(EnemyTarget target, NavMesher navMesher, EnemyDirectionController enemyDirectionController)
        {
            var idleState = new IdleState();
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            SetInitialState(idleState);
            AddState(state: idleState, transitions: new List<Transition>
                {
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance), 
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                }
            );
            AddState(state: findWayState, transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null), 
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                }
            );
            AddState(state: moveForwardState, transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null), 
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                }
            );
        }
    }
}