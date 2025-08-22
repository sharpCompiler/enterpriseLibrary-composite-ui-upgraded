using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.ObjectBuilder;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;

namespace BankShellForm.Upgraded
{
    public partial class Form1 : Form
    {
        private List<string> customers = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        [State("customers")]
        public List<string> Customers
        {
            set { customers = value; }
        }

        [InjectionConstructor]
        public Form1(WorkItem workItem, IWorkItemTypeCatalogService workItemTypeCatalog)
            : this()
        {

            //this.workItem = workItem;
            //this.workItemTypeCatalog = workItemTypeCatalog;
        }


        [CommandHandler("FileExit")]
        public void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        [CommandHandler("HelpAbout")]
        public void OnHelpAbout(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Bank Teller QuickStart Version 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        [EventSubscription("topic://BankShell/statusupdate", Thread = ThreadOption.UserInterface)]
        public void OnStatusUpdate(object sender, DataEventArgs<string> e)
        {
            this.Text = e.Data;
        }
    }
}
