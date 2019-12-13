using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class AbilityCooldown : MonoBehaviour
{
    //input
    public string abilityButtonAxisName = "Fire1";
    [SerializeField] private Ability ability;
    [SerializeField] private GameObject weaponHolder;

    public Image darkMask;
    public Text coolDownTextDisplay;

    private Image myButtonImage;
    private AudioSource abilitySource;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    void Start()
    {
        Initialize(ability, weaponHolder);
    }

    public void Initialize(Ability selectedAbility, GameObject weaponHolder)
    {
        ability = selectedAbility;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability.abiltiySprite;
        darkMask.sprite = ability.abiltiySprite;
        coolDownDuration = ability.abilityBaseCoolDown;
        ability.Initialize(weaponHolder);
        AbilityReady();
    }

    public void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete)
        {
            AbilityReady();
            if (Input.GetButtonDown(abilityButtonAxisName))
            {
                ButtonTriggered();
            }
        } else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    private void ButtonTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;

        abilitySource.clip = ability.abilitySound;
        abilitySource.Play();
        ability.TriggerAbility();
    }
}
