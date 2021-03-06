﻿using UnityEngine;

public class PlayerLocomotion
{
    public bool Enabled { get; private set; } = true;

    private PlayerCharacter _player;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public PlayerLocomotion(PlayerCharacter player)
    {
        _player = player;
        _rigidbody = player.Avatar.GetComponent<Rigidbody>();

        // Record start pos and rot
        _startPosition = player.Avatar.transform.position;
        _startRotation = player.Avatar.transform.rotation;

        // Constrain rigidbody
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation |
                                 RigidbodyConstraints.FreezePositionY;

        // Set zero friction
        var colliders = player.Avatar.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.material = new PhysicMaterial()
            {
                dynamicFriction = 0,
                staticFriction = 0,
                frictionCombine = PhysicMaterialCombine.Minimum
            };
        }
    }

    public void Move(Vector3 direction)
    {
        if (Enabled)
            _moveDirection = direction.normalized;
    }

    public void Update()
    {
        if (_moveDirection.magnitude > 0.1f)
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(_moveDirection), 540 * Time.deltaTime);
        _rigidbody.velocity = _moveDirection * GetMoveSpeed();
        _moveDirection = Vector3.zero;
    }

    public void Reset()
    {
        Enable();
        _player.Avatar.transform.position = _startPosition;
        _player.Avatar.transform.rotation = _startRotation;
    }

    public void Enable() => Enabled = true;
    public void Disable() => Enabled = false;

    private float GetMoveSpeed() => GameSettings.PlayerMoveSpeed;
}
