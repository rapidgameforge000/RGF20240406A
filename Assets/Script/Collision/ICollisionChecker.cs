using Assets.Script.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Collision
{
    public interface ICollisionChecker
    {
        public enum CollideResult {
            None,   // 未衝突
            Hit,    // 衝突(ダメージを食らう)
            Attack, // 踏んだ
        }

        void CollideObstacle(ICharacter character);
        CollideResult CollideEnemy(MainCharacter.MainCharacter character);
    }
}
