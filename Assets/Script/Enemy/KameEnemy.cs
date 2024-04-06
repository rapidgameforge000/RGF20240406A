using Assets.Script.Collision;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Script.Enemy
{
    public class KameEnemy : MonoBehaviour, IEnemy
    {
        private RectTransform mRectTransform;
        private Vector2 mVelocity = Vector2.zero;

        private const float MOVE_SPEED = -0.5f;

        void Awake()
        {
            mRectTransform = GetComponent<RectTransform>();
        }

        public void Process(float deltaTime)
        {
            var posX = mRectTransform.localPosition.x + MOVE_SPEED * deltaTime;
            mRectTransform.localPosition.Set(posX, mRectTransform.localPosition.y, mRectTransform.localPosition.z);
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
