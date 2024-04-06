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

        private Vector2 velocity = Vector2.zero;
        private int jumpCount = 0;
        private int maxJumpCount = 10;

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
            if (this.rectTransform.position.y > 0)
            {
                this.velocity.y -= (MOVE_VELOCITY * deltaTime) / 2;
            }
            else
            {
                this.velocity.y = 0;
            }

            if (this.velocity.sqrMagnitude > (MAX_VELOCITY * MAX_VELOCITY))
            {
                this.velocity = this.velocity.normalized * MAX_VELOCITY;
            }

            this.rectTransform.position += new Vector3(this.velocity.x, this.velocity.y);
            if (this.rectTransform.position.y < 0)
            {
                var pos = this.rectTransform.position;
                pos.y = 0;
                this.rectTransform.position = pos;
            }
        }

        public Rect GetRect()
        {
            return this.rectTransform.rect;
        }

        public void SetPositionVelocity(Vector2 position, Vector2 velocity)
        {
            this.rectTransform.position = position;
            this.velocity += velocity;
        }

        public Vector2 GetVelocity() => this.velocity;
    }
}
