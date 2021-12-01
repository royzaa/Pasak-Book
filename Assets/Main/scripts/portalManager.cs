using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class portalManager : MonoBehaviour
{
    public GameObject Classroom;

    [SerializeField] GameObject VideoScreen;

    [SerializeField] GameObject Portal;

    private List<Material> ClassroomMaterial = new List<Material>();

    InterfaceController interfaceController;

    // Start is called before the first frame update
    void Awake()
    {
        Renderer[] rs = Classroom.GetComponentsInChildren<Renderer>();
        Debug.Log(rs.Length);
        foreach (Renderer r in rs)
        {
            foreach (Material m in r.sharedMaterials)
            {
                if (m != null)
                {
                    ClassroomMaterial.Add(m);
                }
            }
            
        }
        interfaceController = FindObjectOfType<InterfaceController>();
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "portal")
        {
            Debug.Log(ClassroomMaterial.Count);
            Vector3 camPositionPortal = Portal.transform.InverseTransformPoint(transform.position);
            //if (camPositionPortal.y < 0.5f && camPositionPortal.y > -0.5f)
            //{
            //    print("tesekusi");
            //    for (int i = 0; i < ClassroomMaterial.Length; ++i)
            //    {
            //        ClassroomMaterial[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
            //    }
            //    portalPlane.SetInt("_CullMode", (int)CullMode.Off);

            //}
            if (camPositionPortal.y < 0.5f)
            {
                Debug.Log("stencil equal");
                for (int i = 0; i < ClassroomMaterial.Count; i++)
                {
                    ClassroomMaterial[i].SetInt("_StencilComp", (int)CompareFunction.Equal);
                }
                

            } else
            {
                Debug.Log("else is triggered");
                for (int i = 0; i < ClassroomMaterial.Count; ++i)
                {
                    ClassroomMaterial[i].SetInt("_StencilComp", (int)CompareFunction.Always);
                }
                interfaceController.hint.text = "[X] Amati sekeliling dan dektati panah berwarna kuning";
            
            }
        } 
        else if (other.gameObject.tag == "arrow")
        {
            interfaceController.hint.gameObject.SetActive(false);
            Debug.Log("video screen active");
            VideoScreen.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "arrow")
        {
            VideoScreen.SetActive(false);
        }
    }
}
