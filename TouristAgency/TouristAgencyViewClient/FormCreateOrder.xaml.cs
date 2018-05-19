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
using Unity.Attributes;
using Unity;
using IvanAgencyService.Interfaces;
using IvanAgencyService.ViewModel;
using IvanAgencyService.BindingModel;

namespace IvanAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormCreateOrder.xaml
    /// </summary>
    public partial class FormCreateOrder : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClient serviceC;

        private readonly ITravel serviceT;

        private readonly IMain serviceM;


        public FormCreateOrder(IClient serviceC, ITravel serviceT, IMain serviceM)
        {
            InitializeComponent();
            Loaded += FormCreateOrder_Load;
            comboBoxProduct.SelectionChanged += comboBoxProduct_SelectedIndexChanged;
            comboBoxProduct.SelectionChanged += new SelectionChangedEventHandler(comboBoxProduct_SelectedIndexChanged);
            this.serviceC = serviceC;
            this.serviceT = serviceT;
            this.serviceM = serviceM;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<ClientViewModel> listClient = serviceC.GetList();
                if (listClient != null)
                {
                    comboBoxClient.DisplayMemberPath = "ClientFIO";
                    comboBoxClient.SelectedValuePath = "Id";
                    comboBoxClient.ItemsSource = listClient;
                    comboBoxProduct.SelectedItem = null;
                }
                List<TravelViewModel> listProduct = serviceT.GetList();
                if (listProduct != null)
                {
                    comboBoxProduct.DisplayMemberPath = "TravelName";
                    comboBoxProduct.SelectedValuePath = "Id";
                    comboBoxProduct.ItemsSource = listProduct;
                    comboBoxProduct.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxProduct.SelectedItem != null && !string.IsNullOrEmpty(textBoxDay.Text))
            {
                try
                {
                    int id = ((TravelViewModel)comboBoxProduct.SelectedItem).Id;
                    TravelViewModel product = serviceT.GetElement(id);
                    decimal day = Convert.ToDecimal(textBoxDay.Text);
                    textBoxSum.Text = (product.Price * day).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxDay_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDay.Text))
            {
                MessageBox.Show("Заполните поле Дни", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxClient.SelectedItem == null)
            {
                MessageBox.Show("Выберите себя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxProduct.SelectedItem == null)
            {
                MessageBox.Show("Выберите путешествие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceM.CreateOrder(new OrderBindingModel
                {
                    ClientId = ((ClientViewModel)comboBoxClient.SelectedItem).Id,
                    TravelId = ((TravelViewModel)comboBoxProduct.SelectedItem).Id,
                    Day = Convert.ToInt32(textBoxDay.Text),
                    Summa = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = false;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}