using UnityEngine;
using TMPro;

/// <summary>
/// The type of gate
/// </summary>
public enum GateType
{
    MULTIPLY,
    DIVIDE,
    ADD,
    NOOVERRIDE
}

/// <summary>
/// Represents a mathematic gate
/// </summary>
public class Gate : MonoBehaviour
{
    [SerializeField] private GateType Type;
    [SerializeField] private int Value = 2;
    [SerializeField] private TextMeshPro label;
    public bool Activated = false;
    public Animator gateEnterAnim;

    /// <summary>
    /// Initializes the gate and updates its label at spawn
    /// </summary>
    private void Start()
    {
        //SetValue();
    }

    public void SetValue(int overrideValue=0, GateType overrideGateType=GateType.NOOVERRIDE, Material overrideMaterial=null)
    {
        if (overrideGateType != GateType.NOOVERRIDE)
        {
            Type = overrideGateType;
        }

        if (overrideMaterial != null)
        {
            this.GetComponent<Renderer>().material = overrideMaterial;
        }
        switch (Type)
        {
            case GateType.DIVIDE:
                int randDivide = UnityEngine.Random.Range(0, 6);

                switch(randDivide)
                {
                    case 5:
                        Value = 10;
                        break;
                    case 4:
                        Value = 5;
                        break;
                    case 2:
                        Value = 3;
                        break;
                    case 1:
                        Value = 4;
                        break;
                    case 0:
                    default:
                        Value = 2;
                        break;
                }

                if (overrideValue != 0)
                {
                    Value = overrideValue;
                }
                //this.GetComponent<Renderer>().material.color = new Color(255, 78, 0, 128);
            
                label.text = $"÷ {Value}";
                break;
            case GateType.MULTIPLY:
                int randMultiply = UnityEngine.Random.Range(0, 6);

                switch (randMultiply)
                {
                    case 5:
                        Value = 10;
                        break;
                    case 4:
                        Value = 5;
                        break;
                    case 2:
                        Value = 3;
                        break;
                    case 1:
                        Value = 4;
                        break;
                    case 0:
                    default:
                        Value = 2;
                        break;
                }
                if (overrideValue != 0)
                {
                    Value = overrideValue;
                }
                //this.GetComponent<Renderer>().material.color = new Color(0, 165, 255, 128);
                label.text = $"x {Value}";
                break;
            case GateType.ADD:

                int randAdd = UnityEngine.Random.Range(0, 7);

                switch (randAdd)
                {
                    case 6:
                        Value = -10;
                        break;
                    case 5:
                        Value = -5;
                        break;
                    case 4:
                        Value = -2;
                        break;
                    case 2:
                        Value = 8;
                        break;
                    case 1:
                        Value = 5;
                        break;
                    case 0:
                    default:
                        Value = 2;
                        break;
                }
                if (overrideValue != 0)
                {
                    Value = overrideValue;
                }

                if (Value > 0)
                {
                    label.text = $"+ {Value}";
                    //this.GetComponent<Renderer>().material.color = new Color(0, 165, 255, 128);
                }
                else
                {
                    label.text = $"- {Mathf.Abs(Value)}";
                    //this.GetComponent<Renderer>().material.color = new Color(255, 78, 0, 128);
                }
                break;
            default:
                break;
            
        }
    }

    /// <summary>
    /// Handle collision with runners in the crowd
    /// </summary>
    /// <param name="other">The collider collided with</param>
    private void OnTriggerEnter(Collider other)
    {
        if (Activated) return;

        if (other.gameObject.tag == "Player")
        {
            switch(Type)
            {
                case GateType.DIVIDE:
                    PlayerController.Instance.DivideCharacters(Value);
                    break;
                case GateType.MULTIPLY:
                    PlayerController.Instance.MultiplyCharacters(Value);
                    break;
                case GateType.ADD:
                    PlayerController.Instance.AddCharacters(Value);
                    break;
                default:
                    break;
            }

            gateEnterAnim.Play("GateEnter");
            Activated = true;

            if (transform.parent.childCount > 1)
                transform.parent.GetChild(1).GetComponent<Gate>().Activated = true;
        }
    }
}
