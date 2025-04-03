using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    Dictionary<string, int> directionMap = new Dictionary<string, int>();
    private int score = 0;
    private int multiplier = 1;

    private void Start()
    {
        directionMap.Add("Up", 0);
        directionMap.Add("Down", 0);
        directionMap.Add("Left", 0);
        directionMap.Add("Right", 0);
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
        if(directionMap[direction] >= 1)
        {
            Debug.Log("Den var rätt");
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
            directionMap[direction] += 1;

        }
        StartCoroutine(DeactivateAfterDelay(direction, 0.3f));
    }
    private void DeactivateSubCatchBox(string direction)
    {

        if (directionMap.ContainsKey(direction)) 
        {
            directionMap[direction] -= 1;
        }
    }
    private IEnumerator DeactivateAfterDelay(string direction, float delay)
    {
        yield return new WaitForSeconds(delay);  

        DeactivateSubCatchBox(direction);
    }
}
