using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    Dictionary<string, bool> directionMap = new Dictionary<string, bool>();
    private int score = 0;
    private int multiplier = 1;

    private void Start()
    {
        directionMap.Add("Up", false);
        directionMap.Add("Down", false);
        directionMap.Add("Left", false);
        directionMap.Add("Right", false);
    }


    public void SendDanceDirectionPlayer(Vector2 direction, Player player)
    {
        switch (true)
        {
            case bool _ when direction.x < 0:
                CheckIfValid("Left", player);
                break;

            case bool _ when direction.x > 0:
                CheckIfValid("Right", player);
                break;

            case bool _ when direction.y > 0:
                CheckIfValid("Up", player);
                break;

            case bool _ when direction.y < 0:
                CheckIfValid("Down", player);
                break;
        }
    }

    private void CheckIfValid(string direction, Player player)
    {
        if(directionMap[direction] == true)
        {
            Debug.Log("Den var rätt");
            directionMap[direction] = false;
            UpdateScore();
            multiplier++;
            
        }
        else
        {
            Debug.Log("DU SUGER");
            multiplier = 1;
        }
    }

    private void UpdateScore()
    {
        score += 50 * multiplier;
        scoreText.text = score.ToString();
    }
    public void ActivateSubCatchBox(string direction)
    {
        if (directionMap.ContainsKey(direction))  
        {
            directionMap[direction] = true;

        }
        StartCoroutine(DeactivateAfterDelay(direction, 0.5f));
    }
    private void DeactivateSubCatchBox(string direction)
    {

        if (directionMap.ContainsKey(direction)) 
        {
            directionMap[direction] = false;
        }
    }
    private IEnumerator DeactivateAfterDelay(string direction, float delay)
    {
        yield return new WaitForSeconds(delay);  

        DeactivateSubCatchBox(direction);
    }
}
