using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ThinkTwiceKeyboardFix
{
    class InterceptKeys
    {
        private readonly WinApi.LowLevelKeyboardProc _proc;
        private static IntPtr _hookId = IntPtr.Zero;

        private InterceptKeys()
        {
            _proc = HookCallback;
        }

        public static void Main()
        {
            var app = new InterceptKeys();
            app.Run();
        }

        private void Run()
        {
            _hookId = SetHook(_proc);
            var mainForm = new Form { Text = "ThinkTwice keyboard fix", Width = 600, Height = 400};
            mainForm.Controls.Add(new Label {Text = "Remaps {PrtSc} key to {Shift}{F10} which has a function of bringing up current context menu, similar to a right click." + Environment.NewLine + "Fixes Lenovo ThinkPad keyboard problem, as this button is replaced with {PrtSc} on some newer makes.", Dock = DockStyle.Fill});
            Application.Run(mainForm);

            WinApi.UnhookWindowsHookEx(_hookId);
        }

        private static IntPtr SetHook(WinApi.LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return WinApi.SetWindowsHookEx(Messages.WH_KEYBOARD_LL, proc,
                    WinApi.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr) Messages.WM_KEYDOWN)
            {
                var vkCode = Marshal.ReadInt32(lParam);
                var key = (Keys) vkCode;
                Debug.WriteLine(new { Control.ModifierKeys, Key = key });

                if (key == Keys.PrintScreen && Control.ModifierKeys == Keys.None)
                {
                    SendKeys.Send("+{F10}");
                    return (IntPtr) 1;
                }
            }

            return WinApi.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}