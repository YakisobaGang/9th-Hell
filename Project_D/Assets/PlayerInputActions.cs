// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""OpenArea"",
            ""id"": ""8e351830-1b08-47bd-b022-c698b0499a65"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cf65d426-0034-49a4-a7b4-9b728cc95e0a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b14613ab-52f8-475c-b403-07b4eca6f153"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4f2c6403-c77c-49ed-8c2f-0bfd0bc47a57"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6470f025-6bc0-4790-a0e3-646f8425fa94"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b4a4061f-59a8-4c58-b21c-6617a2435d74"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""21ecafed-44f1-4719-8fa4-e9ed8597ebf7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b32d7d1b-15a7-4b0c-ad50-917a9bb2e28a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f2ef31d8-15ec-4407-8699-70fa142071d0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // OpenArea
        m_OpenArea = asset.FindActionMap("OpenArea", throwIfNotFound: true);
        m_OpenArea_Movement = m_OpenArea.FindAction("Movement", throwIfNotFound: true);
        m_OpenArea_Interact = m_OpenArea.FindAction("Interact", throwIfNotFound: true);
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

    // OpenArea
    private readonly InputActionMap m_OpenArea;
    private IOpenAreaActions m_OpenAreaActionsCallbackInterface;
    private readonly InputAction m_OpenArea_Movement;
    private readonly InputAction m_OpenArea_Interact;
    public struct OpenAreaActions
    {
        private @PlayerInputActions m_Wrapper;
        public OpenAreaActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_OpenArea_Movement;
        public InputAction @Interact => m_Wrapper.m_OpenArea_Interact;
        public InputActionMap Get() { return m_Wrapper.m_OpenArea; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OpenAreaActions set) { return set.Get(); }
        public void SetCallbacks(IOpenAreaActions instance)
        {
            if (m_Wrapper.m_OpenAreaActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_OpenAreaActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_OpenAreaActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_OpenAreaActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_OpenAreaActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_OpenAreaActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_OpenAreaActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_OpenAreaActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public OpenAreaActions @OpenArea => new OpenAreaActions(this);
    public interface IOpenAreaActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
