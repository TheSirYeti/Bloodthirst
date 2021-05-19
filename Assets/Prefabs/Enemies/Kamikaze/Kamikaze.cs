using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    public GameObject player;
    float privateRotation;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(ParabolicMovement());
    }

    private void Update()
    {
        
    }

    IEnumerator ParabolicMovement()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(0f);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, player.transform.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * privateRotation * Mathf.Deg2Rad) / 2);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(privateRotation * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(privateRotation * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (2 * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
