using System;
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
        
        [SerializeField] private Rigidbody movementSphere;
        
        #endregion
        
        #region Private fields

        private Vector2 _inputAxis;
        
        private float _movementInput;
        private float _turnInput;
        
        #endregion

        #region Unity lifecycle

        private void OnEnable()
        {
            inputProcessor.OnMove += OnMove;
        }

        private void OnDisable()
        {
            inputProcessor.OnMove -= OnMove;
        }

        private void Start()
        {
            movementSphere.transform.parent = null;
        }

        private void Update()
        {
            float movementInput = _inputAxis.y;
            float speed = movementInput > 0 ? movementSpeed : reverseSpeed;
            _movementInput = movementInput * speed;

            float horizontalInput = _inputAxis.x;
            _turnInput = horizontalInput;
            
            transform.position = movementSphere.position;

            float newRotation = _turnInput * turnSpeed * Time.deltaTime * movementInput;
            transform.Rotate(0, newRotation, 0, Space.World);
        }
        
        private void FixedUpdate()
        {
            movementSphere.AddForce(transform.forward * _movementInput, ForceMode.Acceleration);

            movementSphere.AddForce(Vector3.down * GravityFactor, ForceMode.Acceleration);
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