using Assets.Script.Collision;

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
            return ((UnityEngine.RectTransform)this.transform).rect;
        }

    }
}
