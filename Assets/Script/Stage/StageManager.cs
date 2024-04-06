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
    internal class StageManager
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
            enemyList.AddRange(stage.GetComponentsInChildren<KameEnemy>());
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

        void CollideObstacle(ICharacter character)
        {
            foreach (var obstacle in this.obstacleList)
            {
                if (!ICollision.IsCollide(character, obstacle))
                {
                    continue;
                }

                var nowBottom = character.GetRect().yMin;
                var oldBottom = nowBottom - character.GetVelocity().y * this.deltaTime;
                if (obstacle.GetRect().yMax <= oldBottom)
                {
                    // 着地
                    var newPosition = character.GetRect().position;
                    newPosition.y += obstacle.GetRect().yMax - nowBottom;
                    var newVelocity = character.GetVelocity();
                    newVelocity.y = 0;
                    character.SetPositionVelocity(newPosition, newVelocity);
                } else
                {

                }
            }
        }

    }
}
