using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class Jump : Abilities
    {
        [SerializeField] protected bool limitAirJumps;
        [SerializeField] protected int maxJumps; //indica cuantos saltos continuos puedes hacer
        [SerializeField] protected float jumpForce; //indica la fuerza con que saltara
        [SerializeField] protected float holdForce; //indica la fuerza con que saltara si mantines pulsado el boton
        [SerializeField] protected float buttonHoldTime; //Marca cuanto tiempo se pulso la tecla
        [SerializeField] protected float distanceToCollider;
        [SerializeField] protected float maxJumpSpeed;//limitara que tan rapido subes despues de saltar
        [SerializeField] protected float maxFallSpeed;//limitara que tan rapido caes despues de saltar
        [SerializeField] protected float acceptedFallSpeed;//limitara que tan rapido caes despues de saltar
        [SerializeField] protected LayerMask collisionLayer;

        private bool isJumping; //nos dice si es posible o no saltar en la posicion actual
        private float jumpCountDown; // reinicia el tiempo que se ha pulsado la tecla
        private int numberOfJumpsLeft;//indica cuantos saltos quedan

        protected override void Initialization()
        {
            base.Initialization();
            numberOfJumpsLeft = maxJumps;
            jumpCountDown = buttonHoldTime;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            JumpPressed();
            JumpHeld();
        }
        protected virtual bool JumpPressed()
        {
            if (Input.GetKeyDown(KeyCode.Space)) //verifica por frame si el espacio esta presionado
            {
                if (!character.isGrounded && numberOfJumpsLeft == maxJumps) //verifica si el personaje esta en el suelo y si aun tiene saltos
                {
                    isJumping = false;
                    return false;
                }
                if (limitAirJumps && Falling(acceptedFallSpeed))//prohibe saltar si la caida es mayor a cierto valor
                {
                    isJumping = false;
                    return false;
                }
                numberOfJumpsLeft--;//disminuye los saltos maximos
                if (numberOfJumpsLeft >= 0) //si cumplimos esta condicion podemos seguir saltando
                {
                    jumpCountDown = buttonHoldTime; //reinicia la variable para iniciar de nuevo
                    isJumping = true;
                }
                return true;

            }
            else
            {
                return false;
            }
        }
        protected virtual bool JumpHeld()
        {
            if (Input.GetKey(KeyCode.Space)) //verifica por frame si el espacio esta presionado
            {
                return true;
            }
            else
                return false;
        }
        protected virtual void FixedUpdate()
        {
            IsJumping();
            GroundCheck();
        }

        protected virtual void IsJumping()//funciona para saber si el personaje esta saltando no permitir otro salto
        {

            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);//todos los saltos seran tan potentes como el primero
                rb.AddForce(Vector2.up * jumpForce);
                AdittionalAir();
            }
            if (rb.velocity.y > maxJumpSpeed)// evita moonjumps
            {
                rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
            }
        }
        protected virtual void AdittionalAir()
        {
            if (JumpHeld())
            {
                jumpCountDown -= Time.deltaTime; //va reduciendo el tiempo a lo pulsado de la tecla
                if (jumpCountDown <= 0)
                {
                    jumpCountDown = 0;
                    isJumping = false;
                }
                else
                    rb.AddForce(Vector2.up * holdForce); //multiplica el salto por el efecto del pulsado
            }
            else
                isJumping = false;
        }
        protected virtual void GroundCheck() //verifica si el personaje esta en el piso
        {
            if(CollisionCheck(Vector2.down,distanceToCollider, collisionLayer) && !isJumping)//se pone el isjumping para evitar que se reinicie el contador de saltos
            {
                character.isGrounded = true;
                numberOfJumpsLeft = maxJumps;
            }
            else
            {
                character.isGrounded = false;//como ya se permite el doble salto se requiere una verificacion de que aun quedan salto o ya cae                
                if(Falling(0) && rb.velocity.y < maxFallSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
                }
            }
        }
    }
}