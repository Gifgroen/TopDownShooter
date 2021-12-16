using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ColourInMotion.RaceShooter.Input
{
    [CreateAssetMenu(fileName = "new VehicleInputProcessor", menuName = "ColourInMotion/Create VehicleInputProcessor")]
    public class VehicleInputProcessor : ScriptableObject, VehicleControllerInputActions.IPlayerActions
    {
        #region Exposed fields

        public event UnityAction<Vector2> OnMove;
        
        #endregion

        #region Private fields

        private VehicleControllerInputActions _actionMap;

        #endregion
        
        #region Unity Lifecycle

        private void OnEnable()
        {
            _actionMap = new VehicleControllerInputActions();
            _actionMap.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            _actionMap.Player.Disable();
            _actionMap.Player.SetCallbacks(null);
        }

        #endregion

        #region Input processing

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed && context.phase != InputActionPhase.Canceled)
            {
                return;
            }

            OnMove?.Invoke(context.ReadValue<Vector2>());
        }

        #endregion

        public void EnableMovement()
        {
            _actionMap.Player.Enable();
        }
    }
}
