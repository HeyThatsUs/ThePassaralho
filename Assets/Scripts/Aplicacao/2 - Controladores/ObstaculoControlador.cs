using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Aplicacao._2___Controladores
{
    public class ObstaculoControlador : MonoBehaviour
    {
        public int ValorDano = 50;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Missil"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
