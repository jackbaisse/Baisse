using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsKeyboardHook;
using WindowsFormsKeyboardHook.entity;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<Form2dataEntity> form2DataEntity = new List<Form2dataEntity>();

        List<int> listX = new List<int>();//x坐标
        List<int> listY = new List<int>();//y坐标
        List<int> listType = new List<int>();//鼠标动作
        Point P = new Point();
        bool b = false;
        int i = 0;

        //Point p = new Point(1, 1);//定义存放获取坐标的point变量
        private float fX;
        private float fY;
        KeyboardHook kh;
        MouseHook mh;

        Dictionary<string, List<int>> dict = new Dictionary<string, System.Collections.Generic.List<int>>();
        private void Form1_Load(object sender, EventArgs e)
        {
            //Size size = new Size();
            //Size size1 = new Size();
            //size = MouseHook.WorkingArea;
            //float f1 = MouseHook.DpiX;
            //float f2 = MouseHook.DpiY;
            //size1 = MouseHook.DESKTOP;
            fX = MouseHook.ScaleX;//x获取比例
            fY = MouseHook.ScaleY;//y获取比例

            //this.WindowState = FormWindowState.Maximized;
            kh = new KeyboardHook();
            mh = new MouseHook();
            kh.UnHook();
            mh.UnHook();
            kh.SetHook();
            mh.SetHook();
            kh.OnKeyDownEvent += kh_OnKeyDownEvent;
            mh.MouseMoveEvent += mh_MouseMoveEvent;
            mh.MouseClickEvent += mh_MouseClickEvent;
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mh_MouseClickEvent(object sender, MouseEventArgs e)
        {
            if (!b) return;
            float c = e.Location.X / fX;
            float d = e.Location.Y / fY;
            int x = Convert.ToInt32(c);
            int y = Convert.ToInt32(d);
            if (e.Button == MouseButtons.Left)
            {
                b = false;
                listType.Add(0);
                listX.Add(x);
                listY.Add(y);
                i++;
                Form2dataEntity dataEntity = new Form2dataEntity()
                {
                    textBox1 = "备注" + i,
                    textBox2 = x,
                    textBox3 = y,
                    comboBox1 = 0.ToString(),
                    textBox4 = 3000,
                };
                Form2 form = new Form2(dataEntity);
                form.ShowDialog();
                form2DataEntity.Add(dataEntity);
                if (form.DialogResult == DialogResult.OK)
                {
                    int index = dg1.Rows.Add();
                    dg1.Rows[index].Cells["XH"].Value = index + 1;
                    dg1.Rows[index].Cells["BZ"].Value = dataEntity.textBox1;
                    dg1.Rows[index].Cells["ZBX"].Value = dataEntity.textBox2;
                    dg1.Rows[index].Cells["ZBY"].Value = dataEntity.textBox3;
                    dg1.Rows[index].Cells["DZ"].Value = dataEntity.comboBox1;
                    dg1.Rows[index].Cells["SJ"].Value = dataEntity.textBox4;
                }

            }
            if (e.Button == MouseButtons.Right)
            {
                listType.Add(1);
                listX.Add(x);
                listY.Add(y);
            }
            //if (e.Button == MouseButtons.Middle)
            //{
            //    listType.Add(2);
            //    listX.Add(x);
            //    listY.Add(y);
            //}
            //textBox2.Text = c + "、" + d;
        }
        //鼠标移动事件
        private void mh_MouseMoveEvent(object sender, MouseEventArgs e)
        {
            float c = e.Location.X / fX;
            float d = e.Location.Y / fY;
            int x = Convert.ToInt32(c);
            int y = Convert.ToInt32(d);
            P.X = x;
            P.Y = y;
            textBox1.Text = x + "、" + y;
        }
        //键盘按下事件
        private void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.S | Keys.Control)) { this.Show(); }//Ctrl+S显示窗口

            if (e.KeyData == (Keys.H | Keys.Control)) { this.Hide(); }//Ctrl+H隐藏窗口

            if (e.KeyData == (Keys.C | Keys.Control)) { this.Close(); }//Ctrl+C 关闭窗口 

            if (e.KeyData == (Keys.A | Keys.Control | Keys.Alt)) { this.Text = "你发现了什么？"; }//Ctrl+Alt+A
        }

        //结构体布局 本机位置
        [StructLayout(LayoutKind.Sequential)]
        struct NativeRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //将枚举作为位域处理
        [Flags]
        enum MouseEventFlag : uint //设置鼠标动作的键值
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

        //设置鼠标位置
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        //设置鼠标按键和动作
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo); //UIntPtr指针多句柄类型

        [DllImport("user32.dll")]
        static extern void keybd_event(byte vk, byte vsacn, int flag, int wram);

        [DllImport("user32.dll")]
        static extern void PostMessage(IntPtr hwnd, uint msg, int w, string l);
        [DllImport("user32.dll")]
        static extern void PostMessage(IntPtr hwnd, uint msg, int w, int l);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string strClass, string strWindow);

        //该函数获取一个窗口句柄,该窗口雷鸣和窗口名与给定字符串匹配 hwnParent=Null从桌面窗口查找
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string strClass, string strWindow);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(HandleRef hwnd, out NativeRECT rect);
        //获取当前鼠标位置
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        /// <summary>
        /// 截屏幕成为图像返回
        /// </summary>
        /// <returns></returns>
        public static Bitmap CaptureScreen()
        {
            //获得桌面窗口的上下文
            IntPtr desktopWindow = GetDesktopWindow();
            IntPtr desktopDC = GetDC(desktopWindow);
            //得到image的GDI句柄
            IntPtr desktopBitmap = GetCurrentObject(desktopDC, OBJ_BITMAP);
            //用句柄创建一个.NET图形对象
            Bitmap desktopImage = Image.FromHbitmap(desktopBitmap);
            //释放设备上下文
            ReleaseDC(desktopDC);
            return desktopImage;
        }

        public Bitmap GetScreen()
        {
            //获取整个屏幕图像,不包括任务栏
            Rectangle ScreenArea = Screen.GetWorkingArea(this);
            Bitmap bmp = new Bitmap(ScreenArea.Width, ScreenArea.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(ScreenArea.Width, ScreenArea.Height));
            }
            return bmp;
        }

        const int OBJ_BITMAP = 7;

        [DllImport("user32.dll")]
        private extern static IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private extern static IntPtr GetDC(IntPtr windowHandle);

        [DllImport("gdi32.dll")]
        private extern static IntPtr GetCurrentObject(IntPtr hdc, ushort objectType);

        [DllImport("user32.dll")]
        private extern static void ReleaseDC(IntPtr hdc);

        //定义变量
        const int AnimationCount = 80;
        //private Point endPosition;
        private int count;


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kh.UnHook();
            mh.UnHook();
        }



        /// <summary>
        /// 截图
        /// </summary>
        public void PrintScreen()
        {
            //获取到主显示器
            Screen scr = Screen.PrimaryScreen;
            //获取到它的边界
            Rectangle rc = scr.Bounds;
            //取出宽度
            int iWidth = rc.Width;
            //取出高度
            int iHeight = rc.Height;
            //创建一个和屏幕一样大的Bitmap            
            Image myImage = new Bitmap(iWidth, iHeight);
            //从一个继承自Image类的对象中创建Graphics对象            
            Graphics g = Graphics.FromImage(myImage);
            //抓屏并拷贝到myimage里            
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(iWidth, iHeight));
            //保存为文件            
            myImage.Save("D://cutscreen.jpg");
        }

        static void SendKey(string name, string l)
        {
            var win = FindWindow(null, name);

            keybd_event(0x01, 0, 0, 0);//激活TIM
            PostMessage(win, 0x0302, 0, 0);
            //PostMessage(win, 0x0101, new Random().Next(65,128),0);//发送字符                                              //下面是发送回车
            PostMessage(win, 0x0100, 13, 0);
            PostMessage(win, 0x0101, 13, 0);
            keybd_event(0x11, 0, 0x0002, 0);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            b = true;
            //Form2dataEntity dataEntity = new Form2dataEntity()
            //{
            //    textBox1 = "备注1",
            //    textBox2 = listX[0].ToString() + "," + listY[0].ToString(),
            //    textBox3 = listType[0].ToString(),
            //    textBox4 = "1000",
            //};
            //Form2 form = new Form2(dataEntity);
            //form.ShowDialog();



            //int count = listType.Count();
            //for (int i = 0; i < count; i++)
            //{
            //    //Thread.Sleep(5000);
            //    if (listType[i] == 0)
            //    {
            //        mh.BMouseClick(listX[i], listY[i]);
            //    }
            //    else if (listType[i] == 1)
            //    {
            //        mh.BRightClick(listX[i], listY[i]);
            //    }
            //    Thread.Sleep(1000);
            //}
            //listType.Clear();
            //listX.Clear();
            //listY.Clear();

            //mh.BMouseClick(157, 71);
            //GetWinScaling();

            //SendKeys.SendWait("A");

            var name = "游戏人生";//名称
            var t = "测试";//内容
            var Count = 5;//次数

            while (Count > -1)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
                Clipboard.SetText(t);
                SendKey(name, t);
                Count--;
                Console.WriteLine("测试次数" + Count);
            }
            //Point pc = BConvertzb(198, 87);
            string s = string.Empty;
            //BMouseClick(158,69);//单击
            //PrintScreen();
            //NativeRECT rect;
            //GetWindowRect(new HandleRef(this, ptrStartBtn), out rect);

            //AutoClick();

            //IntPtr ptr = FindWindow("", "微信");
            //NativeRECT rect;
            ////获取主窗体句柄
            //IntPtr ptrTaskbar = Student.GetMainWindowHandle(67688);
            ////IntPtr ptrTaskbar = FindWindow("0x002502c2", null);
            //if (ptrTaskbar == IntPtr.Zero)
            //{
            //    MessageBox.Show("No windows found!");
            //    return;
            //}
            ////获取窗体中"button1"按钮
            //IntPtr ptrStartBtn = FindWindowEx(ptrTaskbar, IntPtr.Zero, null, "查询");
            ////IntPtr ptrStartBtn = FindWindowEx(ptrTaskbar, IntPtr.Zero, null, "病历大文本数据提取");
            //if (ptrStartBtn == IntPtr.Zero)
            //{
            //    MessageBox.Show("No button found!");
            //    return;
            //}
            ////获取窗体大小
            //GetWindowRect(new HandleRef(this, ptrStartBtn), out rect);
            //endPosition.X = (rect.left + rect.right) / 2;
            //endPosition.Y = (rect.top + rect.bottom) / 2;
            ////判断点击按钮
            //if (checkBox1.Checked)
            //{
            //    //选择"查看鼠标运行的轨迹"
            //    this.count = AnimationCount;
            //    movementTimer.Start();
            //}
            //else
            //{
            //    SetCursorPos(endPosition.X, endPosition.Y);
            //    mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            //    mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            //    textBox1.Text = String.Format("{0},{1}", MousePosition.X, MousePosition.Y);
            //}

            ////const int BM_CLICK = 0xF5;
            //IntPtr maindHwnd = FindWindow(null, "QQ用户登录"); //获得QQ登陆框的句柄   
            //if (maindHwnd != IntPtr.Zero)
            //{
            //    IntPtr childHwnd = FindWindowEx(maindHwnd, IntPtr.Zero, null, "登录");   //获得按钮的句柄   
            //    if (childHwnd != IntPtr.Zero)
            //    {
            //        //SendMessage(childHwnd, BM_CLICK, 0, 0); //发送点击按钮的消息   
            //    }
            //    else
            //    {
            //        MessageBox.Show("没有找到子窗口");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("没有找到窗口");
            //}
        }

        public static void AutoClick()
        {
            while (true)
            {
                //设置鼠标的坐标

                SetCursorPos(72, 40);

                //这里模拟的是一个鼠标双击事件
                mouse_event((MouseEventFlag.LeftUp | MouseEventFlag.Absolute), 72, 40, 0, UIntPtr.Zero);
                mouse_event((MouseEventFlag.LeftDown | MouseEventFlag.Absolute), 72, 40, 0, UIntPtr.Zero);
                mouse_event((MouseEventFlag.LeftUp | MouseEventFlag.Absolute), 72, 40, 0, UIntPtr.Zero);
                mouse_event((MouseEventFlag.LeftDown | MouseEventFlag.Absolute), 72, 40, 0, UIntPtr.Zero);
            }
        }


        //Tick:定时器,每当经过多少时间发生函数
        private void movementTimer_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (form2DataEntity == null) return;
            foreach (Form2dataEntity item in form2DataEntity)
            {
                if (item.comboBox1 == "单击")
                {
                    mh.BMouseClick(item.textBox2, item.textBox3, item.textBox4);
                }
                else if (item.comboBox1 == "双击")
                {
                    mh.BDoubleClick(item.textBox2, item.textBox3, item.textBox4);
                }
                else if (item.comboBox1 == "右击")
                {
                    mh.BRightClick(item.textBox2, item.textBox3, item.textBox4);
                }
                else if (item.comboBox1 == "文字")
                {
                    string str = item.textBox1;
                    mh.BSendKeys(str, item.textBox2, item.textBox3);
                }
            }

            //System.Windows.Forms.SendKeys.Send("{RIGHT}" + "123");
            //System.Windows.Forms.SendKeys.Send("123");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dg1.Rows.Clear();
            form2DataEntity.Clear();

            var name = "游戏人生";//名称
            var t = "测试";//内容
            var Count = 5;//次数

            while (Count > -1)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
                Clipboard.SetText(t);
                SendKey(name, t);
                Count--;
                Console.WriteLine("测试次数" + Count);
            }
        }
    }
}

public static class Student
{
    private static class NativeMethods
    {
        internal const uint GW_OWNER = 4;

        internal delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);
    }

    public static IntPtr GetMainWindowHandle(int processId)
    {
        IntPtr MainWindowHandle = IntPtr.Zero;

        NativeMethods.EnumWindows(new NativeMethods.EnumWindowsProc((hWnd, lParam) =>
        {
            IntPtr PID;
            NativeMethods.GetWindowThreadProcessId(hWnd, out PID);

            if (PID == lParam &&
                NativeMethods.IsWindowVisible(hWnd) &&
                NativeMethods.GetWindow(hWnd, NativeMethods.GW_OWNER) == IntPtr.Zero)
            {
                MainWindowHandle = hWnd;
                return false;
            }

            return true;

        }), new IntPtr(processId));

        return MainWindowHandle;
    }

}
