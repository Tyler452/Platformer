using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public GameObject coinPrefab; 
    public GameObject breakEffect; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("Brick"))
                {
                    Instantiate(breakEffect, clickedObject.transform.position, Quaternion.identity);
                    Destroy(clickedObject);
                    GameManager.Instance.AddPoints(10);
                }

                if (clickedObject.CompareTag("Question"))
                {
                    QuestionBlockAnimator questionBlock = clickedObject.GetComponent<QuestionBlockAnimator>();
                    if (questionBlock != null)
                    {
                        questionBlock.OnBlockClicked();
                    }
                }
            }
        }
    }
}