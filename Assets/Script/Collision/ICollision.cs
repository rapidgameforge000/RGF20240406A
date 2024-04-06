using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Collision
{
    internal interface ICollision
    {
        UnityEngine.Vector2 GetPosition();
        UnityEngine.Rect GetRect();
    }
}
