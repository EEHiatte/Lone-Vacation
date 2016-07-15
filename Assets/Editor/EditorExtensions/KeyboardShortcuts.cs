using UnityEditor;
using UnityEngine;
using System.Collections;

public class KeyboardShortcuts : MonoBehaviour
{
    [MenuItem("Shortcuts/Anchor Around Object #&l")]
    static void uGUIAnchorAroundObject()
    {
        GameObject activeObject = Selection.activeGameObject;
        if (activeObject != null && activeObject.GetComponent<RectTransform>() != null)
        {
            RectTransform rectTransform = activeObject.GetComponent<RectTransform>();
            RectTransform parentRectTransform = activeObject.transform.parent.GetComponent<RectTransform>();

            Vector2 offsetMin = rectTransform.offsetMin;
            Vector2 offsetMax = rectTransform.offsetMax;
            Vector2 _anchorMin = rectTransform.anchorMin;
            Vector2 _anchorMax = rectTransform.anchorMax;

            float parent_width = parentRectTransform.rect.width;
            float parent_height = parentRectTransform.rect.height;

            Vector2 anchorMin = new Vector2(_anchorMin.x + (offsetMin.x / parent_width),
                                        _anchorMin.y + (offsetMin.y / parent_height));
            Vector2 anchorMax = new Vector2(_anchorMax.x + (offsetMax.x / parent_width),
                                        _anchorMax.y + (offsetMax.y / parent_height));

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;

            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}


