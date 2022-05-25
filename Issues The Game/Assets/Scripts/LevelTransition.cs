using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    public int index;
    private AnimationController controller;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //LoadScene();
            controller = other.GetComponent<AnimationController>();
            controller.GoalState();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(index);
    }

}
