using UnityEngine;

public class TiltController : MonoBehaviour
{
    public float tiltSpeed = 60f;          // Velocidad de inclinación
    public float maxAngle = 45f;           // Límite de inclinación
    public float smoothness = 5f;          // Qué tan suave es el movimiento

    private Vector3 targetRotation;

    void Start()
    {
        targetRotation = transform.eulerAngles;
    }

    void Update()
    {
        float tiltX = Input.GetKey(KeyCode.W) ? -1 :
                      Input.GetKey(KeyCode.S) ? 1 : 0;

        float tiltZ = Input.GetKey(KeyCode.A) ? 1 :
                      Input.GetKey(KeyCode.D) ? -1 : 0;

        targetRotation.x += tiltX * tiltSpeed * Time.deltaTime;
        targetRotation.z += tiltZ * tiltSpeed * Time.deltaTime;

        // Limitar rotación en X y Z
        targetRotation.x = ClampAngle(targetRotation.x, -maxAngle, maxAngle);
        targetRotation.z = ClampAngle(targetRotation.z, -maxAngle, maxAngle);

        // Aplicar rotación suavizada
        Quaternion targetQuat = Quaternion.Euler(targetRotation.x, 0f, targetRotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuat, Time.deltaTime * smoothness);
    }

    float ClampAngle(float angle, float min, float max)
    {
        angle = NormalizeAngle(angle);
        return Mathf.Clamp(angle, min, max);
    }

    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}
