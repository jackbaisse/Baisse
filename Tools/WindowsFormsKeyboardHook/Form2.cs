using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsKeyboardHook.entity;

namespace WindowsFormsKeyboardHook
{
    public partial class Form2 : Form
    {
        Form2dataEntity form2Data;
        public Form2()
        {
        }

        public Form2(Form2dataEntity form2DataEntity)
        {
            InitializeComponent();
            AddCombobox();
            this.MaximizeBox = Form2Entity.MaximizeBox;
            this.MinimizeBox = Form2Entity.MinimizeBox;
            this.Font = new Font("宋体", 8, FontStyle.Regular);
            this.textBox1.Text = form2DataEntity.textBox1;
            this.textBox2.Text = form2DataEntity.textBox2.ToString();
            this.textBox3.Text = form2DataEntity.textBox3.ToString();
            this.comboBox1.Text = form2DataEntity.comboBox1;
            this.textBox4.Text = form2DataEntity.textBox4.ToString();
            form2Data = form2DataEntity;
        }

        public void AddCombobox()
        {
            ArrayList mylist = new ArrayList();
            mylist.Add(new DictionaryEntry("1", "单击"));
            mylist.Add(new DictionaryEntry("2", "双击"));
            mylist.Add(new DictionaryEntry("3", "右击"));
            mylist.Add(new DictionaryEntry("4", "文字"));
            comboBox1.DataSource = mylist;
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            form2Data.textBox1 = this.textBox1.Text;
            try
            {
                form2Data.textBox2 = Convert.ToInt32(this.textBox2.Text);
                form2Data.textBox3 = Convert.ToInt32(this.textBox3.Text);
                form2Data.textBox4 = Convert.ToInt32(this.textBox4.Text);
            }
            catch (Exception)
            {
                form2Data.textBox2 = 0;
                form2Data.textBox3 = 0;
                form2Data.textBox4 = 0;
            }
            form2Data.comboBox1 = this.comboBox1.Text;
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
