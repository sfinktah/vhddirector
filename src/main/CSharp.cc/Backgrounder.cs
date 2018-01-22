using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
// using System.Windows.Forms;



namespace CSharp.cc.Threading
{
    public class Backgrounder : System.ComponentModel.BackgroundWorker
    {
        public Backgrounder()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

            // DoWork += Backgrounder_DoWork;
        }

        private bool PrepareOutputFolder()
        {
            return true;
        }

        virtual public void Backgrounder_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!PrepareOutputFolder())
            {
                return;
            }



            if (CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            int count = 100;
            while (count-- > 0)
            {
                System.Threading.Thread.Sleep(1000);


                if (CanRaiseEvents)
                {
                    float percent = count * 100f;
                    int percentDone = Convert.ToInt32(percent);
                    ReportProgress(percentDone, "Astract Class");
                }
            }
            e.Result = true;

        }

    }
}

