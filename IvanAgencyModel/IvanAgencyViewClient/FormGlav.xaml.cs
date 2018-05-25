using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity.Attributes;
using Unity;
namespace IvanAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormGlav.xaml
    /// </summary>
    public partial class FormGlav : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public FormGlav()
        {
            InitializeComponent();
        }
        private void buttonReg_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReg>();
            form.ShowDialog();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormLogin>();
            form.ShowDialog();          
            Close();
        }
    }
}
