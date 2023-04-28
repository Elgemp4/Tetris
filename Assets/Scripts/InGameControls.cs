//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/InGameControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InGameControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InGameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InGameControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""2faf9d8f-d311-47a7-aac7-6432afc6ea9a"",
            ""actions"": [
                {
                    ""name"": ""RotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""af43703e-d1f6-48e9-aab4-6ac77306ed9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""d9ee5070-32aa-47e7-93b5-44b9a6715452"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""49c2553f-84eb-49a9-99fe-9dfc158d984d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""e3fc6ac3-9ebc-478e-accc-36c5ca88438b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HardDrop"",
                    ""type"": ""Button"",
                    ""id"": ""ecbfb980-a6e2-4bd6-aba6-fb07a3e0035b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SoftDrop"",
                    ""type"": ""Button"",
                    ""id"": ""4dc91c85-935e-4382-b00f-710cb0412061"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""Button"",
                    ""id"": ""c31917f6-4d00-476f-92fe-b670135b42d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""5809a33b-a9de-45d9-9dc7-0c82ca643976"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2e24cf18-e9ff-4b2c-a1b7-7d30e4d210fb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc3a56e7-2e43-4186-9cb0-657ede489598"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23ea23b6-69b8-40cd-885a-f9c978883233"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f290ee6-04f4-4d9c-943a-4ae8c66bd8cf"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86b2a2ce-b448-4162-88dd-f2a784e9ba36"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""HardDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cf0dab4-4c7f-4e53-905a-ea9edfb4e035"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SoftDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08531e6b-48db-439f-9944-895934b56969"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SoftDrop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""214a38c5-d5c1-4d72-9005-6d96111fe1f4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""427d9ad6-e960-4ce0-b1b5-fc753675fa3a"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c49db815-dcc4-42df-b07b-db7c9e231717"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f39bb82b-8472-421f-8f1f-fe76324f144b"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34c7d998-34dd-453d-8ef0-d28efe7b8e08"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_RotateRight = m_Movement.FindAction("RotateRight", throwIfNotFound: true);
        m_Movement_RotateLeft = m_Movement.FindAction("RotateLeft", throwIfNotFound: true);
        m_Movement_MoveRight = m_Movement.FindAction("MoveRight", throwIfNotFound: true);
        m_Movement_MoveLeft = m_Movement.FindAction("MoveLeft", throwIfNotFound: true);
        m_Movement_HardDrop = m_Movement.FindAction("HardDrop", throwIfNotFound: true);
        m_Movement_SoftDrop = m_Movement.FindAction("SoftDrop", throwIfNotFound: true);
        m_Movement_Hold = m_Movement.FindAction("Hold", throwIfNotFound: true);
        m_Movement_Pause = m_Movement.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_RotateRight;
    private readonly InputAction m_Movement_RotateLeft;
    private readonly InputAction m_Movement_MoveRight;
    private readonly InputAction m_Movement_MoveLeft;
    private readonly InputAction m_Movement_HardDrop;
    private readonly InputAction m_Movement_SoftDrop;
    private readonly InputAction m_Movement_Hold;
    private readonly InputAction m_Movement_Pause;
    public struct MovementActions
    {
        private @InGameControls m_Wrapper;
        public MovementActions(@InGameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotateRight => m_Wrapper.m_Movement_RotateRight;
        public InputAction @RotateLeft => m_Wrapper.m_Movement_RotateLeft;
        public InputAction @MoveRight => m_Wrapper.m_Movement_MoveRight;
        public InputAction @MoveLeft => m_Wrapper.m_Movement_MoveLeft;
        public InputAction @HardDrop => m_Wrapper.m_Movement_HardDrop;
        public InputAction @SoftDrop => m_Wrapper.m_Movement_SoftDrop;
        public InputAction @Hold => m_Wrapper.m_Movement_Hold;
        public InputAction @Pause => m_Wrapper.m_Movement_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @RotateRight.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotateRight;
                @RotateRight.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotateRight;
                @RotateRight.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotateRight;
                @RotateLeft.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRotateLeft;
                @MoveRight.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveRight;
                @MoveLeft.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveLeft;
                @HardDrop.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnHardDrop;
                @HardDrop.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnHardDrop;
                @HardDrop.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnHardDrop;
                @SoftDrop.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSoftDrop;
                @SoftDrop.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSoftDrop;
                @SoftDrop.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSoftDrop;
                @Hold.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnHold;
                @Hold.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnHold;
                @Hold.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnHold;
                @Pause.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RotateRight.started += instance.OnRotateRight;
                @RotateRight.performed += instance.OnRotateRight;
                @RotateRight.canceled += instance.OnRotateRight;
                @RotateLeft.started += instance.OnRotateLeft;
                @RotateLeft.performed += instance.OnRotateLeft;
                @RotateLeft.canceled += instance.OnRotateLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @HardDrop.started += instance.OnHardDrop;
                @HardDrop.performed += instance.OnHardDrop;
                @HardDrop.canceled += instance.OnHardDrop;
                @SoftDrop.started += instance.OnSoftDrop;
                @SoftDrop.performed += instance.OnSoftDrop;
                @SoftDrop.canceled += instance.OnSoftDrop;
                @Hold.started += instance.OnHold;
                @Hold.performed += instance.OnHold;
                @Hold.canceled += instance.OnHold;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnRotateRight(InputAction.CallbackContext context);
        void OnRotateLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnHardDrop(InputAction.CallbackContext context);
        void OnSoftDrop(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
