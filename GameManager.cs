using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Awake()
    {

        int quantidadeGa = FindObjectsOfType<GameManager>().Length;
        if(quantidadeGa > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CarregarCenaJogo()
    {
           SceneManager.LoadScene(1);
    }

    IEnumerator PrimeiraCena()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    public void CarregarCenaInicio()
    { 
        StartCoroutine(PrimeiraCena());
    }

    public void FecharJogo()
    {
        Application.Quit();
    }
}
