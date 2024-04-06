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

        private float GRAVITY = 5f;
        private const int JUMP_TIMING = 2;
        private readonly UnityEngine.Vector2 JUMP_VELOCITY = new UnityEngine.Vector2(-30, 30);

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
            return mRectTransform.rect;
        }

        public void SetPositionVelocity(Vector2 position, Vector2 velocity)
        {
            mRectTransform.position = position;
            mVelocity = velocity;

            if (velocity.y == 0)
            {
                mVelocity += JUMP_VELOCITY;
            }
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
