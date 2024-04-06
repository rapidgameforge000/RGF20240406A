using Assets.Script.Character;
using Assets.Script.Collision;
using Assets.Script.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Stage
{
    internal class StageManager
    {
        List<IEnemy> enemyList = new List<IEnemy>();
        List<ICollision> obstacleList = new List<ICollision>();

        internal void Process(float deltaTime)
        {
            foreach (var enemy in this.enemyList)
            {
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

            }
            return UnityEngine.Vector2.zero;
        }

    }
}
