using UnityEngine;
using UnityUIAliasForSelectable = UnityEngine.UI.Selectable; // make an alias

namespace Money
{
	public class SetInteractableOnBoolValue : MonoBehaviour
	{
		[SerializeField] BoolData boolData;
		[SerializeField] bool boolValue;
		[SerializeField] UnityUIAliasForSelectable targetUIElement;

		void OnEnable()
		{
            targetUIElement.interactable = (boolValue == boolData.Value);
		}
	}
}