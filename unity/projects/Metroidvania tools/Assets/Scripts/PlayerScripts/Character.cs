using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MetroidvaniaTools
{
    public class Character : MonoBehaviour
    {
        [HideInInspector]
        public bool isFacingLeft;
        [HideInInspector]
        public bool isGrounded;

        protected Collider2D col; //declara hitbox de item
        protected Rigidbody2D rb;//declara que no puede ser atravesado
        protected Animator anim;//permitira activar las animaciones

        private Vector2 facingLeft;//Variable para que veo para el lado izquierdo
        
        // Start is called before the first frame update
        void Start()
        {
            Initialization();//lo utilizo para iniciarlizar todo sobre los personajes y no hacerlo una y otra vez
        }

        protected virtual void Initialization()
        {
            col = GetComponent<Collider2D>();//toma los hitbox de los cuerpos rigidos
            rb = GetComponent<Rigidbody2D>();//transforma lo utilizado en cuerpo rigido para que no atraviese objetos
            anim = GetComponent<Animator>();
            facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        protected virtual void Flip()//permite que el personaje voltee o no hacia un lado
        {
            if(isFacingLeft)
            {
                transform.localScale = facingLeft;
            }
            else
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        protected virtual bool CollisionCheck(Vector2 direction, float distance, LayerMask collision) //Layer mask refiere a lo que rodea a un determinado sprite
        {
            RaycastHit2D[] hits = new RaycastHit2D[10];
            int numHits = col.Cast(direction, hits, distance);
            for(int i = 0; i < numHits; i++)
            {
                if((1 <<hits[i].collider.gameObject.layer & collision) != 0)
                {
                    return true;
                }
            }
            return false;
        }
        protected virtual bool Falling(float velocity)//Este metodo revisa si el personaje cae muy rapido o no
        {
            if (!isGrounded && rb.velocity.y < velocity)
            {
                return true;
            }
            else
                return false;
        }
    }

}