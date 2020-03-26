using UnityEngine;

namespace PingPong
{
    public class InputContrloller : MonoBehaviour
    {
        public GameObject ball;
        public GameObject arrow;

        private Vector2 arrowSize;
        private Vector3 startMousePos;
        private Vector3 nextMousePos;

        private static readonly float scaleArrowIndex = 100f;
        private static readonly float scaleForceIndex = 30f;
        private static readonly float minForce = 1.5f;

        private bool touchIsActive = false;

        public void Initialisation()        
        {
            if (arrow != null)
            {
                arrowSize = arrow.GetComponent<SpriteRenderer>().size;
                SetArrow(false);
            }
        }

        public void LocalUpdate()
        {
            if (BallIsActive())
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                {
                    SetArrow(true);
                    startMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    touchIsActive = true;
                }
                else if (touchIsActive && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
                {
                    nextMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    UpdateArrow(nextMousePos, Vector3.Distance(startMousePos, nextMousePos));
                }
                else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
                {
                    ShootBall();
                    SetArrow(false);
                    touchIsActive = false;
                }
            }
        }

        private bool BallIsActive()
        {
            if (ball == null)
                return false;
            return !ball.GetComponent<Ball>().startBall;
        }

        private void ShootBall()
        {
            Vector3 forceDirection = ((nextMousePos - startMousePos) * scaleForceIndex) * -1f;
            forceDirection.z = forceDirection.y;

            if (forceDirection.magnitude > minForce)
            {
                ball.GetComponent<Rigidbody>().AddForce(forceDirection, ForceMode.Impulse);
                ball.GetComponent<Ball>().startBall = true;
            }
        }

        private void SetArrow(bool isVisible)
        {
            arrow.SetActive(isVisible);
            arrow.GetComponent<SpriteRenderer>().size = arrowSize;
        }

        private void UpdateArrow(Vector3 dirrection, float scale)
        {
            SetArrow(true);

            arrow.GetComponent<SpriteRenderer>().size = new Vector2(arrowSize.x + (scale * scaleArrowIndex), arrowSize.y);

            Vector3 rotation = arrow.transform.localRotation.eulerAngles;
            rotation.y = Quaternion.FromToRotation(Vector3.down, dirrection - startMousePos).eulerAngles.z + 270;
            arrow.transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}