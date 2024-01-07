using UnityEngine;

namespace GameName.Gameplay.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMover : ObjectMover
    {
        private CharacterController _controller;
        private Transform _transform;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _transform = _controller.transform;
        }

        public override Vector3 GetPosition()
        {
            return _transform.position;
        }

        public override Quaternion GetRotation()
        {
            return _transform.rotation;
        }

        public override Vector3 GetForward()
        {
            return _transform.forward;
        }

        public override Vector3 GetRight()
        {
            return _transform.right;
        }

        public override void AddPosition(Vector3 addition)
        {
            _controller.SimpleMove(addition);
        }

        public override void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public override void MoveTowards(Vector3 position, float maxDistanceDelta)
        {
            _controller.Move(Vector3.MoveTowards(_transform.position, position, maxDistanceDelta));
        }

        public override void AddRotation(Quaternion addition)
        {
            _transform.rotation *= addition;
        }

        public override void SetRotation(Quaternion rotation)
        {
            _transform.rotation = rotation;
        }

        public override void RotateTowards(Quaternion rotation, float maxDegreesDelta)
        {
            Quaternion rot = Quaternion.RotateTowards(_transform.rotation, rotation, maxDegreesDelta);
            _transform.rotation = rot;
        }
    }
}