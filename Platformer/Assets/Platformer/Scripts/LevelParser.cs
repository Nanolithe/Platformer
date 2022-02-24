using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        // Go through the rows from bottom to top
        int row = 0;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            int column = 0;
            char[] letters = currentLine.ToCharArray();
            foreach (var letter in letters)
            {
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                var testObject = Instantiate(stonePrefab);
                var questionBox = Instantiate(questionBoxPrefab);
                var stoneBox = Instantiate(rockPrefab);
                var brickBox = Instantiate(brickPrefab);
                
                // Ground level
                if (letter == 'x')
                {
                    testObject.transform.position = new Vector3(column, row, 0f); 
                }
                
                // Special boxes
                if (letter == '?')
                {
                    questionBox.transform.position = new Vector3(column, row, 0f); 
                }
                
                // Boxes
                if (letter == 'b')
                {
                    brickBox.transform.position = new Vector3(column, row, 0f); 
                }
                
                // Stairs 
                if (letter == 's')
                {
                    stoneBox.transform.position = new Vector3(column, row, 0f); 
                }
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                column++;
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
