using UnityEngine;

namespace GameName.Gameplay.Movement
{
    public class InputReceiver
    {
        public Vector3 value
        {
            get
            {
                var v = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                return v.normalized;
            }
        }
    }
}