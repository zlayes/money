using UnityEngine;

namespace Money
{
    [CreateAssetMenu(fileName = "TransformData", menuName = "ScriptableObjects/Configs/New Transform Data", order = 1)]
    public class TransformData : BaseDataRef<Transform>
    {
        public override void OnAfterDeserialize()
        {
            runtimeValue = null;
        }
    }
}