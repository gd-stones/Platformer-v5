using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Privacy Policy", fileName = "Policy Data")]
public class PolicyDataSO : ScriptableObject
{
    [SerializeField] [TextArea(2,2)] string _privacyPolicyLink;

    public void OpenPolicyURL()
    {
        string policy = _privacyPolicyLink;
        string protocol = "https://";
        if (policy.Contains(protocol))
        {
            policy = policy.Replace(protocol, "");
        }
        Application.OpenURL($"https://{policy}");
    }
}
