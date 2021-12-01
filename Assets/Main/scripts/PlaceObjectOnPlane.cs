using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class PlaceObjectOnPlane : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject placedPrefab;
    public Material occlusionMat, planeMat;
    public GameObject planePrefab;

    InterfaceController interfaceController;

    private bool isNotHaveObject = true;

    private void Awake() {
        interfaceController = FindObjectOfType<InterfaceController>();
        planeManager.enabled = true;
        raycastManager.enabled = true;
        placedPrefab.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            PlaceObject();
        }

        if (!isNotHaveObject)
        {
            planeManager.requestedDetectionMode = PlaneDetectionMode.None;
            planeManager.SetTrackablesActive(false);
            
        }
    }

    private void PlaceObject()
    {
        if (isNotHaveObject)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !IsPointerOverUIObject())
            {
                Debug.Log("instantiate ...");
                if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
                {
                    Debug.Log("instantiate 1...");
                    Pose hitPose = hits[0].pose;
                    Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    placedPrefab.transform.position = new Vector3(hitPose.position.x, 0.0f, hitPose.position.z);
                    Debug.Log("instantiate 2...");
                    placedPrefab.SetActive(true);
                    Debug.Log("instantiate 3...");
                    
                    
                    isNotHaveObject = !placedPrefab.activeInHierarchy;
                    interfaceController.hint.text = "Jalan perlahan ke dalam kelas";
                    
                }
            } 
        }

       
    }

    private bool IsPointerOverUIObject()
    {
        Debug.Log("instantiate pointer...");
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
         Debug.Log("instantiate pointer 1...");
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
         Debug.Log("instantiate pointer 2...");
        List<RaycastResult> results = new List<RaycastResult>();
         Debug.Log("instantiate pointer 3...");
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
         Debug.Log("instantiate pointer 4...");

        return results.Count > 0;
    }

    private void SetOcclusionMaterial() 
    {
        planePrefab.GetComponent<Renderer>().material = occlusionMat;
        foreach (var plane in planeManager.trackables)
        {
            plane.GetComponent<Renderer>().material = occlusionMat;
        }
    }

    private void SetPlaneMaterial() 
    {
        if (isNotHaveObject)
        {
            planePrefab.GetComponent<Renderer>().material = planeMat;
            foreach (var plane in planeManager.trackables)
            {
                plane.GetComponent<Renderer>().material = planeMat;
            }
        }
    }

}
