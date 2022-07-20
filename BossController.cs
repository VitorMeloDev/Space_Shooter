
using UnityEngine;
using UnityEngine.UI;

public class BossController : InimigoPai
{

    private Rigidbody2D meuRB;
    [SerializeField] private float velBoss = 0.31f;

     private bool direita = true;
    [SerializeField] private GameObject meuTiro2;
    
    [SerializeField] private Transform posTiro;
    [SerializeField] private Transform posTiro2;
    [SerializeField] private string estado = "estado1";
    private float delayTiro = 1f;
    private float esperaTiro2 = 1.5f;

    [SerializeField] private float esperaEstado = 10f;
    [SerializeField] private Image vidaBossBarra;

    [SerializeField] private int vidaMaxima = 300;

    [SerializeField] private string[] estados;

    
    // Start is called before the first frame update
    void Start()
    {
       meuRB = GetComponent<Rigidbody2D>();
        //esperaTiro = UnityEngine.Random.Range(1f,2.8f);
        vida = vidaMaxima;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        TrocaEstado();
    
        switch(estado)
        {
            case "estado1":
            Estado1();
            
            break;

            case "estado2":
            Estado2();
            break;

            case "estado3":
            Estado3();
            break;

        }
       vidaBossBarra.fillAmount = ((float) vida / (float)vidaMaxima);
    }

    private void AumentaDificuldade()
    {
        if(vida <= vidaMaxima/2)
        {
            delayTiro = 0.7f;
        }
    }

    private void Estado1()
    {
        if (esperaTiro < 0)
        {
            CriaTiro1();
            esperaTiro = delayTiro;

        }
        else
        {
            esperaTiro -= Time.deltaTime;
        }

        BossMovendo();

    }

    private void BossMovendo()
    {
        if (direita)
        {
            meuRB.velocity = new Vector2(velBoss, 0f);
        }
        else
        {
            meuRB.velocity = new Vector2(-velBoss, 0f);
        }

        if (transform.position.x >= 5.7)
        {
            direita = false;
        }
        if (transform.position.x <= -5.7)
        {
            direita = true;
        }
    }

    private void Estado2()
    {
        meuRB.velocity = Vector2.zero;

     
        if(esperaTiro < 0f)
        {
            CriaTiro2();
            esperaTiro = delayTiro / 2;
        }else
        {
            esperaTiro -= Time.deltaTime;
        }
      

        
        
    }

    private void Estado3()
    {
        BossMovendo();

        if(esperaTiro <= 0f)
        {
            CriaTiro1();
            esperaTiro = delayTiro;
        }
        else
        {
            esperaTiro -=Time.deltaTime;
        }
        

        if(esperaTiro2<= 0f)
        {
          CriaTiro2();
          esperaTiro2 = delayTiro;
        }
        else
        {
            esperaTiro2 -= Time.deltaTime;
        }




       
    }

    private void CriaTiro1()
    {
        
          var tiro =Instantiate(meuTiro,posTiro.position, transform.rotation);
          tiro.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-velTiro );

          var tiro2 =Instantiate(meuTiro,posicaoTiro.position, transform.rotation);
          tiro2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,-velTiro );

          AudioSource.PlayClipAtPoint(meuSom, Vector3.zero);
          
        
    }

    private void CriaTiro2()
    {
        var player = FindObjectOfType<PlayerController>();
        if(player)
        {
                var tiro = Instantiate(meuTiro2,posTiro2.position, transform.rotation);
                Vector2 direcao = player.transform.position - tiro.transform.position;
                direcao.Normalize();
                tiro.GetComponent<Rigidbody2D>().velocity = direcao * velTiro ;
                //AudioSource.PlayClipAtPoint(meuSom, Vector3.zero);

                float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
                tiro.transform.rotation = Quaternion.Euler(0f,0f, angulo + 90);
                
           
        }
        
    }

    private void TrocaEstado()
    {
        if(esperaEstado < 0f)
        {
            int indEstado = Random.Range(0,estados.Length);

            estado = estados[indEstado];
            esperaEstado = 10f;
        }
        else
        {
           esperaEstado -= Time.deltaTime;
        }
        
    }

    
}
