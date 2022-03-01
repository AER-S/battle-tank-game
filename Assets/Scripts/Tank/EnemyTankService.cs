using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tank
{
    public class EnemyTankService : SingletonMB<EnemyTankService>, ITankService
    {
        [SerializeField] private ParticleSystem tankExplosion;
        [SerializeField] private Scriptable_Object.Tank.EnemyTankList tanks;
        [SerializeField] private Transform[] enemySpawningPoints;

        public ParticleSystem Explosion => tankExplosion;

        private List<EnemyTankController> _tankControllers = new List<EnemyTankController>();

        public List<EnemyTankController> Tanks => _tankControllers;

        private void Start()
        {
            for (int i = 0; i < tanks.List.Length; i++)
            {
                _tankControllers.Add((EnemyTankController) ((ITankService) this).CreateTank(tanks.List[i]));
            }
        }

        TankController ITankService.CreateTank(Scriptable_Object.Tank.Tank tank)
        {
            return new EnemyTankController(tank, GetRandomPosition());
        }

        public void Destroy(TankController controller)
        {
            _tankControllers.Remove((EnemyTankController) controller);
            StartCoroutine(((ITankService) this).KillTank(controller, tankExplosion));
        }

        private Vector3 GetRandomPosition()
        {
            return enemySpawningPoints[Random.Range(0, enemySpawningPoints.Length - 1)].position;
        }
        
        

        public void DestroyAll()
        {
            
        }
    }
}
