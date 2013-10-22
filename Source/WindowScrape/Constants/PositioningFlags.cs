namespace WindowScrape.Constants
{
    using System;

    [Flags]
    internal enum PositioningFlags
    {
        SWP_ASYNCWINDOWPOS = 0x4000,
        SWP_DEFERERASE = 0x2000,
        SWP_DRAWFRAME = 0x20,
        SWP_FRAMECHANGED = 0x20,
        SWP_HIDEWINDOW = 0x80,
        SWP_NOACTIVATE = 0x10,
        SWP_NOCOPYBITS = 0x100,
        SWP_NOMOVE = 2,
        SWP_NOOWNERZORDER = 0x200,
        SWP_NOREDRAW = 8,
        SWP_NOREPOSITION = 0x200,
        SWP_NOSENDCHANGING = 0x400,
        SWP_NOSIZE = 1,
        SWP_NOZORDER = 4,
        SWP_SHOWWINDOW = 0x40
    }
}

