using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gerenciador : MonoBehaviour
{
    [SerializeField] private GameObject[] inimigos;
    [SerializeField] private int pontos = 0;
    [SerializeField] private int baseLevel = 100;
    [SerializeField] private int level;
    private float esperaInimigo = 0f;
    [SerializeField] private int quantidadeIni = 0;
    [SerializeField] private GameObject bossAnim;
    private bool animBoss = false;

    [SerializeField] private float tempoEspera = 5f;
    [SerializeField] private Text pontosQtd;

    [SerializeField]private AudioClip musicaBoss;
    [SerializeField] private AudioSource musicaJogo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(level < 10)
        {
         GeraInimigos();
        }
        else
        {
         BossAparece();
        }

      pontosQtd.text = pontos.ToString();
        
    }

    public void GanhaPontos(int pontos)
    {
        this.pontos += pontos * level ;

        if(this.pontos > baseLevel * level)
        {
            level++;

            baseLevel *= 2;
        }

    }

    public void DiminuiQuantidade()
    {
        quantidadeIni--;
    }

    private bool ChecaPosicao(Vector3 posicao, Vector3 size)
    {
        Collider2D hit = Physics2D.OverlapBox(posicao, size, 0f);
        
        if(hit !=null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void BossAparece()
    {
        if(quantidadeIni <= 0 && tempoEspera > 0)
        {
           tempoEspera -= Time.deltaTime;

        }
        if(!animBoss && tempoEspera <= 0)
        {
            Instantiate(bossAnim, Vector3.zero, transform.rotation);
            animBoss = true;

            musicaJogo.clip = musicaBoss;
            musicaJogo.Play();


        }
        

    }

    private void GeraInimigos()
    {
        
        
             if(esperaInimigo > 0 && quantidadeIni <= 0)
             {
              esperaInimigo -= Time.deltaTime;
             }

                    if(esperaInimigo <= 0f && quantidadeIni <= 0)
                    {

                                int quantidade = level * 4;

                                int tentativas = 0;
                                

                                while (quantidadeIni < quantidade)
                                {

                                    tentativas++;
                                    if(tentativas > 200)
                                    {
                                        break;
                                    }


                                
                                


                                            GameObject inimigoCriado;

                                            float chance = Random.Range(0f, level);
                                            if(chance > 2f)
                                            {
                                                inimigoCriado = inimigos[1];
                                            }
                                            else
                                            {
                                                inimigoCriado = inimigos[0];
                                            }

                                        
                                            Vector3 posicao = new Vector3(Random.Range(-8.08f, 8.24f),Random.Range(6f, 15f), 0f);  
                                            
                                            bool colisao = ChecaPosicao(posicao, inimigoCriado.transform.localScale);
                                            
                                            if(colisao)
                                            {
                                                continue;
                                            }
                                            Instantiate(inimigoCriado, posicao, transform.rotation);
                                            
                                            quantidadeIni ++;
                                            
                                            esperaInimigo = tempoEspera;
                    
                                }
                    }
        
    }
}     

