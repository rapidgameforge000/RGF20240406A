using Assets.Script.Character;
using UnityEngine;

namespace Assets.Script.MainCharacter
{
    public class MainCharacter : MonoBehaviour, ICharacter 
    {
        private RectTransform rectTransform = null;

        private Vector2 velocity = Vector2.zero;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            //Process(0);
        }

        public void Process(float deltaTime)
        {
            Move(deltaTime);

        }

        private void Move(float deltaTime)
        {
            if (Input.GetKey(KeyCode.Escape))
            {

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