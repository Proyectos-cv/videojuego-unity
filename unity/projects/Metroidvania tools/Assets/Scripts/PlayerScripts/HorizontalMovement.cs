using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class HorizontalMovement : Abilities
    {
        [SerializeField] protected float timeTillMaxSpeed;//el serialize sirve para que una protected salga en el inspector controla tiempo para la maxima velocidad
        [SerializeField] protected float maxSpeed;//determina la maxima velocidad
        [SerializeField] protected float sprintMultplier;//
        private float acceleration;
        private float currentSpeed;
        private float horizontalInput;
        private float runTime;

        protected override void Initialization()
        {
            base.Initialization();
        }

        // Update is called once per frame
        protected virtual void Update()//diferencia principal entre update y fixedupdate es que  update refresca cada frame, mientras que fixed update es mas sensible a cambios fisicos externos
        {
            MovementPressed();
            SprintingHeld();
        }

        protected virtual bool MovementPressed()//en esta parte se declara cuando estas pulsando la tecla de movimiento a o d
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                return true;
            }
            else
            {
                return false;
            }
        }
        protected virtual bool SprintingHeld()
        {
            if (Input.GetKey(KeyCode.LeftShift))//determina si la tecla shift izquierda esta presionada
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected virtual void FixedUpdate()
        {
            Movement();//llama a la funcion del movimiento
        }
        protected virtual void Movement()
        {
            if (MovementPressed())//este if declara el movimiento asi como llama a una funcion que limita el movimiento
            {
                anim.SetBool("Walking", true);
                acceleration = maxSpeed / timeTillMaxSpeed;
                runTime += Time.deltaTime;
                currentSpeed = horizontalInput * acceleration * runTime;
                CheckDirection();                
            }
            else
            {
                anim.SetBool("Walking", false);
                acceleration = 0;
                runTime = 0;
                currentSpeed = 0;
            }
            SpeedMultiplier();
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);//este comando hace que el rb se mueva
        }
        protected virtual void CheckDirection()//verifica hacia que lado te estas moviendo
        {
            if (currentSpeed > 0)//controla hacia que lado se mueve
            {
                if(character.isFacingLeft)//determina si gira o no el pj
                {
                    character.isFacingLeft = false;
                    Flip();
                }
                if (currentSpeed > maxSpeed)//limita la velocidad hacia la derecha
                {
                    currentSpeed = maxSpeed;
                }
            }
            if (currentSpeed < 0)//controla hacia que lado se mueve
            {
                if (!character.isFacingLeft)//determina si gira o no el pj
                {
                    character.isFacingLeft = true;
                    Flip();
                }
                if (currentSpeed < -maxSpeed)//limita ala velocidad a la izquierda
                {
                    currentSpeed = -maxSpeed;
                }
            }
        }
        protected virtual void SpeedMultiplier()
        {
            if(SprintingHeld())
            {
                currentSpeed *= sprintMultplier;
            }
        }

    }
}
