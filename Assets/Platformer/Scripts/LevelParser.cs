using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public Transform environmentRoot;

    [Header("Block Prefabs")]
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;

    void Start()
    {
        LoadLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ReloadLevel();
    }

    void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}/Resources/{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }
        }

        int row = 0;

        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();
            char[] letters = currentLine.ToCharArray();

            for (int col = 0; col < letters.Length; col++)
            {
                Vector3 pos = new Vector3(col + 0.5f, row + 0.5f, 0f);

                switch (letters[col])
                {
                    case 'x': 
                        Instantiate(rockPrefab, pos, Quaternion.identity, environmentRoot);
                        break;
                    case 'b': 
                        Instantiate(brickPrefab, pos, Quaternion.identity, environmentRoot);
                        break;
                    case '?': 
                        GameObject questionBlock = Instantiate(questionBoxPrefab, pos, Quaternion.identity, environmentRoot);
                        Renderer renderer = questionBlock.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material.mainTextureScale = new Vector3(-1f, -0.2f);
                            renderer.material.mainTextureOffset = new Vector3(0f, 0f);
                        }
                        break;
                    case 's': 
                        Instantiate(stonePrefab, pos, Quaternion.identity, environmentRoot);
                        break;
                }
            }
            row++;
        }
    }

    void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
            Destroy(child.gameObject);
        }
        LoadLevel();
    }
}