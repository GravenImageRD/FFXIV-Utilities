﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace InputSimulator
{
    public static class InputSimulator
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        const uint WM_KEYDOWN = 0x100;
        const uint WM_KEYUP = 0x0101;

        public class ModifierKey : IDisposable
        {
            private IntPtr _gameWindow;
            private VirtualKeyCode? _modifierKey;

            public ModifierKey(IntPtr gameWindow, VirtualKeyCode? modifierKey)
            {
                this._gameWindow = gameWindow;
                this._modifierKey = modifierKey;

                if (this._modifierKey.HasValue)
                {
                    KeyDown(this._gameWindow, this._modifierKey.Value);
                }
            }

            public void Dispose()
            {
                if (this._modifierKey.HasValue)
                {
                    KeyUp(this._gameWindow, this._modifierKey.Value);
                }
            }
        }

        private static void KeyUp(IntPtr gameWindow, VirtualKeyCode key)
        {
            SendMessage(gameWindow, WM_KEYUP, ((IntPtr)key), (IntPtr)0);
        }

        private static void KeyDown(IntPtr gameWindow, VirtualKeyCode key)
        {
            SendMessage(gameWindow, WM_KEYDOWN, ((IntPtr)key), (IntPtr)0);
        }

        private static void KeyPress(IntPtr gameWindow, VirtualKeyCode key)
        {
            KeyDown(gameWindow, key);
            KeyUp(gameWindow, key);
        }

        public struct KeyData
        {
            public VirtualKeyCode Code;
            public VirtualKeyCode? Modifier;
        }

        public struct KeySequence
        {
            public List<VirtualKeyCode> Keys;
        }
        
        public static void SendKeyPress(IntPtr gameWindow, KeyData keyData)
        {
            using (var modifierKey = new ModifierKey(gameWindow, keyData.Modifier))
            {
                KeyPress(gameWindow, keyData.Code);
            }
        }

        public static void SendKeyPressSequence(IntPtr gameWindow, IEnumerable<KeyData> keys)
        {
            foreach (var keyData in keys)
            {
                Debug.Write(keyData.Code.ToString());
                SendKeyPress(gameWindow, keyData);
                Thread.Sleep(500);
            }
            Debug.WriteLine("");
        }
    }
}
