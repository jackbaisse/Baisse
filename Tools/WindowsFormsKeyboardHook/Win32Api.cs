using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsKeyboardHook
{
    public class Win32Api
    {
        #region 常数和结构

        public const int WM_KEYDOWN = 0x100;

        public const int WM_KEYUP = 0x101;

        public const int WM_SYSKEYDOWN = 0x104;

        public const int WM_SYSKEYUP = 0x105;

        public const int WH_KEYBOARD_LL = 13;

        #region DeviceCaps常量
        public const int HORZRES = 8;
        public const int VERTRES = 10;
        public const int LOGPIXELSX = 88;
        public const int LOGPIXELSY = 90;
        public const int DESKTOPVERTRES = 117;
        public const int DESKTOPHORZRES = 118;
        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]//声明鼠标勾子的封送结构类型
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)] //声明键盘钩子的封送结构类型 
        public class KeyboardHookStruct
        {

            public int vkCode; //表示一个在1到254间的虚似键盘码 

            public int scanCode; //表示硬件扫描码 

            public int flags;

            public int time;

            public int dwExtraInfo;

        }
        #region Api

        //public delegate int HookProcMouse(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate int HookProcKey(int nCode, Int32 wParam, IntPtr lParam);

        //安装钩子的函数 

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern int SetWindowsHookEx(int idHook, HookProcKey lpfn, IntPtr hInstance, int threadId);

        //卸下钩子的函数 

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern bool UnhookWindowsHookEx(int idHook);

        //下一个钩挂的函数 

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        [DllImport("user32")]

        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]

        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        #region 鼠标
        //设置鼠标位置
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        //设置鼠标按键和动作
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo); //UIntPtr指针多句柄类型
        #endregion

        #endregion

        #endregion

        #region 获取电脑缩放比例
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(
        IntPtr hdc, // handle to DC
        int nIndex // index of capability
        );
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        #endregion
    }

    //将枚举作为位域处理
    [Flags]
    public enum MouseEventFlag : uint //设置鼠标动作的键值
    {
        Move = 0x0001,               //发生移动
        LeftDown = 0x0002,           //鼠标按下左键
        LeftUp = 0x0004,             //鼠标松开左键
        RightDown = 0x0008,          //鼠标按下右键
        RightUp = 0x0010,            //鼠标松开右键
        MiddleDown = 0x0020,         //鼠标按下中键
        MiddleUp = 0x0040,           //鼠标松开中键
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,              //鼠标轮被移动
        VirtualDesk = 0x4000,        //虚拟桌面
        Absolute = 0x8000//标示是否采用绝对坐标
    }
}
