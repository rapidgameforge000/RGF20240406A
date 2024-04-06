using Assets.Script.Collision;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Script.Enemy
{
    public class KaeruEnemy : MonoBehaviour, IEnemy
    {
        private RectTransform mRectTransform;
        private Vector2 mVelocity = Vector2.zero;
        private ICollisionChecker mCollisionChecker;

        private float GRAVITY = 150.0f;
        private readonly UnityEngine.Vector2 JUMP_VELOCITY = new UnityEngine.Vector2(-150, 300);

        public void Initialize(ICollisionChecker checker) {
            mCollisionChecker = checker;
        }

        void Awake()
        {
            mRectTransform = GetComponent<RectTransform>();
        }

        public void Process(float deltaTime)
        {
            UnityEngine.Vector2 position = transform.localPosition;
            position += mVelocity * deltaTime;
            transform.localPosition = position;
            mVelocity.y -= GRAVITY * deltaTime;

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
            mVelocity = velocity;
            float diff = mRectTransform.position.y - position.y;
            if ( diff < 0 && velocity.y == 0) {
                mVelocity = JUMP_VELOCITY;
            }
            mRectTransform.position = position;
        }

        public Vector2 GetVelocity() => this.mVelocity;


        public void Kill()
        {
            GameObject.Destroy(this.gameObject);
        }

        public bool IsKillable()
        {
            return true;
        }
    }
}
