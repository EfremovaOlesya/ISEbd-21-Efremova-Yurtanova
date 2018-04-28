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
using TouristAgencyService.Interfaces;
using TouristAgencyService.ViewModel;
using Unity;
using Unity.Attributes;

namespace TouristAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormTravelTour.xaml
    /// </summary>
    public partial class FormTravelTour : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public TravelTourViewModel Model { set { model = value; } get { return model; } }

        private readonly ITourService service;

        private TravelTourViewModel model;

        public FormTravelTour(ITourService service)
        {
            InitializeComponent();
            Loaded += FormTravelTour_Load;
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
                textBoxCount.Text = model.Price.ToString();
            }
        }

        private void CalcSum()
        {
            if (comboBoxComponent.SelectedItem != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = ((TourViewModel)comboBoxComponent.SelectedItem).Id;
                    TourViewModel tour = service.GetElement(id);                   
                    textBoxCount.Text = (tour.PriceTour).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
            try
            {
                if (model == null)
                {
                    model = new TravelTourViewModel
                    {
                        TourId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        TourName = comboBoxComponent.Text,
                        Price = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Price = Convert.ToInt32(textBoxCount.Text);
                }
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

