using Assets.Script.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Enemy
{
    internal interface IEnemy : ICollision
    {
        void Process(float deltaTime);
    }
}
