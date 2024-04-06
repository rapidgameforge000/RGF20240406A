using Assets.Script.Character;
using Assets.Script.Collision;
using UnityEngine;

namespace Assets.Script.MainCharacter
{
    public class MainCharacter : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private int MOVE_VELOCITY = 50;
        [SerializeField]
        private int JUMP_VELOCITY = 80;
        [SerializeField]
        private int BRAKE_VELOCITY = 5;
        [SerializeField]
        private int MAX_VELOCITY = 50;


        private RectTransform rectTransform = null;
        private ICollisionChecker collisionChecker = null;

        [SerializeField]
        private Vector2 velocity = Vector2.zero;
        private int jumpCount = 0;
        private int maxJumpCount = 25;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        internal void Initialize(ICollisionChecker collisionChecker)
        {
            this.collisionChecker = collisionChecker;
        }

        public void Process(float deltaTime)
        {
            Move(deltaTime);
            CollideEnemy(deltaTime);
            this.collisionChecker.CollideObstacle(this);
        }

        private void Move(float deltaTime)
        {
            // キー入力.
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.velocity.x += MOVE_VELOCITY * deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.velocity.x -= MOVE_VELOCITY * deltaTime;
            }
            if (Input.GetKey(KeyCode.Space) && jumpCount < maxJumpCount)
            {
                this.velocity.y += JUMP_VELOCITY * deltaTime;
                ++this.jumpCount;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                this.jumpCount = 0;
            }

            // ブレーキ.
            if (System.Math.Abs(this.velocity.x) > (BRAKE_VELOCITY * deltaTime))
            {
                this.velocity.x -= BRAKE_VELOCITY * deltaTime * System.Math.Sign(this.velocity.x);
            }
            else
            {
                this.velocity.x = 0;
            }

            // 重力
            this.velocity.y -= JUMP_VELOCITY * deltaTime;

            if (this.velocity.sqrMagnitude > (MAX_VELOCITY * MAX_VELOCITY))
            {
                this.velocity = this.velocity.normalized * MAX_VELOCITY;
            }

            this.rectTransform.localPosition += new Vector3(this.velocity.x, this.velocity.y) * deltaTime;
        }

        private void CollideEnemy(float deltaTime)
        {
            var result = this.collisionChecker.CollideEnemy(this);

            switch (result)
            {
                case ICollisionChecker.CollideEnemyResult.None:
                    break;
                case ICollisionChecker.CollideEnemyResult.Hit:
                    break;
                case ICollisionChecker.CollideEnemyResult.Attack:
                    this.velocity.y += JUMP_VELOCITY * deltaTime * 5;
                    break;
                default:
                    throw new System.Exception();
            }
        }

        public Rect GetRect()
        {
            var rect = this.rectTransform.rect;
            rect.center += (Vector2)this.rectTransform.position;
            return rect;
        }

        public void SetPositionVelocity(Vector2 position, Vector2 velocity)
        {
            this.rectTransform.position = position;
            this.velocity = velocity;
        }

        public Vector2 GetVelocity() => this.velocity;
    }
}
