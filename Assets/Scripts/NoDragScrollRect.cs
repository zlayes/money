using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoDragScrollRect : ScrollRect
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        //ExecuteEvents.ExecuteHierarchy<IBeginDragHandler>(transform.parent.gameObject, eventData, ExecuteEvents.beginDragHandler);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        //ExecuteEvents.ExecuteHierarchy<IDragHandler>(transform.parent.gameObject, eventData, ExecuteEvents.dragHandler);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        //ExecuteEvents.ExecuteHierarchy<IEndDragHandler>(transform.parent.gameObject, eventData, ExecuteEvents.endDragHandler);
    }
}