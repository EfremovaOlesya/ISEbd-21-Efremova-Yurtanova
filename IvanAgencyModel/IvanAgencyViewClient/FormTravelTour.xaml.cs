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
using IvanAgencyService.ViewModel;
using IvanAgencyService.Interfaces;

namespace IvanAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormTravelTour.xaml
    /// </summary>
    public partial class FormTravelTour : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public TravelTourViewModel Model { set { model = value; } get { return model; } }

        private readonly ITour service;

        private TravelTourViewModel model;

        public FormTravelTour(ITour service)
        {
            InitializeComponent();
            Loaded += FormTravelTour_Load;
            comboBoxComponent.SelectionChanged += comboBoxComponent_SelectedIndexChanged;

            comboBoxComponent.SelectionChanged += new SelectionChangedEventHandler(comboBoxComponent_SelectedIndexChanged);
            this.service = service;
        }

        private void FormTravelTour_Load(object sender, EventArgs e)
        {
            List<TourViewModel> list = service.GetList();
            try
            {
                if (list != null)
                {
                    comboBoxComponent.DisplayMemberPath = "TourName";
                    comboBoxComponent.SelectedValuePath = "Id";
                    comboBoxComponent.ItemsSource = list;
                    comboBoxComponent.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (model != null)
            {
                comboBoxComponent.IsEnabled = false;
                foreach (TourViewModel item in list)
                {
                    if (item.TourName == model.TourName)
                    {
                        comboBoxComponent.SelectedItem = item;
                    }
                }
            }
        }

        private void CalcSum()
        {
            try
            {
                int id = ((TourViewModel)comboBoxComponent.SelectedItem).Id;
                TourViewModel product = service.GetElement(id);
                textBoxCount.Text = product.PriceTour.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void comboBoxComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (comboBoxComponent.SelectedItem == null)
            {
                MessageBox.Show("Выберите тур", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (textBoxCount.Text == null)
            {
                MessageBox.Show("Укажите цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new TravelTourViewModel
                    {
                        TourId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        TourName = comboBoxComponent.Text,
                        TourPrice = Convert.ToDecimal(textBoxCount.Text)
                    };
                }
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                MessageBox.Show(ex.InnerException.Message);
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