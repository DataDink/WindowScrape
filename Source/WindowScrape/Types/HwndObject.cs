namespace WindowScrape.Types
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using WindowScrape.Constants;
    using WindowScrape.Static;

    public class HwndObject
    {
        public HwndObject(IntPtr hwnd)
        {
            this.Hwnd = hwnd;
        }

        public void Click()
        {
            HwndInterface.ClickHwnd(this.Hwnd);
        }

        // <summary>
        // Bring this window to the foreground
        // </summary>
        public bool Activate()
        {
            return HwndInterface.ActivateWindow(Hwnd);
        }

        // <summary>
        // Minimize this window
        // </summary>
        public bool Minimize()
        {
            return HwndInterface.MinimizeWindow(Hwnd);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (!(obj.GetType() == typeof(HwndObject)))
            {
                return false;
            }
            return this.Equals((HwndObject) obj);
        }

        public bool Equals(HwndObject obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            return (object.ReferenceEquals(this, obj) || obj.Hwnd.Equals(this.Hwnd));
        }

        public HwndObject GetChild(string cls, string title)
        {
            return new HwndObject(HwndInterface.GetHwndChild(this.Hwnd, cls, title));
        }

        public List<HwndObject> GetChildren()
        {
            List<HwndObject> list = new List<HwndObject>();
            foreach (IntPtr ptr in HwndInterface.EnumChildren(this.Hwnd))
            {
                list.Add(new HwndObject(ptr));
            }
            return list;
        }

        public override int GetHashCode()
        {
            return this.Hwnd.GetHashCode();
        }

        public int GetMessageInt(WM msg)
        {
            return HwndInterface.GetMessageInt(this.Hwnd, msg);
        }

        public string GetMessageString(WM msg, uint param)
        {
            return HwndInterface.GetMessageString(this.Hwnd, msg, param);
        }

        public HwndObject GetParent()
        {
            return new HwndObject(HwndInterface.GetHwndParent(this.Hwnd));
        }

        public static HwndObject GetWindowByTitle(string title)
        {
            return new HwndObject(HwndInterface.GetHwndFromTitle(title));
        }

        public static HwndObject GetWindowByClassName(string className)
        {
            return new HwndObject(HwndInterface.GetHwndFromClass(className));
        }

        public static List<HwndObject> GetWindows()
        {
            List<HwndObject> list = new List<HwndObject>();
            foreach (IntPtr ptr in HwndInterface.EnumHwnds())
            {
                list.Add(new HwndObject(ptr));
            }
            return list;
        }

        public static bool operator ==(HwndObject a, HwndObject b)
        {
            if (object.ReferenceEquals(a, null))
            {
                return object.ReferenceEquals(b, null);
            }
            else if (object.ReferenceEquals(b, null))
            {
                return object.ReferenceEquals(a, null);
            }
            return (a.Hwnd == b.Hwnd);
        }

        public static bool operator !=(HwndObject a, HwndObject b)
        {
            return !(a == b);
        }

        public void SendMessage(WM msg, uint param1, string param2)
        {
            HwndInterface.SendMessage(this.Hwnd, msg, param1, param2);
        }

        public void SendMessage(WM msg, uint param1, uint param2)
        {
            HwndInterface.SendMessage(this.Hwnd, msg, param1, param2);
        }

        public override string ToString()
        {
            Point location = this.Location;
            System.Drawing.Size size = this.Size;
            return string.Format("({0}) {1},{2}:{3}x{4} \"{5}\"", new object[] { this.Hwnd, location.X, location.Y, size.Width, size.Height, this.Title });
        }

        public string ClassName
        {
            get
            {
                return HwndInterface.GetHwndClassName(this.Hwnd);
            }
        }

        public IntPtr Hwnd { get; private set; }

        public Point Location
        {
            get
            {
                return HwndInterface.GetHwndPos(this.Hwnd);
            }
            set
            {
                HwndInterface.SetHwndPos(this.Hwnd, value.X, value.Y);
            }
        }

        public System.Drawing.Size Size
        {
            get
            {
                return HwndInterface.GetHwndSize(this.Hwnd);
            }
            set
            {
                HwndInterface.SetHwndSize(this.Hwnd, value.Width, value.Height);
            }
        }

        public string Text
        {
            get
            {
                return HwndInterface.GetHwndText(this.Hwnd);
            }
            set
            {
                HwndInterface.SetHwndText(this.Hwnd, value);
            }
        }

        public string Title
        {
            get
            {
                return HwndInterface.GetHwndTitle(this.Hwnd);
            }
            set
            {
                HwndInterface.SetHwndTitle(this.Hwnd, value);
            }
        }
    }
}

