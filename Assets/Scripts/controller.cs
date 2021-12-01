using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARCore;

public class controller : MonoBehaviour
{
    private GameObject spawnedObject;

    [SerializeField]
    public GameObject targetFrame;
    public GameObject spawnedModel;


    
    private void Awake() {
        Renderer[] rs = spawnedModel.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs) {
            r.enabled = false;
        }
        
    }

    private void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            spawnedObject = Instantiate(spawnedModel, spawnedModel.transform.position, spawnedModel.transform.rotation);
            Renderer[] rs = spawnedObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
            {
                r.enabled = true;
            }
            Destroy(targetFrame);
            Destroy(spawnedModel);
        }
        
    }
}
