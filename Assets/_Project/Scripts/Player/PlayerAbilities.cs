using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private MonoBehaviour ability1; // must implement IAbility
    [SerializeField] private MonoBehaviour ability2; // must implement IAbility

    private float _cooldown1;
    private float _cooldown2;

    private void Update()
    {
        _cooldown1 -= Time.deltaTime;
        _cooldown2 -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q)) TryUseAbility1();
        if (Input.GetKeyDown(KeyCode.F)) TryUseAbility2();
    }

    private void TryUseAbility1()
    {
        if (_cooldown1 > 0f) return;
        IAbility a = ability1 as IAbility;
        if (a == null) return;
        a.Activate(gameObject);
        _cooldown1 = a.Cooldown;
    }

    private void TryUseAbility2()
    {
        if (_cooldown2 > 0f) return;
        IAbility a = ability2 as IAbility;
        if (a == null) return;
        a.Activate(gameObject);
        _cooldown2 = a.Cooldown;
    }
}