using UnityEngine;

namespace Money
{
	[CreateAssetMenu(fileName = "TextureData", menuName = "ScriptableObjects/Configs/New Texture Data", order = 1)]
	public class TextureData : BaseDataRef<Texture>
	{
        public override void OnAfterDeserialize()
        {
            runtimeValue = null;
        }
    }
}
