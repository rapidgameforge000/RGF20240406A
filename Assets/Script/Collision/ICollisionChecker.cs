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
        public enum CollideObstacleResult {
            None,   // 未衝突
            LeftToRight, // 左から右に進んで障害物の左端にぶつかった
            TopToBottom, // 落下して着地した
            RightToLeft,    // 右から左に進んで障害物の右端にぶつかった
            BottomToTop,    // 下から上に進んで障害物の天井にぶつかった
        }
        public enum CollideEnemyResult {
            None,   // 未衝突
            Hit,    // 衝突(ダメージを食らう)
            Attack, // 踏んだ
        }

        CollideObstacleResult CollideObstacle(ICharacter character);
        CollideEnemyResult CollideEnemy(MainCharacter.MainCharacter character);
    }
}
