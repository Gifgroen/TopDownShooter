using ColourInMotion.Input;
using UnityEngine;

namespace ColourInMotion.Control
{
    public class VehicleController : MonoBehaviour
    {
        #region Constants

        private const float GravityFactor = 9.81f;

        #endregion

        #region Exposed fields

        [SerializeField] private VehicleInputProcessor inputProcessor;

        [SerializeField] private float movementSpeed = 1;

        [SerializeField] private float reverseSpeed = 1;

        [SerializeField] private float turnSpeed = 1;

        [SerializeField] private float airDrag;

        [SerializeField] private Rigidbody movementSphere;

        [SerializeField] private LayerMask floorLayerMask;

        #endregion

        #region Private fields

        private Vector2 _inputAxis;

        private float _movementInput;
        
        private float _turnInput;
        
        private float _groundDrag;

        [SerializeField] private bool isGrounded;

        #endregion

        #region Unity lifecycle

        private void OnEnable()
        {
            inputProcessor.EnableMovement();
            inputProcessor.OnMove += OnMove;
        }

        private void OnDisable()
        {
            inputProcessor.OnMove -= OnMove;
        }

        private void Start()
        {
            movementSphere.transform.parent = null;
            _groundDrag = movementSphere.drag;
        }

        private void Update()
        {
            float movementInput = _inputAxis.y;
            float speed = movementInput > 0 ? movementSpeed : reverseSpeed;
            _movementInput = movementInput * speed;

            float horizontalInput = _inputAxis.x;
            _turnInput = horizontalInput;

            Transform transform1 = transform;

            transform1.position = movementSphere.position;

            float newRotation = _turnInput * turnSpeed * Time.deltaTime * movementInput;
            transform1.Rotate(0, newRotation, 0, Space.World);

            Vector3 up = transform1.up;
            isGrounded = Physics.Raycast(transform1.position, -up, out RaycastHit hit, 1f, floorLayerMask);
            transform1.rotation *= Quaternion.FromToRotation(up, hit.normal);

            movementSphere.drag = isGrounded ? _groundDrag : airDrag;
        }

        private void FixedUpdate()
        {
            if (isGrounded)
            {
                movementSphere.AddForce(transform.forward * _movementInput, ForceMode.Acceleration);
            }
            else
            {
                movementSphere.AddForce(Vector3.down * GravityFactor);
            }
        }

        #endregion

        #region Input handling

        private void OnMove(Vector2 input)
        {
            _inputAxis = input;
        }

        #endregion
    }
}