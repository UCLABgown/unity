using UnityEngine;

/*
 * RayReactor�� ����ڰ� �߻��ϴ� �̺�Ʈ(�ü�, ������ ���)�� �޴� Ŭ�����Դϴ�.
 * RayReactor receives event(eye gazing, hand holding) happened from user.
 */
[RequireComponent(typeof(Collider))]
public class RayReactor : MonoBehaviour
{
    [Tooltip("Object name used to record when user is focusing on object.")]
    public string objectName = "";

    [SerializeField]
    [Tooltip("Show effect when user is focusing on object.")]
    public bool showEffect = false;

    [SerializeField]
    [Tooltip("Effect color when user is focusing on object.")]
    private Color effectColor = Color.white;

    // Reacted�� ���� ������ ������Ʈ�� focusing�ϰ� ������ ��Ÿ���ϴ�.
    // Reacted means user is focusing.
    [HideInInspector]
    public bool isEyeReacted { get; set; }


    // ����ڰ� ������Ʈ�� �ٶ󺼰�� �����.
    // Execute when user is gazing an object.
    private void EyeReact()
    {
        if (showEffect)
            ShowEffect();
    }

    // ����ڰ� ������Ʈ�� �ٶ��� ������� �����.
    // Execute when user is not gazing an object.
    private void EyeHalt()
    {
        if (showEffect)
            HideEffect();
    }

    private void ShowEffect()
    {
        GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", effectColor);
    }

    private void HideEffect()
    {
        GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }
/*
    private void FixedUpdate()
    {
        // User eye focusing check
        if (isEyeReacted)
            EyeReact();
        else
            EyeHalt();
    }
    */
}
