using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaoPao.Winform
{
    /// <summary>
    /// winform窗体控件自动随窗体缩放大小
    /// </summary>
    public class FormAutoResize
    {
        /// <summary>
        /// Form窗体X坐标
        /// </summary>
        public float X { set; get; }
        /// <summary>
        ///  Form窗体Y坐标
        /// </summary>
        public float Y { set; get; }

        //获取控件的width,height,left,top,字体的大小值，存放在控件的tag属性中
        public void SetTag(Control cons)
        {
            //遍历窗体中的控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    SetTag(con);
                }
            }
        }
        //根据窗体大小调整控件大小 
        public void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件tag属性值，并分割后存储字符串数组
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的宽度值
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)a;//高度

                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)a;//左边缘距离
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)a;//上边缘距离
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;
                //con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }

            }
        }
        public void form_Resize(Form fr)
        {
            float newx = (fr.Width) / X;
            float newy = (fr.Height) / Y;
            setControls(newx, newy, fr);
            //fr.Text = fr.Width.ToString() + " " + fr.Height.ToString();
        }
    }
}
