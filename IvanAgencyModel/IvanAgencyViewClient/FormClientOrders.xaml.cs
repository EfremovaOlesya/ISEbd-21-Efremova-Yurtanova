
using IvanAgencyService.BindingModel;
using IvanAgencyService.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows;
using Unity;
using Unity.Attributes;
namespace IvanAgencyViewClient
{
    public partial class FormClientOrders : System.Windows.Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IReport service;

        public FormClientOrders(IReport service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                reportViewer.LocalReport.ReportEmbeddedResource = "IvanAgencyViewClient.Report1.rdlc";
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                            "c " + Convert.ToDateTime(dateTimePickerFrom.SelectedDate).ToString("dd-MM") +
                                            " по " + Convert.ToDateTime(dateTimePickerTo.SelectedDate).ToString("dd-MM"));
                reportViewer.LocalReport.SetParameters(parameter);
                            
                var dataSource = service.GetClientOrders(App.id, new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.SelectedDate,
                    DateTo = dateTimePickerTo.SelectedDate
                });
                 ReportDataSource source = new ReportDataSource("DataSet", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);                                         
                reportViewer.RefreshReport();
                    
                    service.SaveClientOrders(App.id, new ReportBindingModel
                    {                       
                        DateFrom = dateTimePickerFrom.SelectedDate,
                        DateTo = dateTimePickerTo.SelectedDate
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }      
        private void buttonMail_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormLetterPDF>();
            form.ShowDialog();
        }
        }
}