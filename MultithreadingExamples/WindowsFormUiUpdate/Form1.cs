using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormUiUpdate
{
    public partial class Form1 : Form
    {
        private SynchronizationContext uiContext;

        public Form1()
        {
            InitializeComponent();

            uiContext = WindowsFormsSynchronizationContext.Current;
        }

        private void SimpleInvokeExample()
        {
            for (int i = 0; i < Width; i += 10)
            {
                Invoke(new MethodInvoker(() =>
                {
                    pictureBoxRed.Left = i;
                }));

                Thread.Sleep(100);
            }
        }

        private void SynchronizationContextExample()
        {
            for (int i = 0; i < Width; i += 10)
            {
                uiContext.Post(new SendOrPostCallback((parameter) =>
                {
                    pictureBoxBlue.Left = (int)parameter;
                }), i);

                Thread.Sleep(100);
            }
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            var blueThread = new Thread(SynchronizationContextExample);
            blueThread.Start();
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            var redThread = new Thread(SimpleInvokeExample);
            redThread.Start();
        }
    }
}
