using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConvertFilesToFormat.Classes
{

    public class ControlWriter : TextWriter
    {
        private Control textbox;
        public ControlWriter(Control textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            if (value != 13)
            {
                textbox.SynchronizedInvoke(() => textbox.Text = textbox.Text + value);
            }
        }

        public override void Write(string value)
        {
            textbox.SynchronizedInvoke(() => textbox.Text += value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }

}
