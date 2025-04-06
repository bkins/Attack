using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Player;

        public Vector3   Offset      = new Vector3(0, 10, -10);
        public float     SmoothSpeed = 0.125f;

        private void LateUpdate()
        {
            var desiredPosition  = Player.position + Offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);

            transform.position = smoothedPosition;
            transform.LookAt(Player);
        }
    }
}