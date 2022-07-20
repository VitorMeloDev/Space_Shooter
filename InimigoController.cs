using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : InimigoPai
{
    private Rigidbody2D meuRB;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        meuRB.velocity = new Vector2(0f, vel);

        esperaTiro = Random.Range(1f,2.8f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
       bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;
       
       if(visivel == true)
       {
        esperaTiro -= Time.deltaTime;
        if(esperaTiro <= 0)
        {
          AudioSource.PlayClipAtPoint(meuSom, Vector3.zero);
          var tiro =Instantiate(meuTiro,posicaoTiro.position, transform.rotation);
          tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,velTiro );
           esperaTiro = Random.Range(1f,2.8f);
        }
        }
        
    }



    
}
