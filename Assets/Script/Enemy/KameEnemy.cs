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

        private float mYSpd = 0f;

        private const float MOVE_SPEED_X = -15.5f;
        private const float GRAVITY = -1.89f;


        public void Initialize(ICollisionChecker checker)
        {
            mRectTransform = GetComponent<RectTransform>();
            mCollisionChecker = checker;
        }

        public void Process(float deltaTime)
        {
            var localPosition = mRectTransform.localPosition;

            // ‰¡ˆÚ“®
            localPosition.x = mRectTransform.localPosition.x + MOVE_SPEED_X * deltaTime;

            // d—Í
            mYSpd += GRAVITY;
            localPosition.y = mRectTransform.localPosition.y + mYSpd  * deltaTime;
            
            mRectTransform.localPosition = localPosition;
        }

        public Rect GetRect()
        {
            return mRectTransform.rect;
        }

        public void SetPositionVelocity(Vector2 position, Vector2 velocity)
        {
            mRectTransform.position = position;
            mVelocity += velocity;
        }

        public Vector2 GetVelocity() => this.mVelocity;
    }
}
