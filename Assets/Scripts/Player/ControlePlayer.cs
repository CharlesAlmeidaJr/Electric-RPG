using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlePlayer : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Animator animator;

    public float velocidade = 5f;
    public Vector2 direcao;
    private float raio = 1f;
    private float angulo = 60f;
    private LayerMask interacaoLayer;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        interacaoLayer = LayerMask.GetMask("interacaoLayer");
    }

    void FixedUpdate(){
        if(!GameManager.gameManager.pausado){
            Mover();
        }
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
            //transform.rotation = Quaternion.Euler(0, 0, Input.GetAxisRaw("Horizontal") * (-90) + Input.GetAxisRaw("Vertical") * 180);
        
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
            for (int i = 0; i < verificaInteragiveis.Length; i++){
                Vector2 direcaoInteragivel = (verificaInteragiveis[i].transform.position - transform.position).normalized;

                if(Vector2.Angle(direcao, direcaoInteragivel) < angulo/2){
                    verificaInteragiveis[i].SendMessage("Interacao");
                    break;
                }
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, raio);

        Vector3 angulo1 = DirecaoDoAngulo(Vector2.Angle(direcao, transform.up)*(GetComponent<SpriteRenderer>().flipX?-1:1), -angulo/2);
        Vector3 angulo2 = DirecaoDoAngulo(Vector2.Angle(direcao, transform.up)*(GetComponent<SpriteRenderer>().flipX?-1:1), angulo/2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angulo1 * raio);
        Gizmos.DrawLine(transform.position, transform.position + angulo2 * raio);
    }

    Vector2 DirecaoDoAngulo(float eulerY, float anguloEmGraus){
        anguloEmGraus += eulerY;
        return new Vector2(Mathf.Sin(anguloEmGraus*Mathf.Deg2Rad), Mathf.Cos(anguloEmGraus*Mathf.Deg2Rad));
    }

}
