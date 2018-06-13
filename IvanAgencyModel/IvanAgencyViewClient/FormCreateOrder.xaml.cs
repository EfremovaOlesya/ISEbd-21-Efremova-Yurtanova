using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity.Attributes;
using Unity;
using IvanAgencyService.Interfaces;
using IvanAgencyService.ViewModel;
using IvanAgencyService.BindingModel;
namespace IvanAgencyViewClient
{
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
                ClientViewModel Client = serviceC.GetElement(App.id);
                if (Client != null)
                {

                    textBoxClient.Text = Client.ClientFIO;
                    textBoxClient.IsEnabled=false;
              
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
            if (textBoxClient.Text == null)
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
                    ClientId = App.id,
                    TravelId = ((TravelViewModel)comboBoxProduct.SelectedItem).Id,
                    Day = Convert.ToInt32(textBoxDay.Text),
                    Summa = Convert.ToDecimal(textBoxSum.Text),
                    Status = "Не_оплачен"
                });
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}