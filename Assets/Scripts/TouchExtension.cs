using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TouchExtension : MonoBehaviour
{
    float timeBetweenClicks = 0.2f;
    bool clickEnabled = true;
    bool doubleClicked = false;
    bool clickComplete = false;
    bool performDouble = false;
    //int numClicks = 0;
    public UnityEvent singleClick;
    public UnityEvent doubleClick;

    void Awake()
    {
        if (singleClick == null)
        {
            singleClick = new UnityEvent();
        }
        if (doubleClick == null)
        {
            doubleClick = new UnityEvent();
        }
    }

    void Update()
    {
        if (clickComplete)
        {
            clickComplete = false;
            if (performDouble == true)
            {
                // Do double click
                doubleClick.Invoke();
            }
            else
            {
                // Do single click
                singleClick.Invoke();
            }
            performDouble = false;
        }
    }

    void OnMouseUp()
    {
        if (clickEnabled)
        {
            clickEnabled = false;
            StartCoroutine("DoubleClick", timeBetweenClicks);
        }
    }

    IEnumerator DoubleClick(float delay)
    {
        //Debug.Log("Starting to listen for double clicks");
        float endTime = Time.time + delay;
        while (Time.time < endTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Double click!");
                yield return new WaitForSeconds(0.4f);
                clickEnabled = true;
                doubleClicked = true;

            }
            yield return 0;
        }

        if (!doubleClicked)
        {
            //Debug.Log("no double click in " + delay + " amount of time");
            //single click
        }
        else
        {
            doubleClicked = false;
            performDouble = true;
        }

        clickEnabled = true;
        clickComplete = true;
        yield return 0;
    }
}

