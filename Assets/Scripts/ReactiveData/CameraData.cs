using UnityEngine;

namespace Money
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/Configs/New Camera Data", order = 1)]
    public class CameraData : BaseDataRef<Camera>
    {
        public override void OnAfterDeserialize()
        {
            runtimeValue = null;
        }
    }
}