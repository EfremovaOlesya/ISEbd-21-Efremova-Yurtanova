using System;
using System.Collections.Generic;
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
using Unity;
using Unity.Attributes;
using System.Data;
using TouristAgencyService.Interfaces;
using TouristAgencyService.ViewModel;

namespace TouristAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormMain.xaml
    /// </summary>
    public partial class FormMain : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMainService service;

        public FormMain(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                List<OrderViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                    dataGridView.Columns[0].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Visibility = Visibility.Hidden;
                    dataGridView.Columns[2].Visibility = Visibility.Hidden;
                    dataGridView.Columns[3].Visibility = Visibility.Hidden;
                    dataGridView.Columns[4].Visibility = Visibility.Hidden;
                    dataGridView.Columns[5].Visibility = Visibility.Hidden;
                    dataGridView.Columns[7].Visibility = Visibility.Hidden;
                    dataGridView.Columns[8].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }      

        private void путешествияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTravels>();
            form.ShowDialog();
        }      

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }

       

        private void buttonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedItem != null)
            {
                var form = Container.Resolve<FormPayOrder>();
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
