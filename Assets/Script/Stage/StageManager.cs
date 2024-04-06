using Assets.Script.Character;
using Assets.Script.Collision;
using Assets.Script.Enemy;
using Assets.Script.Obstacle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Stage
{
    internal class StageManager : ICollisionChecker
    {
        private UnityEngine.Transform root;
        private float deltaTime;

        private MainCharacter.MainCharacter mainCharacter;
        List<IEnemy> enemyList = new List<IEnemy>();
        List<ICollision> obstacleList = new List<ICollision>();

        internal void Initialize(UnityEngine.Transform root)
        {
            this.root = root;

            var stagePrefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("Stage/stage_01");
            var stage = UnityEngine.Object.Instantiate(stagePrefab, this.root, false);

            this.mainCharacter = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<MainCharacter.MainCharacter>("MainCharacter/MainCharacter"), this.root, false);
            obstacleList.AddRange(stage.GetComponentsInChildren<ObstacleObject>());
            enemyList.AddRange(stage.GetComponentsInChildren<IEnemy>());
        }

        internal void Process(float deltaTime)
        {
            this.deltaTime = deltaTime;


            this.mainCharacter.Process(deltaTime);

            foreach (var enemy in this.enemyList)
            {
                enemy.Process(deltaTime);
            }
        }

        public void CollideObstacle(ICharacter character)
        {
            foreach (var obstacle in this.obstacleList)
            {
                if (!ICollision.IsCollide(character, obstacle))
                {
                    continue;
                }

                var nowRect = character.GetRect();
                var oldRect = nowRect;
                oldRect.position -= (character.GetVelocity() * this.deltaTime);
                if (obstacle.GetRect().yMax <= oldRect.yMin)
                {
                    // 着地
                    var newPosition = nowRect.position;
                    newPosition.y += obstacle.GetRect().yMax - nowRect.yMin;
                    var newVelocity = character.GetVelocity();
                    newVelocity.y = 0;
                    character.SetPositionVelocity(newPosition, newVelocity);
                } else
                {
                    // 衝突

                }
            }
        }

    }
}
