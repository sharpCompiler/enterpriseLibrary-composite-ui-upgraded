using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;

namespace BankShellForm.Upgraded
{
    [SmartPart]
    public partial class UserControl1 : UserControl
    {
        [EventPublication("topic://BankShell/statusupdate", PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> UpdateStatusTextEvent;

        public UserControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateStatusTextEvent.Invoke(sender, new DataEventArgs<string>("test"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WorkItem rootWorkItem = new WorkItem();

            OrderController controller = rootWorkItem.Items.AddNew<OrderController>();

            rootWorkItem.Commands["SaveOrder"].Execute();

            rootWorkItem.Commands["DeleteOrder"].Execute();
        }
    }
}
