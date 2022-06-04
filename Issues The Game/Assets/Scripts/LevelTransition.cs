using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    public int index;
    private AnimationController controller;
    private TileMapMove tileMapMove;

    private void Start()
    {
        tileMapMove = FindObjectOfType<TileMapMove>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        tileMapMove.StopInk();
        if (other.CompareTag("Player"))
        {
            controller = other.GetComponent<AnimationController>();
            controller.GoalState();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(index);
    }

}
