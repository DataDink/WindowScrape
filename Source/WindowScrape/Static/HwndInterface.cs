namespace WindowScrape.Static
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using WindowScrape.Constants;
    using WindowScrape.Types;

    public static class HwndInterface
    {
        public static bool ActivateWindow(IntPtr hwnd)
        {
            return SetForegroundWindow(hwnd);
        }

        public static void ClickHwnd(IntPtr hwnd)
        {
            SendMessage(hwnd, 0xf5, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("user32.dll")]
        private static extern bool CloseWindow(IntPtr hWnd);
        public static List<IntPtr> EnumChildren(IntPtr hwnd)
        {
            IntPtr zero = IntPtr.Zero;
            List<IntPtr> list = new List<IntPtr>();
            do
            {
                zero = FindWindowEx(hwnd, zero, null, null);
                if (zero != IntPtr.Zero)
                {
                    list.Add(zero);
                }
            }
            while (zero != IntPtr.Zero);
            return list;
        }

        public static List<IntPtr> EnumHwnds()
        {
            return EnumChildren(IntPtr.Zero);
        }

        [DllImport("user32.dll")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError=true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        public static IntPtr GetHwnd(string windowText, string className)
        {
            return (IntPtr) FindWindow(className, windowText);
        }

        public static IntPtr GetHwndChild(IntPtr hwnd, string clsName, string ctrlText)
        {
            return FindWindowEx(hwnd, IntPtr.Zero, clsName, ctrlText);
        }

        public static string GetHwndClassName(IntPtr hwnd)
        {
            StringBuilder lpClassName = new StringBuilder(0x100);
            GetClassName(hwnd, lpClassName, lpClassName.MaxCapacity);
            return lpClassName.ToString();
        }

        public static IntPtr GetHwndFromClass(string className)
        {
            return (IntPtr) FindWindow(className, null);
        }

        public static IntPtr GetHwndFromTitle(string windowText)
        {
            return (IntPtr) FindWindow(null, windowText);
        }

        public static IntPtr GetHwndParent(IntPtr hwnd)
        {
            return GetParent(hwnd);
        }

        public static Point GetHwndPos(IntPtr hwnd)
        {
            WindowScrape.Types.RECT lpRect = new WindowScrape.Types.RECT();
            GetWindowRect(hwnd, out lpRect);
            return new Point(lpRect.Left, lpRect.Top);
        }

        public static Size GetHwndSize(IntPtr hwnd)
        {
            WindowScrape.Types.RECT lpRect = new WindowScrape.Types.RECT();
            GetWindowRect(hwnd, out lpRect);
            return new Size(lpRect.Right - lpRect.Left, lpRect.Bottom - lpRect.Top);
        }

        public static string GetHwndText(IntPtr hwnd)
        {
            int capacity = ((int) SendMessage(hwnd, (uint) 14, (uint) 0, (uint) 0)) + 1;
            StringBuilder lParam = new StringBuilder(capacity);
            SendMessage(hwnd, 13, (uint) capacity, lParam);
            return lParam.ToString();
        }

        public static string GetHwndTitle(IntPtr hwnd)
        {
            StringBuilder lpString = new StringBuilder(GetHwndTitleLength(hwnd) + 1);
            GetWindowText(hwnd, lpString, lpString.Capacity);
            return lpString.ToString();
        }

        public static int GetHwndTitleLength(IntPtr hwnd)
        {
            return GetWindowTextLength(hwnd);
        }

        public static int GetMessageInt(IntPtr hwnd, WM msg)
        {
            return (int) SendMessage(hwnd, (uint) msg, (uint) 0, (uint) 0);
        }

        public static string GetMessageString(IntPtr hwnd, WM msg, uint param)
        {
            StringBuilder lParam = new StringBuilder(0x10000);
            SendMessage(hwnd, (uint) msg, param, lParam);
            return lParam.ToString();
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern IntPtr GetParent(IntPtr hWnd);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out WindowScrape.Types.RECT lpRect);
        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        public static bool MinimizeWindow(IntPtr hwnd)
        {
            return CloseWindow(hwnd);
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, string lParam);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, StringBuilder lParam);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        public static int SendMessage(IntPtr hwnd, WM msg, uint param1, string param2)
        {
            return (int) SendMessage(hwnd, (uint) msg, param1, param2);
        }

        public static int SendMessage(IntPtr hwnd, WM msg, uint param1, uint param2)
        {
            return (int) SendMessage(hwnd, (uint) msg, param1, param2);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        public static bool SetHwndPos(IntPtr hwnd, int x, int y)
        {
            return SetWindowPos(hwnd, IntPtr.Zero, x, y, 0, 0, 5);
        }

        public static bool SetHwndSize(IntPtr hwnd, int w, int h)
        {
            return SetWindowPos(hwnd, IntPtr.Zero, 0, 0, w, h, 6);
        }

        public static void SetHwndText(IntPtr hwnd, string text)
        {
            SendMessage(hwnd, (uint) 12, (uint) 0, text);
        }

        public static bool SetHwndTitle(IntPtr hwnd, string text)
        {
            return SetWindowText(hwnd, text);
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("USER32.DLL")]
        private static extern bool SetWindowText(IntPtr hWnd, string lpString);
    }
}

