using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthListener : MonoBehaviour
{
    [SerializeField] private Text healthText = null;

    [SerializeField] private PlayerBehaviour playerBehaviour = null;
    private int lastValue = 0;

    //private void OnEnable()
    //{
    //    if(playerBehaviour != null) { playerBehaviour.OnPlayerHealthChanged += UpdateTextField; }
    //}

    public void SetupListener(Text playerHealthText)
    {
        healthText = playerHealthText;
        playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
        if (playerBehaviour != null) { playerBehaviour.OnPlayerHealthChanged += UpdateTextField; }
    }

    private void UpdateTextField(int playerHealth) { healthText.text = playerHealth.ToString(); }

}
