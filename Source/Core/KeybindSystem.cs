namespace Hot_Lava_Cheat.Source.Core
{
    public class KeybindSystem
    {
        private class KeybindState
        {
            public System.Func<UnityEngine.KeyCode> keyGetter;
            public System.Func<bool> toggleGetter;
            public bool active;
            public System.Action<bool> callback;
            public bool lastKeyState;
            public string name;
            public bool wasActive;
        }

        private static System.Collections.Generic.List<KeybindState> keybinds = new System.Collections.Generic.List<KeybindState>();

        public static void RegisterKeybind(System.Func<UnityEngine.KeyCode> keyGetter, System.Func<bool> toggleGetter, System.Action<bool> callback, string name)
        {
            keybinds.Add(new KeybindState
            {
                keyGetter = keyGetter,
                toggleGetter = toggleGetter,
                active = false,
                callback = callback,
                lastKeyState = false,
                name = name,
                wasActive = false
            });
        }

        public static void ProcessKeybinds()
        {
            bool bBlocked = NS_Core.Utils.IsGameplayInputBlocked();

            foreach (var bind in keybinds)
            {
                UnityEngine.KeyCode key = bind.keyGetter();

                bool currentKeyState = NS_Core.Utils.GetKeyState(key);
                bool toggle = bind.toggleGetter();

                if (bBlocked)
                {
                    if (!toggle && bind.active && bind.callback != null)
                        bind.callback(false);

                    bind.active = false;
                    bind.lastKeyState = currentKeyState;
                    bind.wasActive = currentKeyState;

                    continue;
                }

                if (toggle)
                {
                    bind.active = currentKeyState;

                    if (currentKeyState && !bind.lastKeyState)
                    {
                        if (bind.callback != null)
                            bind.callback(true);
                    }

                    bind.wasActive = currentKeyState;
                }

                else
                {
                    bind.active = currentKeyState;

                    if (bind.active && !bind.wasActive)
                    {
                        if (bind.callback != null)
                            bind.callback(true);
                    }

                    else if (!bind.active && bind.wasActive)
                    {
                        if (bind.callback != null)
                            bind.callback(false);
                    }

                    bind.wasActive = bind.active;
                }

                bind.lastKeyState = currentKeyState;
            }
        }

        public static bool GetBindState(string name)
        {
            foreach (var bind in keybinds)
            {
                if (bind.name == name)
                    return bind.active;
            }

            return false;
        }

        public static void Clear()
        {
            keybinds.Clear();
        }
    }
}