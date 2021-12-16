using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ColourInMotion.CARkanoid.Input
{
    [CreateAssetMenu(fileName = "InputProcessor")]
    public class InputProcessor : ScriptableObject, PlayerControllerInputActions.IPlayerActions
    {
        public event UnityAction<float> OnMove;

        private PlayerControllerInputActions _controls; 

        #region Unity Lifecycle
        private void OnEnable()
        {
            _controls = new PlayerControllerInputActions();
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
        }
        #endregion

        #region Input processing
        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.performed == false || context.canceled == false)
            {
                OnMove?.Invoke(context.ReadValue<float>());
            }
        }
        #endregion
    }
}
