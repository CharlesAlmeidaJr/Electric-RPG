using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlePlayer : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;

    private float velocidade {set; get;} = 5f;
    public Vector2 direcao;
    private float raio {set; get;} = 1f;
    private float angulo {set; get;} = 20f;
    private LayerMask interacaoLayer;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        interacaoLayer = LayerMask.GetMask("interacaoLayer");
    }

    void FixedUpdate(){
        Mover();
    }

    void Update()
    {
        AtualizarPosicaoInteracao();
        Animacao();

        if(Input.GetKeyDown(KeyCode.Z)){
            Interagir();
        }        
    }

    void Mover(){
        rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * velocidade, Input.GetAxisRaw("Vertical") * velocidade);
    }

    void AtualizarPosicaoInteracao(){
        
        if(Input.GetAxisRaw("Horizontal") != 0 ^ Input.GetAxisRaw("Vertical") != 0){
            direcao = (transform.up * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));            
        
            if(direcao.x < 0){
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else{
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

    }
    
    void Animacao(){
        animator.SetBool("andando", Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);
        animator.SetBool("frente", direcao.y == -1);
        animator.SetBool("costas", direcao.y == 1);
        animator.SetBool("lado", direcao.x != 0);
    }

    void Interagir(){
        Collider2D[] verificaInteragiveis = Physics2D.OverlapCircleAll(transform.position, raio, interacaoLayer);

        if(verificaInteragiveis.Length > 0){

            Vector2 direcaoInteragivel = (verificaInteragiveis[0].transform.position - transform.position).normalized;

            if(Vector2.Angle(direcao, direcaoInteragivel) == angulo/2){

            }
        }
    }

}
