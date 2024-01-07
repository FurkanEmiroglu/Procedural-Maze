using UnityEngine;

namespace GameName.MazeSystem
{
    [SelectionBase]
    public sealed class Wall : MonoBehaviour
    {
        [field: SerializeField] public WallType WallType { get; private set; }

        private void OnValidate()
        {
            Cell parent = GetComponentInParent<Cell>();
            
            if (parent != null) parent.OnValidate();
        }
    }
}