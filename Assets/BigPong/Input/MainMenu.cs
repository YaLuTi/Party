// GENERATED AUTOMATICALLY FROM 'Assets/BigPong/Input/MainMenu.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainMenu : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainMenu()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainMenu"",
    ""maps"": [
        {
            ""name"": ""MainMenuUI"",
            ""id"": ""1000980e-a220-44be-a373-6b92253cda1f"",
            ""actions"": [
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""ceae5742-833d-43e2-a42b-9214722e54af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3a210943-e34c-4eef-a707-ab94a0c788e1"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c458f18-2e11-4291-a5bb-a028a3b3a1fe"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": []
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // MainMenuUI
        m_MainMenuUI = asset.FindActionMap("MainMenuUI", throwIfNotFound: true);
        m_MainMenuUI_Next = m_MainMenuUI.FindAction("Next", throwIfNotFound: true);
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

    // MainMenuUI
    private readonly InputActionMap m_MainMenuUI;
    private IMainMenuUIActions m_MainMenuUIActionsCallbackInterface;
    private readonly InputAction m_MainMenuUI_Next;
    public struct MainMenuUIActions
    {
        private @MainMenu m_Wrapper;
        public MainMenuUIActions(@MainMenu wrapper) { m_Wrapper = wrapper; }
        public InputAction @Next => m_Wrapper.m_MainMenuUI_Next;
        public InputActionMap Get() { return m_Wrapper.m_MainMenuUI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuUIActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuUIActions instance)
        {
            if (m_Wrapper.m_MainMenuUIActionsCallbackInterface != null)
            {
                @Next.started -= m_Wrapper.m_MainMenuUIActionsCallbackInterface.OnNext;
                @Next.performed -= m_Wrapper.m_MainMenuUIActionsCallbackInterface.OnNext;
                @Next.canceled -= m_Wrapper.m_MainMenuUIActionsCallbackInterface.OnNext;
            }
            m_Wrapper.m_MainMenuUIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Next.started += instance.OnNext;
                @Next.performed += instance.OnNext;
                @Next.canceled += instance.OnNext;
            }
        }
    }
    public MainMenuUIActions @MainMenuUI => new MainMenuUIActions(this);
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IMainMenuUIActions
    {
        void OnNext(InputAction.CallbackContext context);
    }
}
