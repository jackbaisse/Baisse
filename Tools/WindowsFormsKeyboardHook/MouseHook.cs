using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsKeyboardHook.Win32Api;

namespace WindowsFormsKeyboardHook
{
    public class MouseHook
    {
        //委托+事件（把钩到的消息封装为事件，由调用者处理）
        public delegate void MouseMoveHandler(object sender, MouseEventArgs e);
        public event MouseMoveHandler MouseMoveEvent;

        public delegate void MouseClickHandler(object sender, MouseEventArgs e);
        public event MouseClickHandler MouseClickEvent;

        public delegate void MouseEventHandler(object sender, MouseEventArgs e);

        //Point p = new Point(1, 1);//定义存放获取坐标的point变量

        private Point point;
        public Point Point
        {
            get { return point; }
            set
            {
                if (point != value)
                {
                    point = value;
                }
            }
        }


        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private int hHook = 0;
        public const int WH_MOUSE_LL = 14;
        public Win32Api.HookProcKey hProc;

        public HookProc MyProcedure;
        public MouseHook()
        {
            //MouseClickEvent += MouseHook_MouseClickEvent;
            //this.Point = new Point();
        }

        /// <summary>
        /// 安装勾子
        /// </summary>
        /// <returns></returns>
        public int SetHook()
        {
            hProc = new Win32Api.HookProcKey(MouseHookProc);
            hHook = Win32Api.SetWindowsHookEx(WH_MOUSE_LL, hProc, IntPtr.Zero, 0);
            return hHook;
        }
        /// <summary>
        /// 卸载勾子
        /// </summary>
        public void UnHook()
        {
            Win32Api.UnhookWindowsHookEx(hHook);
        }

        /// <summary>
        /// 下一个勾子
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (MouseClickEvent != null || MouseMoveEvent != null))
            {
                Win32Api.MouseHookStruct MyMouseHookStruct = (Win32Api.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.MouseHookStruct));
                Point = new Point(MyMouseHookStruct.pt.x, MyMouseHookStruct.pt.y);
                if (MouseMoveEvent != null && wParam == 512)//移动
                {
                    MouseEventArgs e = new MouseEventArgs(MouseButtons.Middle, 0, point.X, point.Y, 0);
                    MouseMoveEvent(this, e);
                }
                else if (MouseClickEvent != null && wParam == 513)//左建
                {
                    MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 0);
                    MouseClickEvent(this, e);
                }
                else if (MouseClickEvent != null && wParam == 516)//右键
                {
                    MouseEventArgs e = new MouseEventArgs(MouseButtons.Right, 0, point.X, point.Y, 0);
                    MouseClickEvent(this, e);
                }
                //if ((MouseClickEvent != null) && (wParam == 522))//鼠标滚轮
                //{
                //    MouseEventArgs e = new MouseEventArgs(MouseButtons.XButton1, 0, point.X, point.Y, 0);
                //    MouseClickEvent(this, e);
                //}
                //if ((MouseClickEvent != null) && (wParam == 519))//按下鼠标滚轮
                //{
                //    MouseEventArgs e = new MouseEventArgs(MouseButtons.XButton1, 0, point.X, point.Y, 0);
                //    MouseClickEvent(this, e);
                //}
            }
            return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
            //Win32Api.MouseHookStruct MyMouseHookStruct = (Win32Api.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.MouseHookStruct));
            //if (nCode < 0)
            //{
            //    return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
            //}
            //else
            //{
            //    this.Point = new Point(MyMouseHookStruct.pt.x, MyMouseHookStruct.pt.y);
            //    return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
            //}

            //MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 0);
            //MouseClickEvent(this, e);

        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void BMouseClick(int x, int y, int i = 0)
        {
            point.X = x;
            point.Y = y;
            SetCursorPos(point.X, point.Y);
            mouse_event(MouseEventFlag.LeftDown, 0, point.Y, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, point.Y, 0, UIntPtr.Zero);
            Thread.Sleep(i);
        }

        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void BDoubleClick(int x, int y, int i = 0)
        {
            point.X = x;
            point.Y = y;
            SetCursorPos(point.X, point.Y);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftDown | MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            //mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            //mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            //mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(i);
        }

        /// <summary>
        /// 右击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void BRightClick(int x, int y, int i = 0)
        {
            point.X = x;
            point.Y = y;
            SetCursorPos(point.X, point.Y);
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(i);
        }

        /// <summary>
        /// 发送文字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool BSendKeys(string str)
        {
            try
            {
                System.Windows.Forms.SendKeys.Send(str);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送文字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool BSendKeys(string str, int x, int y, int i = 0)
        {
            try
            {
                point.X = x;
                point.Y = y;
                SetCursorPos(point.X, point.Y);
                mouse_event(MouseEventFlag.LeftDown, 0, point.Y, 0, UIntPtr.Zero);
                mouse_event(MouseEventFlag.LeftUp, 0, point.Y, 0, UIntPtr.Zero);
                Thread.Sleep(i);
                System.Windows.Forms.SendKeys.Send(str);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        #region 属性
        /// <summary>
        /// 获取屏幕分辨率当前物理大小
        /// </summary>
        public static Size WorkingArea
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                Size size = new Size();
                size.Width = GetDeviceCaps(hdc, HORZRES);
                size.Height = GetDeviceCaps(hdc, VERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return size;
            }
        }
        /// <summary>
        /// 当前系统DPI_X 大小 一般为96
        /// </summary>
        public static int DpiX
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                int DpiX = GetDeviceCaps(hdc, LOGPIXELSX);
                ReleaseDC(IntPtr.Zero, hdc);
                return DpiX;
            }
        }
        /// <summary>
        /// 当前系统DPI_Y 大小 一般为96
        /// </summary>
        public static int DpiY
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                int DpiX = GetDeviceCaps(hdc, LOGPIXELSY);
                ReleaseDC(IntPtr.Zero, hdc);
                return DpiX;
            }
        }
        /// <summary>
        /// 获取真实设置的桌面分辨率大小
        /// </summary>
        public static Size DESKTOP
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                Size size = new Size();
                size.Width = GetDeviceCaps(hdc, DESKTOPHORZRES);
                size.Height = GetDeviceCaps(hdc, DESKTOPVERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return size;
            }
        }

        /// <summary>
        /// 获取宽度缩放百分比
        /// </summary>
        public static float ScaleX
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                int t = GetDeviceCaps(hdc, DESKTOPHORZRES);
                int d = GetDeviceCaps(hdc, HORZRES);
                float ScaleX = (float)GetDeviceCaps(hdc, DESKTOPHORZRES) / (float)GetDeviceCaps(hdc, HORZRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleX;
            }
        }
        /// <summary>
        /// 获取高度缩放百分比
        /// </summary>
        public static float ScaleY
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                float ScaleY = (float)(float)GetDeviceCaps(hdc, DESKTOPVERTRES) / (float)GetDeviceCaps(hdc, VERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleY;
            }
        }
        #endregion
    }
}
