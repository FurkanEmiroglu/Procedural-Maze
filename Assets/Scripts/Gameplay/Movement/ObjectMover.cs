using UnityEngine;

namespace GameName.Gameplay.Movement
{
    /// <summary>
    ///     A base class that can be used for both Rigidbody and Transform movement.
    ///     Contains methods for moving and rotating objects (not including tweening).
    /// </summary>
    public abstract class ObjectMover : MonoBehaviour
    {
        public abstract Vector3 GetPosition();

        public abstract Quaternion GetRotation();

        public abstract Vector3 GetForward();

        public abstract Vector3 GetRight();

        public abstract void AddPosition(Vector3 addition);

        public abstract void SetPosition(Vector3 position);

        public abstract void MoveTowards(Vector3 position, float maxDistanceDelta);

        public abstract void AddRotation(Quaternion addition);

        public abstract void SetRotation(Quaternion rotation);

        public abstract void RotateTowards(Quaternion rotation, float maxDegreesDelta);
    }
}