using Assets.Script.Collision;
using UnityEngine;

namespace Assets.Script.Obstacle
{
    
    internal class ObstacleObject : UnityEngine.MonoBehaviour, ICollision
    {
        static internal ObstacleObject load( UnityEngine.Transform transform ) {
            ObstacleObject prefab = UnityEngine.Resources.Load<ObstacleObject>("Obstacle/obstacle");
            ObstacleObject instance = Instantiate(prefab, transform);
            return instance;
        }
        
        internal void initialize( )
        {
           
        }

        internal void process()
        {
            
        }

        public UnityEngine.Rect GetRect() {
            var rect = ((UnityEngine.RectTransform)this.transform).rect;
            rect.center += (UnityEngine.Vector2)this.transform.position;
            return rect;
        }

    }
}
