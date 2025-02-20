using System;
using System.Collections;
using UnityEngine;

public class DemoLogic : MonoBehaviour
{
    public GameObject parachuteObj;
    public GameObject packageObj;
    public float parachuteOpenDistance = 5f;
    public float parachuteDrag = 7f;
    public Transform parachutePivot;
    public Transform debugSphereTransform;
    public Camera cam;
    
    float defualtParachuteOpenDistance;
    bool hasParachuteOpened;
    public float parachuteOpenTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defualtParachuteOpenDistance = packageObj.GetComponent<Rigidbody>().linearDamping;
        parachuteObj.SetActive(false);

       // StartCoroutine(TestCoroutine());
    }
/*
    IEnumerator TestCoroutine()
    {
        Debug.Log("Started Coroutine");
        yield return new WaitForSeconds(1f);
        Debug.Log("next");
        yield return new WaitForSeconds(1f);
        Debug.Log("Last thing");
    }
*/
    // Update is called once per frame
    void Update()
    {
        Ray lookForGround = new Ray(packageObj.transform.position, Vector3.down);
        if (Physics.Raycast(lookForGround, out RaycastHit hitInfo))
        {
            if (hitInfo.distance < parachuteOpenDistance)
            {
                bool chuteOpen = (hitInfo.distance < parachuteOpenDistance);
                //Draw a colored line from the box to the ground
                Color lineColor = (hitInfo.distance < parachuteOpenDistance) ? Color.red : Color.blue;
                Debug.DrawRay(packageObj.transform.position, Vector3.down * parachuteOpenDistance, lineColor);
                
                //update drag
                packageObj.GetComponent<Rigidbody>().linearDamping = (chuteOpen) ? parachuteDrag : defualtParachuteOpenDistance;
                parachuteObj.SetActive(chuteOpen);
                
                if (chuteOpen && !hasParachuteOpened)
                {
                    StartCoroutine(AnimateParachuteOpen());
                    hasParachuteOpened = true;
                    
                }
            }

            

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Input.mousePosition;
                
                Ray cursorRay = cam.ScreenPointToRay(Input.mousePosition);
                bool rayHitSomething = Physics.Raycast(cursorRay, out RaycastHit screenHitInfo);
                if (rayHitSomething && screenHitInfo.transform.gameObject.CompareTag("Brick"))
                {
                    debugSphereTransform.position = screenHitInfo.point; 
                }
            }
        }

        IEnumerator AnimateParachuteOpen()
        {
            float duration = 2f;
            float elapsedTime = 0f;
            parachutePivot.localScale = Vector3.zero;

            while (elapsedTime < duration)
            {
                float percentComplete = elapsedTime / duration;
                parachutePivot.localScale = new Vector3(percentComplete, percentComplete, percentComplete);
                //Debug.Log(percentComplete);
            }
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        
    }
    
}
