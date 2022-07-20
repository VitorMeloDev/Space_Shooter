using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController2 : InimigoPai
{
   private Rigidbody2D meuRB;
 

 

    private bool possoMover = true;

    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
        meuRB.velocity = new Vector2(0f, vel);

        esperaTiro = Random.Range(2.5f,5f);
        
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < 3.5f && possoMover)
        {
           if(transform.position.x < 0f)
           {
            meuRB.velocity = new Vector2(0.61f, vel);

            possoMover = false;
           }
           else
           {
            meuRB.velocity = new Vector2(-0.61f, vel);               
                   
            possoMover = false;   
           }
        }
        
       bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;
       
       if(visivel == true)
       {
                var player = FindObjectOfType<PlayerController>();
                if(player)
                {
                esperaTiro -= Time.deltaTime;
                    if(esperaTiro <= 0)
                            {
                            var tiro = Instantiate(meuTiro,posicaoTiro.position, transform.rotation);
                            Vector2 direcao = player.transform.position - tiro.transform.position;
                            direcao.Normalize();
                            tiro.GetComponent<Rigidbody2D>().velocity = direcao * velTiro ;
                            AudioSource.PlayClipAtPoint(meuSom, Vector3.zero);

                            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                            tiro.transform.rotation = Quaternion.Euler(0f,0f, angulo + 90);
                            
                            esperaTiro = Random.Range(2.5f,5f);
                            }
                }
        }
        
    }

    
}
