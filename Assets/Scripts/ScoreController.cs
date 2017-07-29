using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    Text val;
    private void Start()
    {
        GameManager.instance.ScoreChange += OnScoreChange;
        val = GetComponent<Text>();


    }
    private void OnScoreChange()
    {
        val.text = GameManager.instance.Score.ToString();


    }
}
