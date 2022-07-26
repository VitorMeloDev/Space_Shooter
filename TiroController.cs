using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{

    private Rigidbody2D meuRB;
    
    [SerializeField] GameObject impactoAnim;
   


    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
       // meuRB.velocity = Vector2.up * vel;

    
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Inimigo") && transform.position.y < 4.9f)
        {
        
         

        var ini = collision.GetComponent<InimigoPai>();
        ini.PerdeVida(1);
        
        }

        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().PerdeVida(1);
        }

        Instantiate(impactoAnim, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    

    // Update is called once per frame
    void Update()
    {  
        //Testando Diff


        
    }
}
