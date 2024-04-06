using Assets.Script.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Stage
{
    internal class StageManager
    {
        List<ICollision> enemyList = new List<ICollision>();
        List<ICollision> obstacleList = new List<ICollision>();

        internal void Process(float deltaTime)
        {
            foreach (var enemy in this.enemyList)
            {
            }
        }

        UnityEngine.Vector2 CheckPosition(ICollision collision)
        {
            collision.getp
        }

    }
}
