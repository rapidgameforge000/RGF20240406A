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
using static Assets.Script.Collision.ICollisionChecker;

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

            string[] stageNameList = {
                "Stage/stage_01",
                "Stage/stage_kame",
                "Stage/stage_goal",
            };
            for (int i  = 0; i < stageNameList.Length; i++)
            {
                var stagePrefab = UnityEngine.Resources.Load<UnityEngine.GameObject>(stageNameList[i]);
                var stage = UnityEngine.Object.Instantiate(stagePrefab, this.root, false);

                var position = stage.transform.position;
                position.x += i * 1920;
                stage.transform.position = position;

                this.mainCharacter = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<MainCharacter.MainCharacter>("MainCharacter/MainCharacter"), this.root, false);
                obstacleList.AddRange(stage.GetComponentsInChildren<ObstacleObject>());
                enemyList.AddRange(stage.GetComponentsInChildren<IEnemy>());
            }


            mainCharacter.Initialize(this);
            foreach (var enemy in enemyList)
            {
                enemy.Initialize(this);
            }
        }

        internal void Process(float deltaTime)
        {
            this.deltaTime = deltaTime;


            this.mainCharacter.Process(deltaTime);

            foreach (var enemy in this.enemyList)
            {
                enemy.Process(deltaTime);
            }

            var rootPosition = this.root.position;
            rootPosition.x = -this.mainCharacter.transform.localPosition.x + 1920 / 2;
            this.root.position = rootPosition;
        }

        public CollideObstacleResult CollideObstacle(ICharacter character)
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
                if (character.GetVelocity().y < 0 && obstacle.GetRect().yMax < oldRect.yMin && nowRect.yMin <= obstacle.GetRect().yMax)
                {
                    // 着地
                    var newPosition = nowRect.center;
                    newPosition.y += obstacle.GetRect().yMax - nowRect.yMin + 0.01f;
                    var newVelocity = character.GetVelocity();
                    newVelocity.y = 0;
                    character.SetPositionVelocity(newPosition, newVelocity);
                    return CollideObstacleResult.TopToBottom;
                } else
                {
                    // 衝突
                    if (0 < character.GetVelocity().x && oldRect.xMax < obstacle.GetRect().xMin && obstacle.GetRect().xMin <= nowRect.xMax)
                    {
                        // 左から衝突
                        var newPosition = nowRect.center;
                        newPosition.x += obstacle.GetRect().xMin - nowRect.xMax - 0.01f;
                        var newVelocity = character.GetVelocity();
                        newVelocity.x = 0;
                        character.SetPositionVelocity(newPosition, newVelocity);
                        return CollideObstacleResult.LeftToRight;
                    }
                    else if (character.GetVelocity().x < 0 && obstacle.GetRect().xMax < oldRect.xMin && nowRect.xMin <= obstacle.GetRect().xMax)
                    {
                        // 右から衝突
                        var newPosition = nowRect.center;
                        newPosition.x += obstacle.GetRect().xMax - nowRect.xMin + 0.01f;
                        var newVelocity = character.GetVelocity();
                        newVelocity.x = 0;
                        character.SetPositionVelocity(newPosition, newVelocity);
                        return CollideObstacleResult.RightToLeft;
                    }
                    else if (0 < character.GetVelocity().y && oldRect.yMax < obstacle.GetRect().yMin && obstacle.GetRect().yMin <= nowRect.yMax)
                    {
                        // 下から衝突
                        var newPosition = nowRect.center;
                        newPosition.y += obstacle.GetRect().yMin - nowRect.yMax - 0.01f;
                        var newVelocity = character.GetVelocity();
                        newVelocity.y = 0;
                        character.SetPositionVelocity(newPosition, newVelocity);
                        return CollideObstacleResult.BottomToTop;
                    }
                }
            }
            return CollideObstacleResult.None;
        }

        public CollideEnemyResult CollideEnemy(MainCharacter.MainCharacter character)
        {
            foreach (var enemy in this.enemyList)
            {
                if (!ICollision.IsCollide(character, enemy))
                {
                    continue;
                }

                var nowRect = character.GetRect();
                var oldRect = nowRect;
                oldRect.position -= (character.GetVelocity() * this.deltaTime);
                if (enemy.IsKillable() && character.GetVelocity().y < 0 && enemy.GetRect().yMax < oldRect.yMin && nowRect.yMin <= enemy.GetRect().yMax)
                {
                    // 踏んで倒した
                    enemy.Kill();
                    this.obstacleList.Remove(enemy);
                    return CollideEnemyResult.Attack;
                }
                else
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
                    //return CollideEnemyResult.Hit;
                    return CollideEnemyResult.None;
                }
            }
            return CollideEnemyResult.None;
        }
    }
}
