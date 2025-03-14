using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float desaceleracao = 1f; 

    // inicializa a bola randomicamente para esquerda ou direita
    void GoBall(){
        float rand = Random.Range(0, 2);
        if(rand < 1){
            rb2d.AddForce(new Vector2(20, -15));
        } else {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }

    void Start () {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
        Invoke("GoBall", 2);    // Chama a função GoBall após 2 segundos
    }

    // Reinicializa a posição e velocidade da bola
    void ResetBall(){
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Reinicializa o jogo
    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }

    // Determina o comportamento da bola nas colisões com os Players (raquetes)
    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player")){
            Vector2 vel;
            vel.x = ((rb2d.velocity.x) + (coll.collider.attachedRigidbody.velocity.x)) + 5;
            vel.y = ((rb2d.velocity.y) + (coll.collider.attachedRigidbody.velocity.y)) + 5;
            rb2d.velocity = vel;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (rb2d.velocity.magnitude > 0.1f) // Evita valores muito pequenos
        {
            rb2d.velocity -= rb2d.velocity.normalized * desaceleracao * Time.fixedDeltaTime;
        }
        else
        {
            rb2d.velocity = Vector2.zero; // Para completamente quando for bem pequeno
        }
    }
}