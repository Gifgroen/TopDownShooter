using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ColourInMotion.Input
{
    [CreateAssetMenu(fileName = "new VehicleInputProcessor", menuName = "ColourInMotion/Create VehicleInputProcessor")]
    public class VehicleInputProcessor : ScriptableObject, VehicleControllerInput.IPlayerActions
    {
        #region Exposed fields

        public event UnityAction<Vector2> OnMove = delegate { };
        
        #endregion

        #region Private fields

        private VehicleControllerInput _actionMap;

        #endregion
        
        #region Unity Lifecycle

        private void OnEnable()
        {
            _actionMap = new VehicleControllerInput();
            _actionMap.Player.SetCallbacks(this);
            _actionMap.Enable();
        }

        private void OnDisable()
        {
            _actionMap.Disable();
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
            OnMove(context.ReadValue<Vector2>());
        }

        #endregion
    }
}
