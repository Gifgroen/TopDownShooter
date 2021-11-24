using UnityEngine;

namespace ColourInMotion.Core
{
    public class CameraFollow: MonoBehaviour
    {
        [SerializeField] private Transform target;
    
        [SerializeField] private float smoothTime = 0.3f;
 
        private Vector3 _velocity = Vector3.zero;
 
        private void Update() {
            Vector3 goalPos = target.position;
            goalPos.y = transform.position.y;
            transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref _velocity, smoothTime);
        }
    }
}
