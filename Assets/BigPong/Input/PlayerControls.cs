// GENERATED AUTOMATICALLY FROM 'Assets/BigPong/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""fe2a82f3-bbc0-4015-9bb3-2d6173c284e6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""260556b6-532e-4886-aedb-8ef1d64fbe2e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9f135245-1434-49d6-8130-6db1f22c10c6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pick"",
                    ""type"": ""Button"",
                    ""id"": ""c39d4c13-e5e3-4a49-97e1-c8410d7b262f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""1883b29a-41db-47e9-b042-19d5c64a2e52"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""6f7c1c1c-c7ce-4280-8252-829c4af75de5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""b26c138d-b7f2-4c75-8f81-05784d3351d6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UI_Up"",
                    ""type"": ""Button"",
                    ""id"": ""39b36c9d-b321-4af7-8418-fa1280169c31"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UI_Down"",
                    ""type"": ""Button"",
                    ""id"": ""2043ad6a-1557-462e-b38c-129ed837a476"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UI_Left"",
                    ""type"": ""Button"",
                    ""id"": ""a09874c5-db05-4d8d-b81d-0feaf234e8a3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UI_Right"",
                    ""type"": ""Button"",
                    ""id"": ""50b8513b-073a-4948-ab49-10260f965bb8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5a56a8ee-84c3-448c-b3be-38a1ec5d76ff"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d4112baf-f53e-4933-b774-4b2475dfef9c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ab48a966-6cb8-4d08-bf3a-d8b4c1183dba"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e88baa45-7002-4dbb-b70b-4bfbd1168577"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e18bc1d8-c2e6-4d18-97c1-3baa232ef575"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f120d459-e39d-47d0-889b-8e01333c1a1d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""53d2acf4-87ff-4ce4-92d4-ab95bb1714fa"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0a94306-55e2-46ca-82f9-0401830cccb8"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5527283-c3ea-4787-8a9c-a29bf721a387"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Pick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0430dfe7-9e14-4107-8ac9-ff6003e0b4b7"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Pick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ffda010-e541-422a-855f-74202a2f10f0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8da6f623-23a5-4747-a710-d55fad0a0f00"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7d9329b-3e86-49e8-a7d7-ec5212a1e2a7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4c33382-9d8c-41d1-90f1-4cdf6da70a95"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0855eb4-93b7-4bf0-a185-a9ff3086a3d8"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6af673b6-d4ed-411e-aa6b-c1bb31e223a3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""UI_Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""261dbfb2-1a7e-47bc-b233-2f95fc2852bf"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""UI_Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d55746f-f075-4527-a5b1-f903950ef32b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""UI_Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bdc31c6-8d45-47bc-ba8d-1e4cd9ae8124"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""UI_Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84a104bd-e96a-4018-bd34-8cb5ba8f2b10"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""UI_Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46218998-2b86-42b4-90cd-b9138d5efdfa"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""UI_Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""909c8135-cd40-47e4-9599-c78893e98bb9"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""UI_Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27e305b6-46ef-4f47-bc72-095ff9a85821"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""UI_Right"",
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
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Move = m_GamePlay.FindAction("Move", throwIfNotFound: true);
        m_GamePlay_Shoot = m_GamePlay.FindAction("Shoot", throwIfNotFound: true);
        m_GamePlay_Pick = m_GamePlay.FindAction("Pick", throwIfNotFound: true);
        m_GamePlay_Throw = m_GamePlay.FindAction("Throw", throwIfNotFound: true);
        m_GamePlay_Test = m_GamePlay.FindAction("Test", throwIfNotFound: true);
        m_GamePlay_Pause = m_GamePlay.FindAction("Pause", throwIfNotFound: true);
        m_GamePlay_UI_Up = m_GamePlay.FindAction("UI_Up", throwIfNotFound: true);
        m_GamePlay_UI_Down = m_GamePlay.FindAction("UI_Down", throwIfNotFound: true);
        m_GamePlay_UI_Left = m_GamePlay.FindAction("UI_Left", throwIfNotFound: true);
        m_GamePlay_UI_Right = m_GamePlay.FindAction("UI_Right", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Move;
    private readonly InputAction m_GamePlay_Shoot;
    private readonly InputAction m_GamePlay_Pick;
    private readonly InputAction m_GamePlay_Throw;
    private readonly InputAction m_GamePlay_Test;
    private readonly InputAction m_GamePlay_Pause;
    private readonly InputAction m_GamePlay_UI_Up;
    private readonly InputAction m_GamePlay_UI_Down;
    private readonly InputAction m_GamePlay_UI_Left;
    private readonly InputAction m_GamePlay_UI_Right;
    public struct GamePlayActions
    {
        private @PlayerControls m_Wrapper;
        public GamePlayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GamePlay_Move;
        public InputAction @Shoot => m_Wrapper.m_GamePlay_Shoot;
        public InputAction @Pick => m_Wrapper.m_GamePlay_Pick;
        public InputAction @Throw => m_Wrapper.m_GamePlay_Throw;
        public InputAction @Test => m_Wrapper.m_GamePlay_Test;
        public InputAction @Pause => m_Wrapper.m_GamePlay_Pause;
        public InputAction @UI_Up => m_Wrapper.m_GamePlay_UI_Up;
        public InputAction @UI_Down => m_Wrapper.m_GamePlay_UI_Down;
        public InputAction @UI_Left => m_Wrapper.m_GamePlay_UI_Left;
        public InputAction @UI_Right => m_Wrapper.m_GamePlay_UI_Right;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Shoot.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShoot;
                @Pick.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPick;
                @Pick.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPick;
                @Pick.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPick;
                @Throw.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnThrow;
                @Test.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTest;
                @Pause.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnPause;
                @UI_Up.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Up;
                @UI_Up.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Up;
                @UI_Up.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Up;
                @UI_Down.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Down;
                @UI_Down.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Down;
                @UI_Down.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Down;
                @UI_Left.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Left;
                @UI_Left.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Left;
                @UI_Left.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Left;
                @UI_Right.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Right;
                @UI_Right.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Right;
                @UI_Right.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnUI_Right;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Pick.started += instance.OnPick;
                @Pick.performed += instance.OnPick;
                @Pick.canceled += instance.OnPick;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @UI_Up.started += instance.OnUI_Up;
                @UI_Up.performed += instance.OnUI_Up;
                @UI_Up.canceled += instance.OnUI_Up;
                @UI_Down.started += instance.OnUI_Down;
                @UI_Down.performed += instance.OnUI_Down;
                @UI_Down.canceled += instance.OnUI_Down;
                @UI_Left.started += instance.OnUI_Left;
                @UI_Left.performed += instance.OnUI_Left;
                @UI_Left.canceled += instance.OnUI_Left;
                @UI_Right.started += instance.OnUI_Right;
                @UI_Right.performed += instance.OnUI_Right;
                @UI_Right.canceled += instance.OnUI_Right;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IGamePlayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnPick(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnUI_Up(InputAction.CallbackContext context);
        void OnUI_Down(InputAction.CallbackContext context);
        void OnUI_Left(InputAction.CallbackContext context);
        void OnUI_Right(InputAction.CallbackContext context);
    }
}
