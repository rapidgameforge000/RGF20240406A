using Assets.Script.Collision;
using Assets.Script.Stage;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Script.Enemy
{
    public class KameEnemy : MonoBehaviour, IEnemy
    {
        private RectTransform mRectTransform;
        private Vector2 mVelocity = Vector2.zero;
        private ICollisionChecker mCollisionChecker;

        private const float MOVE_SPEED_X = -15.5f;
        private const float GRAVITY = -0.89f;


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
            localPosition.x = mRectTransform.localPosition.x + mVelocity.x;

            // d—Í
            mVelocity.y += GRAVITY * deltaTime;
            localPosition.y = mRectTransform.localPosition.y + mVelocity.y;

            mRectTransform.localPosition = localPosition;

            // ‰Ÿ‚µ‡‚¢”»’è
            mCollisionChecker.CollideObstacle(this);
        }

        public Rect GetRect()
        {
            return mRectTransform.rect;
        }

        public void SetPositionVelocity(Vector2 position, Vector2 velocity)
        {
            mRectTransform.position = position;
            mVelocity = velocity;
        }

        public Vector2 GetVelocity() => this.mVelocity;
    }
}
