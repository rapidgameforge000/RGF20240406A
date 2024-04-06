using Assets.Script.Collision;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Script.Enemy
{
    public class KameEnemy : MonoBehaviour, IEnemy
    {
        private RectTransform mRectTransform;
        private Vector2 mVelocity = Vector2.zero;

        private const float MOVE_SPEED = -5.5f;

        void Awake()
        {
            mRectTransform = GetComponent<RectTransform>();
        }

        public void Process(float deltaTime)
        {
            var localPosition = mRectTransform.localPosition;
            localPosition.x = mRectTransform.localPosition.x + MOVE_SPEED * deltaTime;
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
