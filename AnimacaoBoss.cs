using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimacaoBoss : MonoBehaviour
{

    [SerializeField] private GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

       void Destruir()
    {
        Destroy(gameObject);
    }
    public void CriaBoss()
    {
         Instantiate(boss, transform.position,transform.rotation);
    }

    public void MorreBoss()
    {
        SceneManager.LoadScene(0);

    }
}
