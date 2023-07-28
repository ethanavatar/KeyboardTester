using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLaptop: MonoBehaviour {
    static int keySize = Screen.width / 20;
    static int keySpacing = (int) (0.15f * keySize);
    static KeyCode[] Keyboard = new KeyCode[] { KeyCode.Escape, KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12, KeyCode.Delete,
        KeyCode.BackQuote, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Minus, KeyCode.Equals, KeyCode.Backspace,
        KeyCode.Tab, KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.LeftBracket, KeyCode.RightBracket, KeyCode.Backslash,
        KeyCode.CapsLock, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Return,
        KeyCode.LeftShift, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period, KeyCode.Slash, KeyCode.RightShift,
        KeyCode.LeftControl, KeyCode.LeftWindows, KeyCode.LeftAlt, KeyCode.Space, KeyCode.RightAlt, KeyCode.RightControl, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow };

    static int[] rowCounts = new int[] {
        13,
        14,
        14,
        13,
        12,
        10,
    };

    static Dictionary<KeyCode, float> wideKeys = new Dictionary<KeyCode, float>() {
        { KeyCode.Tab, 1.5f },
        { KeyCode.CapsLock, 1.75f },
        { KeyCode.LeftShift, 2.25f },
        { KeyCode.RightShift, 2.75f },
        { KeyCode.LeftControl, 1.25f },
        { KeyCode.RightControl, 1.25f },
        { KeyCode.LeftAlt, 1.25f },
        { KeyCode.RightAlt, 1.25f },
        { KeyCode.LeftWindows, 1.25f },
        { KeyCode.RightWindows, 1.25f },
        { KeyCode.Menu, 1.25f },
        { KeyCode.Return, 1.75f },
        { KeyCode.Backspace, 1.75f },
        { KeyCode.Space, 6.5f },
    };

    static Dictionary<KeyCode, float> shortKeys = new Dictionary<KeyCode, float>() {
        { KeyCode.UpArrow, 0.45f },
        { KeyCode.DownArrow, 0.45f },
    };

    bool[] keystate = new bool[Keyboard.Length];
    void OnGUI() {

        keySize = Screen.width / 20;

        int startingX = (int) (Screen.width * 0.5f - (keySize * 15 + keySpacing * 14) * 0.5f);
        int startingY = (int) (Screen.height * 0.5f - (keySize * 7 + keySpacing * 6) * 0.5f);

        int horizontalOffset = startingX;
        int verticalOffset = startingY;

        int row = 0;
        int col = 0;

        int numHalfHeights = 0;

        for (int i = 0; i < Keyboard.Length; i++) {

            bool isDown = keystate[i];
            if (keystate[i]) {
                isDown = true;
            }
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.textColor = isDown ? Color.red : Color.white;

            float keyWidthMultiplier = 1;
            if (wideKeys.ContainsKey(Keyboard[i])) {
                keyWidthMultiplier = wideKeys[Keyboard[i]];
            }

            float keyHeightMultiplier = 1;
            if (shortKeys.ContainsKey(Keyboard[i])) {
                keyHeightMultiplier = shortKeys[Keyboard[i]];
                numHalfHeights += 1;
            }

            int keyWidth = (int) (keySize * keyWidthMultiplier);
            int keyHeight = (int) (keySize * keyHeightMultiplier);
            GUI.Box(new Rect(horizontalOffset, verticalOffset, keyWidth, keyHeight), Keyboard[i].ToString(), style);

            if (keyHeightMultiplier < 1) {
                verticalOffset += keyHeight + keySpacing;
            } else {
                horizontalOffset += keyWidth + keySpacing;
            }

            if (numHalfHeights == 2) {
                verticalOffset -= keySize + keySpacing;
                horizontalOffset += keySize + keySpacing;
                numHalfHeights = 0;
            }
            
            if (col == rowCounts[row]) {
                horizontalOffset = startingX;
                verticalOffset += keySize + keySpacing;
                row++;
                col = 0;
            }

            col += 1;
        }
    }

    void FixedUpdate() {
        for (int i = 0; i < Keyboard.Length; i++) {
            keystate[i] = Input.GetKey(Keyboard[i]);
        }
    }
}
