using UnityEngine;
using System.Collections;

public class BillboardTest : MonoBehaviour 
{
    Camera mainCamera;
    //BillboardRenderer billboardRenderer;
    //BillboardAsset billboardAsset;

	// Use this for initialization
	void Start () 
    {
        //billboardAsset = new BillboardAsset();
        //billboardRenderer = GetComponent<BillboardRenderer>();
        //billboardAsset.height = 1.0f;
        //billboardAsset.width = 1.0f;
        //billboardAsset.bottom = -1.0f;
        //Debug.Log("Ran through BillboardTest");
        //billboardRenderer.billboard = billboardAsset;
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(mainCamera.transform.position);
	}

    void OnBecameInvisible()
    {
        enabled = false;
    }

    void OnBecameVisible()
    {
        enabled = true;
    }
}