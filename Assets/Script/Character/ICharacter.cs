using Assets.Script.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Character
{
    internal interface ICharacter : ICollision
    {
        UnityEngine.Vector2 GetVelocity();
        void Process(float deltaTime);
        void SetPositionVelocity(UnityEngine.Vector2 position, UnityEngine.Vector2 velocity);
    }
}
