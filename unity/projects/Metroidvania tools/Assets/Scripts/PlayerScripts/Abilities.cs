using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class Abilities : Character //hereda de la clase character vy tiene acceso a lo protegido
    {
        protected Character character;
        protected override void Initialization()//esto agregara mas funciones a initialization desde fuera
        {
            base.Initialization();
            character = GetComponent<Character>();
        }
    }
}