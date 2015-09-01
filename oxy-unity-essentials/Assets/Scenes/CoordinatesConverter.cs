using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoordinatesConverter : MonoBehaviour 
{
	#region Inspector Values
	[SerializeField]
	private RectTransform target;
	[SerializeField]
	private Canvas targetCanvas;
	[SerializeField]
	private RectTransform pointer;
	[SerializeField]
	private Canvas pointerCanvas;
	#endregion
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PositionPointerToTarget();
		}
	}

	private void PositionPointerToTarget ()
	{
		// 1. Get screen position of the target
		Vector3 wp0 = target.position;
		Vector2 screenPos;
		if (targetCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
		{
			screenPos = (Vector2)wp0;
		}
		else
		{
			screenPos = targetCanvas.worldCamera.WorldToScreenPoint(wp0);
		}
		Debug.LogFormat("Screen position of the target: ({0}, {1})", screenPos.x, screenPos.y);
		
		// 2. Convert screen position of target to world position of pointer
		Vector2 pointerPos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			(RectTransform)pointer.parent, screenPos, pointerCanvas.worldCamera, out pointerPos);
		pointer.localPosition = pointerPos;
	}
}
