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
        [SerializeField]
        private int GRAVITY = 50;


        private RectTransform rectTransform = null;
        private ICollisionChecker collisionChecker = null;

        [SerializeField]
        private Vector2 velocity = Vector2.zero;
        private int jumpCount = 0;
        private int maxJumpCount = 25;

        [SerializeField]
        private bool isGround = false;

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
            CollideObstacle(deltaTime);
        }

        private bool canJump()
        {
            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                // ジャンプ開始
                return true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (jumpCount > 0 && jumpCount < maxJumpCount)
                {
                    return true;
                }
            }
            return false;
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
            if (canJump())
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
            this.velocity.y -= GRAVITY * deltaTime;

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

        private void CollideObstacle(float deltaTime)
        {
            this.isGround = false;
            var result = this.collisionChecker.CollideObstacle(this);
            switch (result)
            {
                case ICollisionChecker.CollideObstacleResult.None:
                    break;
                case ICollisionChecker.CollideObstacleResult.LeftToRight:
                    break;
                case ICollisionChecker.CollideObstacleResult.TopToBottom:
                    this.isGround = true;
                    break;
                case ICollisionChecker.CollideObstacleResult.RightToLeft:
                    break;
                case ICollisionChecker.CollideObstacleResult.BottomToTop:
                    break;
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
