using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hareketHizi = 5f; // Hareket hýzý
    public float bakmaHizi = 10f; // Dönüþ hýzý
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Yumuþak hareket için Rigidbody'nin doðru þekilde ayarlandýðýndan emin olun
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        // Hareket giriþini iþle
        float yatayHareket = Input.GetAxis("Horizontal");
        float dikeyHareket = Input.GetAxis("Vertical");
        Vector3 hareket = new Vector3(yatayHareket, 0f, dikeyHareket).normalized;

        // Hareket vektörünü hesapla ve karakteri hareket ettir
        Vector3 hareketVector = transform.TransformDirection(hareket) * hareketHizi * Time.deltaTime;
        rb.MovePosition(rb.position + hareketVector);

        // Karakterin gitmekte olduðu yöne dönmesini saðla
        if (hareket != Vector3.zero)
        {
            Quaternion yeniRotasyon = Quaternion.LookRotation(hareket);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, yeniRotasyon, bakmaHizi * Time.deltaTime));
        }

        // Yürüyor mu kontrolü
        bool isWalking = yatayHareket != 0f || dikeyHareket != 0f;
        animator.SetBool("isWalking", isWalking);
    }
}
