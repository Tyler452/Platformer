using UnityEngine;

public class QuestionBlockAnimator : MonoBehaviour
{
    public Material questionBlockMaterial;
    public float frameDuration = 0.2f;
    public GameObject coinPrefab;

    private bool isHit = false;
    private bool isAnimating = true;

    private void Start()
    {
        StartCoroutine(AnimateQuestionBlock());
    }

    private System.Collections.IEnumerator AnimateQuestionBlock()
    {
        while (isAnimating)
        {
            for (float offset = 0f; offset <= 0.8f; offset += 0.2f)
            {
                questionBlockMaterial.mainTextureOffset = new Vector2(0, offset);
                yield return new WaitForSeconds(frameDuration);
            }
            questionBlockMaterial.mainTextureOffset = new Vector2(0, 0);
            yield return new WaitForSeconds(frameDuration);
        }
    }

    public void OnBlockHit()
    {
        if (!isHit)
        {
            isHit = true;
            isAnimating = false;

            if (coinPrefab != null)
            {
                GameObject coin = Instantiate(coinPrefab, transform.position + Vector3.up, Quaternion.identity);
                Animator coinAnimator = coin.GetComponent<Animator>();
                if (coinAnimator != null)
                {
                    coinAnimator.Play("CoinPop");
                }
                Destroy(coin, 1f);
                GameManager.Instance.AddCoins(1); // Add a coin
                GameManager.Instance.AddPoints(100); // Add points for collecting a coin
            }

            GetComponent<Collider>().enabled = false; // Disable further interactions
        }
    }
}