using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PassaralhoMovimentoControlador : MonoBehaviour
{
    [Header("Variáveis")]
    public float VelocidadePassaralho = 5f;
    public GameObject TargetMovimento;
    public GameObject Passaralho;
    public bool Habilitado = true;
    [Range(1, 10)]
    public float SensibilidadeTarget = 3;

    private Vector3 Movimento;
    private Rigidbody2D Passaralho_Rb;
    private Rigidbody2D Target_Rb;

    void Start()
    {
        this.Passaralho_Rb = this.GetComponent<Rigidbody2D>();
        this.Target_Rb = TargetMovimento.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Habilitado)
        {
            OlharParaTarget();

            var movimentoGradual = Vector3.Lerp(
                Passaralho.transform.position,
                TargetMovimento.transform.position,
                VelocidadePassaralho * Time.fixedDeltaTime
            );
            this.Passaralho_Rb.MovePosition(new Vector3(0f, movimentoGradual.y, 0f));

            var movimentoTarget = Vector3.Lerp(this.TargetMovimento.transform.position, this.TargetMovimento.transform.position + (this.Movimento / SensibilidadeTarget), 3f);

            Target_Rb.MovePosition(
                new Vector3(TargetMovimento.transform.position.x, movimentoTarget.y, 0f)
            );

            if (Input.touchCount > 0)
            {
                Touch toque = Input.GetTouch(0);

                var posicaoMundo = Camera.main.ScreenToWorldPoint(toque.position);

                Target_Rb.MovePosition(
                new Vector3(TargetMovimento.transform.position.x, posicaoMundo.y, 0f)
                );

            }

        }
    }

    private void OlharParaTarget()
    {
        Vector3 difference = TargetMovimento.transform.position - Passaralho.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Passaralho.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        this.Movimento = new Vector3(v.x, v.y);
    }
}
