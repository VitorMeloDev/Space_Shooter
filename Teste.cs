using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPai : MonoBehaviour
{

    [SerializeField] protected float vel ;
    [SerializeField] protected int vida;
    [SerializeField] protected GameObject explosao;
    [SerializeField] protected float velTiro = -5;

    [SerializeField] protected GameObject meuTiro;
    [SerializeField] protected float esperaTiro;
    [SerializeField] protected Transform posicaoTiro;
    [SerializeField] protected GameObject powerUP;
    [SerializeField] protected int ponto = 10;

    [SerializeField] protected float itemRate = 0.9f;
    [SerializeField] protected AudioClip meuSom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerdeVida(int dano)
    {
        vida -= dano;
        if(vida <=0)
        {
            Destroy(gameObject);
            Instantiate(explosao, transform.position, transform.rotation);
            var gerador = FindObjectOfType<Gerenciador>();
            gerador.DiminuiQuantidade();

           
            gerador.GanhaPontos(ponto);
            

            if(powerUP)
            {
                DropaItem();
            }
        }
    }

    /*private void OnDestroy()
    {
        var gerador = FindObjectOfType<Gerenciador>();
        if(gerador)
        {
          gerador.DiminuiQuantidade();
        }
    }*/


     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Parede"))
        {
          Destroy(gameObject);
         // Instantiate(explosao, transform.position, transform.rotation);
          var gerador = FindObjectOfType<Gerenciador>();
          gerador.DiminuiQuantidade();
          

        
        }
        
    }

     void OnCollisionEnter2D(Collision2D other)
     {
         if(other.gameObject.CompareTag("Player"))
        {
            DropaItem();

           // var gerador = FindObjectOfType<Gerenciador>();
            //gerador.DiminuiQuantidade();
            other.gameObject.GetComponent<PlayerController>().PerdeVida(1);


            Instantiate(explosao, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void DropaItem()
    {

        float chance = Random.Range(0f, 1f);

        if(chance > itemRate)
        {
        GameObject Pu = Instantiate(powerUP, transform.position, transform.rotation);
        Destroy(Pu, 5F);
        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Pu.GetComponent<Rigidbody2D>().velocity = dir;
        }
    
    }
}
