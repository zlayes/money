using UnityEngine;

namespace Money
{
    [CreateAssetMenu(fileName = "BoolData", menuName = "ScriptableObjects/Configs/New Bool Data", order = 1)]
    public class BoolData : BaseData<bool>
    {
        public void Invert()
        {
            Value = !Value;
        }
    }
}
