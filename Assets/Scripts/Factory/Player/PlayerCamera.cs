﻿using System;
using Factory.Utils;
using UnityEngine;

namespace Factory.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private float _sensitivity;

        [SerializeField]
        private float _minDelta;

        [SerializeField]
        private float _maxDelta;

        private Vector3 _cameraDelta;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.TryGetComponent(out ClickListener clickListener))
                    {
                        clickListener.TriggerClickEvent();
                    }
                }
            }

            _cameraDelta.x = Input.GetAxis("Mouse X");
            _cameraDelta.y = Input.GetAxis("Mouse Y");

            var speedVector = _cameraDelta * (_sensitivity * Time.deltaTime);

            if (_cameraDelta != Vector3.zero)
            {
                speedVector = MathUtils.ClampAbsoluteValue(speedVector, _minDelta, _maxDelta);
            }

            speedVector.y = -speedVector.y;

            var rotationAngles = transform.eulerAngles;
            rotationAngles.y += speedVector.x;
            _rigidbody.MoveRotation(Quaternion.Euler(rotationAngles));

            var nextXAngle = _camera.transform.eulerAngles.x + speedVector.y;

            switch (nextXAngle)
            {
                case <= 90f and >= 80f:
                case >= 270f and <= 280f:
                    speedVector.y = 0f;
                    break;
            }

            _camera.transform.Rotate(Vector3.right, speedVector.y);
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}