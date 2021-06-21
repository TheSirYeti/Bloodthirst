using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathParabola : MonoBehaviour
{
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;

    public float minimumDistance;
    bool attack = false;
    bool attacking = false;
    bool ended = false;
    [SerializeField]Vector3 finalTarget;
    public ParticleSystem explosion;

    public Collider collider;

    void Awake()
    {
        explosion.Stop();
        myTransform = transform;
    }

    void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, Target.position) > minimumDistance)
        {
            Target = GameObject.FindWithTag("Player").transform;
        }
        else
        {
            attack = true;
        }

        if (attack && !attacking)
        {
            finalTarget = Target.position;
            StartCoroutine(ParabolicMovement());
            attacking = true;
        }

        if (ended)
        {   
            StartCoroutine(Explode());
        }
    }

    IEnumerator ParabolicMovement()
    {

        // Short delay added before Projectile is thrown
        SoundManager.instance.PlaySound(SoundID.KAMIKAZE_BUILDUP);
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.PlaySound(SoundID.KAMIKAZE);
        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        SoundManager.instance.StopSound(SoundID.KAMIKAZE);
        SoundManager.instance.PlaySound(SoundID.EXPLOSION1);
        ended = true;
    }

    IEnumerator Explode()
    {
        explosion.Play();
        collider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
