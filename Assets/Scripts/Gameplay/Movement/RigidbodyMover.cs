using UnityEngine;

namespace GameName.Gameplay.Movement
{
    /// <summary>
    ///     Contains methods for moving and rotating a Rigidbody.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMover : ObjectMover
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override Vector3 GetPosition()
        {
            return _rigidbody.position;
        }

        public override Quaternion GetRotation()
        {
            return _rigidbody.rotation;
        }

        public override Vector3 GetForward()
        {
            return _rigidbody.transform.forward;
        }

        public override Vector3 GetRight()
        {
            return _rigidbody.transform.right;
        }

        public override void AddPosition(Vector3 addition)
        {
            Vector3 p = _rigidbody.position;
            _rigidbody.MovePosition(p + addition);
        }

        public override void SetPosition(Vector3 position)
        {
            _rigidbody.MovePosition(position);
        }

        public override void MoveTowards(Vector3 position, float maxDistanceDelta)
        {
            _rigidbody.MovePosition(Vector3.MoveTowards(_rigidbody.position, position, maxDistanceDelta));
        }

        public override void AddRotation(Quaternion addition)
        {
            _rigidbody.MoveRotation(_rigidbody.rotation * addition);
        }

        public override void SetRotation(Quaternion rotation)
        {
            _rigidbody.MoveRotation(rotation);
        }

        public override void RotateTowards(Quaternion rotation, float maxDegreesDelta)
        {
            _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation, rotation, maxDegreesDelta));
        }
    }
}