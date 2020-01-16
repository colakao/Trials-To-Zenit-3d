using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace playerScripts
{
    public enum AttackType { heavy = 0, light = 1 };
    public class CharacterCombo : MonoBehaviour
    {
        PlayerInput inputActions;
        Animator playerAnim;

        [Header("Attacks")]
        public Attack lightAttack;
        public Attack heavyAttack;

        [Header("Combos")]
        public List<Combo> combos;
        public float timeBetweenAttacks = 0.3f;

        Attack currentAttack = null;
        private float timer = 0f;
        private float leeway = 0f;

        private ComboInput input = null;
        private ComboInput lastInput = null;

        bool skip = false;

        List<int> currentCombos = new List<int>();

        private void Awake()
        {
            playerAnim = transform.GetChild(0).GetComponent<Animator>();
            inputActions = new PlayerInput();
            PrimeCombos();
        }

        void PrimeCombos()
        {
            for (int i = 0; i < combos.Count; i++)
            {
                Combo c = combos[i];
                c.onInputted.AddListener(() =>
                {
                    //Call attack function with the combo's attack
                    skip = true;
                    Attack(c.comboAttack);
                    ResetCombos();
                });
            }
        }

        private void Update()
        {
            if (currentAttack != null)
            {
                if (timer > 0)
                    timer -= Time.deltaTime;
                else
                    currentAttack = null;

                return;
            }

            if (currentCombos.Count > 0)
            {
                leeway += Time.deltaTime;
                if (leeway >= timeBetweenAttacks)
                {
                    if (lastInput != null)
                    {
                        Attack(getAttackFromType(lastInput.type));
                        lastInput = null;
                    }
                    ResetCombos();
                }
            }
            else
                leeway = 0f;

            if (input == null)
                return;
            lastInput = input;

            List<int> remove = new List<int>();
            for (int i = 0; i < currentCombos.Count; i++)
            {
                Combo c = combos[currentCombos[i]];
                if (c.continueCombo(input))
                    leeway = 0f;
                else
                    remove.Add(i);
            }

            if (skip)
            {
                skip = false;
                return;
            }

            for (int i = 0; i < combos.Count; i++)
            {
                if (currentCombos.Contains(i))
                    continue;
                if (combos[i].continueCombo(input))
                {
                    currentCombos.Add(i);
                    leeway = 0f;
                }
            }

            foreach (int i in remove)
            {
                currentCombos.RemoveAt(i);
            }

            if (currentCombos.Count <= 0)
                Attack(getAttackFromType(input.type));
        }

        void ResetCombos()
        {
            leeway = 0f;
            for (int i = 0; i < currentCombos.Count; i++)
            {
                Combo c = combos[currentCombos[i]];
                c.ResetCombo();
            }

            currentCombos.Clear();
        }

        void Attack(Attack atk)
        {
            currentAttack = atk;
            timer = atk.animationLength;
            playerAnim.Play(atk.animationName, -1, 0);

        }

        Attack getAttackFromType(AttackType t)
        {

            if (t == AttackType.heavy)
                return heavyAttack;
            if (t == AttackType.light)
                return lightAttack;
            return null;
        }

        private void OnEnable()
        {
            inputActions.Enable();
            inputActions.Player.LightAttack.performed += LightAttack;
            inputActions.Player.HeavyAttack.performed += HeavyAttack;
            inputActions.Player.Ability1.performed += FirstAbility;
            inputActions.Player.Ability2.performed += SecondAbility;
            inputActions.Player.Ability3.performed += ThirdAbility;
        }

        private void OnDisable()
        {
            inputActions.Disable();
            inputActions.Player.LightAttack.canceled -= LightAttack;
            inputActions.Player.HeavyAttack.canceled -= HeavyAttack;
            inputActions.Player.Ability1.canceled -= FirstAbility;
            inputActions.Player.Ability2.canceled -= SecondAbility;
            inputActions.Player.Ability3.canceled -= ThirdAbility;
        }

        private void LightAttack(InputAction.CallbackContext context)
        {
            input = new ComboInput(AttackType.light);
            playerAnim.SetTrigger("lightAttack");
            Debug.Log("Light Attack");
        }

        private void HeavyAttack(InputAction.CallbackContext context)
        {
            input = new ComboInput(AttackType.heavy);
            playerAnim.SetTrigger("heavyAttack");
            Debug.Log("Heavy Attack");
        }

        private void FirstAbility(InputAction.CallbackContext context)
        {
            Debug.Log("First Ability");
        }

        private void SecondAbility(InputAction.CallbackContext context)
        {
            Debug.Log("Second Ability");
        }

        private void ThirdAbility(InputAction.CallbackContext context)
        {
            Debug.Log("Third Ability");
        }
    }

    [System.Serializable]
    public class Attack
    {
        public string animationName;
        public float animationLength;
    }

    [System.Serializable]
    public class ComboInput
    {
        public AttackType type;
        //Movement Combos
        public ComboInput(AttackType t)
        {
            type = t;
        }
        public bool isSameAs(ComboInput test)
        {
            return (type == test.type);
        }
    }

    [System.Serializable]
    public class Combo
    {
        public string name;
        public List<ComboInput> inputs;
        public Attack comboAttack;
        public UnityEvent onInputted;
        int currentInput = 0;

        public bool continueCombo(ComboInput i)
        {
            if (inputs[currentInput].isSameAs(i))
            {
                currentInput++;
                if (currentInput >= inputs.Count)
                {
                    onInputted.Invoke();
                    currentInput = 0;
                }
                return true;
            }
            else
            {
                currentInput = 0;
                return false;
            }
        }

        public ComboInput currentComboInput()
        {
            if (currentInput >= inputs.Count)
                return null;
            return inputs[currentInput];
        }

        public void ResetCombo()
        {
            currentInput = 0;
        }
    }
}


