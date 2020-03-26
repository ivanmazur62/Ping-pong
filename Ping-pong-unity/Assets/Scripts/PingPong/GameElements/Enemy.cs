using UnityEngine;

namespace PingPong
{
    public class Enemy : MonoBehaviour
    {
        public int Speed { get; private set; }
        private const float speedScale = 0.01f;

        public GameObject targetBall;

        public void LocalUpdate()
        {
            if (targetBall != null)
                MoveEnemy(targetBall.transform.position);
        }

        private void MoveEnemy(Vector3 targetPosition)
        {
            Vector3 lerpStep = Vector3.Lerp(gameObject.transform.position, targetPosition, Speed * speedScale);
            Vector3 nextStep = gameObject.transform.position;
            nextStep.x = lerpStep.x;

            GetComponent<Rigidbody>().MovePosition(nextStep);
        }

        public void SetSpeed(int value)
        {
            Speed = value;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<Ball>())
                collider.gameObject.GetComponent<Ball>().DestroyBall();
        }
    }
}