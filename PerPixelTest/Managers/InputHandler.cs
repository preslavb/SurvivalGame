namespace PerPixelTest.Managers
{
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;

    public static class InputHandler
    {
        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;

        private static MouseState currentMouseState;
        private static MouseState previousMouseState;

        private static List<Keys> pressedKeys;
        private static Keys keyToCheck;
        private static List<KeyState> pressedKeysStates;

        private static MouseKeys pressedMouseKey;
        private static MouseKeyState pressedMouseKeyState;

        private enum MouseKeys
        {
            Left,
            Right,
            Middle,
            None
        }

        private enum MouseKeyState
        {
            Held,
            Clicked,
            Released,
            None
        }

        public enum KeyState
        {
            Held,
            Clicked,
            Released,
            None
        }

        public static KeyboardState CurrentKeyboardState
        {
            get
            {
                return currentKeyboardState;
            }

            private set
            {
                currentKeyboardState = value;
            }
        }

        public static KeyboardState PreviousKeyboardState
        {
            get
            {
                return previousKeyboardState;
            }

            private set
            {
                previousKeyboardState = value;
            }
        }

        public static MouseState CurrentMouseState
        {
            get
            {
                return currentMouseState;
            }

            private set
            {
                currentMouseState = value;
            }
        }

        public static MouseState PreviousMouseState
        {
            get
            {
                return previousMouseState;
            }

            private set
            {
                previousMouseState = value;
            }
        }

        public static List<Keys> PressedKeys
        {
            get
            {
                return pressedKeys;
            }

            private set
            {
                pressedKeys = value;
            }
        }

        public static Keys KeyToCheck
        {
            get
            {
                return keyToCheck;
            }

            private set
            {
                keyToCheck = value;
            }
        }

        public static List<KeyState> PressedKeysStates
        {
            get
            {
                return pressedKeysStates;
            }

            private set
            {
                pressedKeysStates = value;
            }
        }

        private static MouseKeys PressedMouseKey
        {
            get
            {
                return pressedMouseKey;
            }

            set
            {
                pressedMouseKey = value;
            }
        }

        private static MouseKeyState PressedMouseKeyState
        {
            get
            {
                return pressedMouseKeyState;
            }

            set
            {
                pressedMouseKeyState = value;
            }
        }

        public static void Initialize()
        {
            PressedKeys = new List<Keys>();
            PressedKeysStates = new List<KeyState>();
        }
        public static void LoadContent()
        {
            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;

            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;
        }
        public static void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            CheckKeyState(previousKeyboardState, currentKeyboardState);
        }

        private static void CheckKeyState(KeyboardState previousState, KeyboardState currentState)
        {
            //pressedKeys.Clear();
            //pressedKeysStates.Clear();
            //
            for (int i = 0; i < currentState.GetPressedKeys().Length; i++)
            {
                keyToCheck = currentState.GetPressedKeys()[i];
                if (previousState.IsKeyUp(keyToCheck) && currentState.IsKeyDown(keyToCheck))
                {
                    pressedKeys.Add(keyToCheck);
                    pressedKeysStates.Add(KeyState.Clicked);
                }
                else if (previousState.IsKeyDown(keyToCheck) && currentState.IsKeyDown(keyToCheck))
                {
                    //pressedKeys.Add(keyToCheck);
                    pressedKeysStates[pressedKeys.IndexOf(keyToCheck)] = KeyState.Held;
                }
            }

            for (int i = 0; i < previousState.GetPressedKeys().Length; i++)
            {
                keyToCheck = previousState.GetPressedKeys()[i];
                if (previousState.IsKeyDown(keyToCheck) && currentState.IsKeyUp(keyToCheck))
                {
                    pressedKeysStates[pressedKeys.IndexOf(keyToCheck)] = KeyState.Released;
                }
            }

            for (int i = 0; i < pressedKeys.Count; i++)
            {
                if (previousState.IsKeyUp(pressedKeys[i]) && currentState.IsKeyUp(pressedKeys[i]))
                {
                    pressedKeys[i] = Keys.None;
                    pressedKeysStates[i] = KeyState.None;
                }
            }

            while (pressedKeys.Contains(Keys.None))
            {
                pressedKeysStates.RemoveAt(pressedKeys.IndexOf(Keys.None));
                pressedKeys.RemoveAt(pressedKeys.IndexOf(Keys.None));
            }
        }
        private static void CheckButtonState(MouseState previousState, MouseState currentState)
        {
            if (previousState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed)
            {
                pressedMouseKey = MouseKeys.Left;
                pressedMouseKeyState = MouseKeyState.Clicked;
            }
            else if (previousState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Pressed)
            {
                pressedMouseKey = MouseKeys.Left;
                pressedMouseKeyState = MouseKeyState.Held;
            }
            else if (previousState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released)
            {
                pressedMouseKey = MouseKeys.Left;
                pressedMouseKeyState = MouseKeyState.Released;
            }

            else if (previousState.RightButton == ButtonState.Released && currentState.RightButton == ButtonState.Pressed)
            {
                pressedMouseKey = MouseKeys.Right;
                pressedMouseKeyState = MouseKeyState.Clicked;
            }
            else if (previousState.RightButton == ButtonState.Pressed && currentState.RightButton == ButtonState.Pressed)
            {
                pressedMouseKey = MouseKeys.Right;
                pressedMouseKeyState = MouseKeyState.Held;
            }
            else if (previousState.RightButton == ButtonState.Pressed && currentState.RightButton == ButtonState.Released)
            {
                pressedMouseKey = MouseKeys.Right;
                pressedMouseKeyState = MouseKeyState.Released;
            }

            else if (previousState.MiddleButton == ButtonState.Released && currentState.MiddleButton == ButtonState.Pressed)
            {
                pressedMouseKey = MouseKeys.Middle;
                pressedMouseKeyState = MouseKeyState.Clicked;
            }
            else if (previousState.MiddleButton == ButtonState.Pressed && currentState.MiddleButton == ButtonState.Pressed)
            {
                pressedMouseKey = MouseKeys.Middle;
                pressedMouseKeyState = MouseKeyState.Held;
            }
            else if (previousState.MiddleButton == ButtonState.Pressed && currentState.MiddleButton == ButtonState.Released)
            {
                pressedMouseKey = MouseKeys.Middle;
                pressedMouseKeyState = MouseKeyState.Released;
            }

            else
            {
                pressedMouseKey = MouseKeys.None;
                pressedMouseKeyState = MouseKeyState.None;
            }
        }
    }
}
