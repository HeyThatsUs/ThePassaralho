using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovimento : MonoBehaviour
{
    public float Velocidade = 5f;
    public GameObject TargetMovimento;
    public GameObject Passaralho;

    private Vector3 Movimento;
    private Rigidbody2D Passaralho_Rb;
    private Rigidbody2D Target_Rb;

    void Start()
    {
            this.Passaralho_Rb = Passaralho.GetComponent<Rigidbody2D>();
            this.Target_Rb = TargetMovimento.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OlharParaTarget();

        var movimentoGradual = Vector3.Lerp(Passaralho.transform.position, TargetMovimento.transform.position, 3f * Time.fixedDeltaTime);
        this.Passaralho_Rb.MovePosition(new Vector3(0f, movimentoGradual.y, 0f));

        var movimentoTarget = this.TargetMovimento.transform.position + this.Movimento;

        Target_Rb.MovePosition(new Vector3 (TargetMovimento.transform.position.x, movimentoTarget.y, 0f) );
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
