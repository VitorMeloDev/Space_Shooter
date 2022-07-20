using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
   private Rigidbody2D meuRB;
   [SerializeField] private float velocidade = 4f;
   [SerializeField] private GameObject tiro;

   [SerializeField] private GameObject tiro2;

   [SerializeField] private GameObject shield;

   [SerializeField] private Transform tiroSaida;

   [SerializeField] private float velocidadeTiro;
   
   [SerializeField] private int vida = 3;

   [SerializeField] private GameObject explosao;

   [SerializeField] private float xLimite;
 
   [SerializeField] private float yLimite;

   [SerializeField] private int tiroLevel;

   private GameObject escudoAtivo;

   [SerializeField] private float quanEscudos = 3f;

   private float escudoTimer;
   [SerializeField] private Text vidaTexto;

   [SerializeField] private Text escudoQtd;
   [SerializeField] private AudioClip meuSom;
   [SerializeField] private AudioClip morteSom;
   [SerializeField] private AudioClip powerSom;
   [SerializeField] private AudioClip escudoSom;
   [SerializeField] private AudioClip escudoSom2;

   


    // Start is called before the first frame update
    void Start()
    {
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movendo();
        EscudoPlayer();

        TiroPlayer();

        

       

        //vidaTexto.text = vida.ToString();

        escudoQtd.text = quanEscudos.ToString();

    }

    private void Movendo()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 minhaVelocidade = new Vector2(horizontal, vertical) * velocidade;
        minhaVelocidade.Normalize();
        meuRB.velocity = minhaVelocidade * velocidade;


        float meuX = Mathf.Clamp(transform.position.x, -xLimite,xLimite);
        float meuY = Mathf.Clamp(transform.position.y, -4.2f,yLimite);

        transform.position = new Vector3(meuX, meuY, transform.position.z);
        
            
        
    }

    private void EscudoPlayer()
    {

        

        if(Input.GetButtonDown("Shield"))
        {
            if(!escudoAtivo && quanEscudos > 0f)
            {
                AudioSource.PlayClipAtPoint(escudoSom,Vector3.zero);
                escudoAtivo =  Instantiate(shield, transform.position, transform.rotation);
                quanEscudos--;
            }
        } 

        if(Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if (!escudoAtivo && quanEscudos > 0f)
            {
                AudioSource.PlayClipAtPoint(escudoSom, Vector3.zero);
                escudoAtivo = Instantiate(shield, transform.position, transform.rotation);
                quanEscudos--;
            }
        }

        if(escudoAtivo)
        {
                escudoAtivo.transform.position = transform.position;

                escudoTimer += Time.deltaTime;

                if(escudoTimer > 12.1f)
                {
                    AudioSource.PlayClipAtPoint(escudoSom2, Vector3.zero);
                    Destroy(escudoAtivo);

                    escudoTimer = 0f;
                }
        }
    }

    private void TiroPlayer()
    {
        Vector3 posicao =new Vector3(transform.position.x - 0.43f, transform.position.y + 0.1f, 0f);
        Vector3 posicao2 =new Vector3(transform.position.x + 0.41f, transform.position.y + 0.1f, 0f);
        if (Input.GetButtonDown("Fire1"))
        {
            switch(tiroLevel)
            { 
                case 1:
                CriaTiro(tiro, tiroSaida.position);

                break;

               case 2:
               
               CriaTiro(tiro2, posicao);
              
               CriaTiro(tiro2, posicao2);
               break;

               case 3:
               CriaTiro(tiro, tiroSaida.position);
               CriaTiro(tiro2, posicao);
               CriaTiro(tiro2, posicao2);
               break;
            }
        }
        
    }

    private void CriaTiro(GameObject tiroCriado, Vector3 posicao)
    {

        AudioSource.PlayClipAtPoint(meuSom, Vector3.zero);
        GameObject Meutiro = Instantiate(tiroCriado, posicao, transform.rotation);
        Meutiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, velocidadeTiro); 
        

    }

    public void PerdeVida(int dano)
    {
        vida = vida - dano;
        vidaTexto.text = vida.ToString();
      if(vida <= 0)
      {

          Instantiate(explosao, transform.position, transform.rotation);
          Destroy(gameObject);

          var gameManager = FindObjectOfType<GameManager>();
          if(gameManager)
          {
          gameManager.CarregarCenaInicio();
          }
      }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("power_up"))
        {
            if(tiroLevel < 3)
            {
                AudioSource.PlayClipAtPoint(powerSom, Vector3.zero);
               tiroLevel++;
            }

            Destroy(collision.gameObject);
        }
    }
}
