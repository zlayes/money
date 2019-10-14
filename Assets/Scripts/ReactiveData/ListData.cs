using System.Collections.Generic;
using UnityEngine;

namespace Money
{
	[CreateAssetMenu(fileName = "ListData", menuName = "ScriptableObjects/Configs/New List Data", order = 1)]
	public class ListData : BaseDataRef<List<GameObject>>
    {
        public override void OnAfterDeserialize()
        {
            runtimeValue = new List<GameObject>();
        }
    }
}