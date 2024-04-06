using Assets.Script.Collision;
using UnityEngine;

namespace Assets.Script.Enemy
{
    public class KameEnemy : MonoBehaviour, IEnemy
    {
        private RectTransform mRectTransform;
        private Vector2 mVelocity = Vector2.zero;
        private ICollisionChecker mCollisionChecker;

        private const float MOVE_SPEED_X = -6000f;
        private const float GRAVITY = -88.9f;


        public void Initialize(ICollisionChecker checker)
        {
            mRectTransform = GetComponent<RectTransform>();
            mCollisionChecker = checker;
        }

        public void Process(float deltaTime)
        {
            var localPosition = mRectTransform.localPosition;

            // ‰¡ˆÚ“®
            mVelocity.x = MOVE_SPEED_X * deltaTime;
            localPosition.x = mRectTransform.localPosition.x + mVelocity.x * deltaTime;

            // d—Í
            mVelocity.y += GRAVITY * deltaTime;
            localPosition.y = mRectTransform.localPosition.y + mVelocity.y * deltaTime;

            mRectTransform.localPosition = localPosition;

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
    }
}
