﻿using Assets.Script.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Collision
{
    public interface ICollisionChecker
    {
        void CollideObstacle(ICharacter character);
    }
}
