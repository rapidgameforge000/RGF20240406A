using Assets.Script.Collision;
using UnityEngine;

namespace Assets.Script.Enemy
{
    public class NoMoveEnemy : MonoBehaviour, IEnemy
    {
        private RectTransform mRectTransform;
        private Vector2 mVelocity = Vector2.zero;
        private ICollisionChecker mCollisionChecker;


        public void Initialize(ICollisionChecker checker)
        {
            mRectTransform = GetComponent<RectTransform>();
            mCollisionChecker = checker;
        }

        public void Process(float deltaTime)
        {
            // ‰Ÿ‚µ‡‚¢”»’è
            mCollisionChecker.CollideObstacle(this);
        }

        public Rect GetRect()
        {
            var rect = mRectTransform.rect;
            rect.center += (UnityEngine.Vector2)mRectTransform.position;
            return rect;
        }

        public void SetPositionVelocity(Vector2 position, Vector2 velocity)
        {
            mRectTransform.position = position;
            mVelocity = velocity;
        }

        public Vector2 GetVelocity() => this.mVelocity;


        public void Kill()
        {
            GameObject.Destroy(this.gameObject);
        }

        public bool IsKillable()
        {
            return false;
        }
    }
}
