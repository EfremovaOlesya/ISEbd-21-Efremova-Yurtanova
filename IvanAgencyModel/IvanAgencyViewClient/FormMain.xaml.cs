using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity.Attributes;
using Unity;
using IvanAgencyService.Interfaces;
using IvanAgencyService.ViewModel;
using IvanAgencyService.BindingModel;
using Microsoft.Win32;
using System.IO;
namespace IvanAgencyViewClient
{
    public partial class FormMain : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
      
        private readonly IMain service;

        private readonly ISerialize serviceS;

        private readonly IReport reportService;

        public int Id { set { id = value; } }

        private int? id;

        public FormMain(IMain service, IReport reportService, ISerialize serviceS)
        {
            InitializeComponent();
            this.service = service;
            this.reportService = reportService;
            this.serviceS = serviceS;
        }

        private void LoadData()
        {
            try
            {
                List<OrderViewModel> list = service.GetList(App.id);
                if (list != null)
                {
                    dataGridViewMain.ItemsSource = list;
                    dataGridViewMain.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewMain.Columns[1].Visibility = Visibility.Hidden;                 
                    dataGridViewMain.Columns[3].Visibility = Visibility.Hidden;
                    dataGridViewMain.Columns[6].Visibility = Visibility.Hidden;
                    dataGridViewMain.Columns[5].Visibility = Visibility.Hidden;
                    dataGridViewMain.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
     
        private void путешествияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTravels>();
            form.ShowDialog();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                var form = Container.Resolve<FormCreateOrder>();
                form.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());

            }
        }
      
        private void buttonPay_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.SelectedItem != null)
            {                
                var form = Container.Resolve<FormPay>();
                form.Id = ((OrderViewModel)dataGridViewMain.SelectedItem).Id;
                form.ShowDialog();
                LoadData();
            }
        }       
        private void buttonRef_Click(object sender, EventArgs e)
        {           
                LoadData();           
        }

        private void списокТуровToolStripMenuItem_Click(object sender, EventArgs e)
        {           
                try
                {
                    reportService.SaveTourPriceW(new ReportBindingModel
                    {});
                    reportService.SaveTourPriceE(new ReportBindingModel
                    {});

                var form = Container.Resolve<FormLetter>();
                form.ShowDialog();
            }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }        
        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClientOrders>();
            form.ShowDialog();
        }

        private void buttonBonus_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.SelectedItem != null)
            {
                OrderViewModel view = service.GetElement(((OrderViewModel)dataGridViewMain.SelectedItem).Id);
                if (view != null)
                {
                    decimal x = view.Summa;
                    decimal y = view.Bonus;
                   service.UpdateOrder(new OrderBindingModel
                {
                    Id = ((OrderViewModel)dataGridViewMain.SelectedItem).Id,
                    Summa = x - y                   
                });
                }
                
                LoadData();
            }
        }

        private void buttonBec_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Json files (*.json)|*.json|Word files (*.doc)|*.doc" };
            if (sfd.ShowDialog() == true)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(sfd.FileName);

                    writer.WriteLine(serviceS.GetData());
                    writer.Dispose();

                    MessageBox.Show("Бэкап БД проведен успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

