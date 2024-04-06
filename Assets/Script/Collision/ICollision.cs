using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Collision
{
    internal interface ICollision
    {
        UnityEngine.Rect GetRect();

        internal static bool IsCollide(ICollision left, ICollision right)
        {
            var leftRect = left.GetRect();
            var rightRect = right.GetRect();

            return leftRect.xMin <= rightRect.xMax && rightRect.xMin <= leftRect.yMax &&
                leftRect.yMin <= rightRect.yMax && rightRect.yMin <= leftRect.yMax;
        }
    }
}
