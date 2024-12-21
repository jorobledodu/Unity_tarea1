using UnityEngine;
using System.Collections;

public class VisualTutorial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        Invoke("InstanciadorCanvas", 1.5f);
        Invoke("DesInstanciadorCanvas",13);
    }

    void InstanciadorCanvas()
    {
        gameObject.SetActive(true);
    }

    void DesInstanciadorCanvas()
    {
        gameObject.SetActive(false);
    }
}
