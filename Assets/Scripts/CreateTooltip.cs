using UnityEngine;
using System.Collections;

public class CreateTooltip : MonoBehaviour
{
    GameObject tooltip;
    public string nameText = "NAME";
    public string descriptionTest = "DESCRIPTION";
    bool tooltipOn = false;

    void OnMouseOver()
    {
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (tooltipOn == false)
            {
                tooltip = Instantiate(Resources.Load("Tooltip"), Input.mousePosition, Quaternion.identity) as GameObject;
                tooltip.GetComponent<TooltipBehavior>().nameText = nameText;
                tooltip.GetComponent<TooltipBehavior>().descriptionText = descriptionTest;
                tooltipOn = true;
            }
        }
        else
        {
            Destroy(tooltip);
            tooltipOn = false;
        }
    }

    void OnMouseExit()
    {
        if (tooltip != null)
        {
            Destroy(tooltip);
            tooltipOn = false;
        }
    }

    void OnDestroy()
    {
        if (tooltip != null)
        {
            Destroy(tooltip);
            tooltipOn = false;
        }
    }
}
