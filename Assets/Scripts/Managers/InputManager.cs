using UnityEngine;

//when using InputActions, uses singleton to access inputMap so multiple objects don't create an inputMap variable
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public InputMap inputMap;

    //initialization of inputMap
    private void OnEnable()
    {
        inputMap.Enable();
    }

    //unassignment of inputMap
    private void OnDisable()
    {
        inputMap.Disable();
    }
    
    private void Awake()
    {
        //creates new inputMap instance
        inputMap = new InputMap();
        
        //creates singleton of InputManager
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
