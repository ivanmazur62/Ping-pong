using UnityEngine;

namespace PingPong
{
    public class Ball : MonoBehaviour
    {
        private float lifeTime = 10;
        private float currentTime = 0;
        public bool startBall = false;

        public void LocalUpdate()
        {
            if (startBall)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= lifeTime)
                    DestroyBall();
            }
        }

        public void DestroyBall()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponent<Block>())
            {
                GameController.Instance.levelController.DestroyBlock(collider.GetComponent<Block>());
                DestroyBall();
            }
        }
    }
}