using ColourInMotion.CARkanoid.Input;
using UnityEngine;

namespace ColourInMotion.CARkanoid.Control
{
    public class Player : MonoBehaviour
    {
        #region Exposed fields
        [SerializeField] private InputProcessor inputProcessor;

        [SerializeField] private float speed;
        #endregion
        
        #region Private fields
        private float _moveDirection;
        #endregion

        #region Unity Lifecycle
        private void Start()
        {
            inputProcessor.OnMove += OnMove;
        }

        private void FixedUpdate()
        {
            Transform myTransform = transform;
            myTransform.position += _moveDirection * speed * Time.deltaTime * myTransform.right;
        }
        #endregion

        #region Input Handlers
        private void OnMove(float direction)
        {
            _moveDirection = direction;
        }
        #endregion
    }
}