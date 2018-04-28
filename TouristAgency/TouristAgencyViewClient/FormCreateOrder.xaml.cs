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
using TouristAgencyService.BindingModel;
using TouristAgencyService.Interfaces;
using TouristAgencyService.ViewModel;
using Unity;
using Unity.Attributes;


namespace TouristAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormCreateOrder.xaml
    /// </summary>
    public partial class FormCreateOrder : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClientService serviceC;

        private readonly ITravelService serviceT;

        private readonly IMainService serviceM;

        public FormCreateOrder(IClientService serviceC, ITravelService serviceP, IMainService serviceM)
        {
            InitializeComponent();
            Loaded += FormCreateOrder_Load;
            comboBoxTravel.SelectionChanged += comboBoxTravel_SelectedIndexChanged;

            comboBoxTravel.SelectionChanged += new SelectionChangedEventHandler(comboBoxTravel_SelectedIndexChanged);
            this.serviceC = serviceC;
            this.serviceT = serviceP;
            this.serviceM = serviceM;
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<ClientViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxClient.DisplayMemberPath = "ClientFIO";
                    comboBoxClient.SelectedValuePath = "Id";
                    comboBoxClient.ItemsSource = listC;
                    comboBoxTravel.SelectedItem = null;
                }
                List<TravelViewModel> listT = serviceT.GetList();
                if (listT != null)
                {
                    comboBoxTravel.DisplayMemberPath = "TravelName";
                    comboBoxTravel.SelectedValuePath = "Id";
                    comboBoxTravel.ItemsSource = listT;
                    comboBoxTravel.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxTravel.SelectedItem != null)
            {
                try
                {
                    int id = ((TravelViewModel)comboBoxTravel.SelectedItem).Id;
                    TravelViewModel travel = serviceT.GetElement(id);                 
                    textBoxSum.Text = (travel.PriceTravel).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
       
        private void comboBoxTravel_SelectedIndexChanged(object sender, EventArgs e)
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
            if (string.IsNullOrEmpty(textBoxChildren.Text))
            {
                MessageBox.Show("Заполните поле Дети", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxAdults.Text))
            {
                MessageBox.Show("Заполните поле Взрослые", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxClient.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxTravel.SelectedItem == null)
            {
                MessageBox.Show("Выберите путешествие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                serviceM.CreateOrder(new OrderBindingModel
                {
                    ClientId = ((ClientViewModel)comboBoxClient.SelectedItem).Id,
                    TravelId = ((TravelViewModel)comboBoxTravel.SelectedItem).Id,                    
                    Summ = Convert.ToInt32(textBoxSum.Text)
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
            DialogResult = false;
            Close();
        }
    }
}
