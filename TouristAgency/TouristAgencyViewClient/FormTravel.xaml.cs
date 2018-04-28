using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для FormTravel.xaml
    /// </summary>
    public partial class FormTravel : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ITravelService service;

        private int? id;

        private List<TravelTourViewModel> travelTours;

        public FormTravel(ITravelService service)
        {
            InitializeComponent();
            Loaded += FormTravel_Load;
            this.service = service;
        }

        private void FormTravel_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                   TravelViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                       textBoxName.Text = view.TravelName;
                        textBoxPrice.Text = view.PriceTravel.ToString();
                        travelTours = view.TravelTours;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                travelTours = new List<TravelTourViewModel>();
        }

        private void LoadData()
        {
            try
            {
                if (travelTours != null)
                {
                    dataGridViewProduct.ItemsSource = null;
                    dataGridViewProduct.ItemsSource = travelTours;
                    dataGridViewProduct.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewProduct.Columns[1].Visibility = Visibility.Hidden;
                    dataGridViewProduct.Columns[2].Visibility = Visibility.Hidden;
                    dataGridViewProduct.Columns[3].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTravelTour>();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                        form.Model.TravelId = id.Value;
                    travelTours.Add(form.Model);
                }
                LoadData();
            }
        }
        
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewProduct.SelectedItem != null)
            {
                var form = Container.Resolve<FormTravelTour>();
                form.Model = travelTours[dataGridViewProduct.SelectedIndex];
                if (form.ShowDialog() == true)
                {
                    travelTours[dataGridViewProduct.SelectedIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewProduct.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        travelTours.RemoveAt(dataGridViewProduct.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
          
            if (travelTours == null || travelTours.Count == 0)
            {
                MessageBox.Show("Выбирете туры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<TravelTourBindingModel> travelTourBM = new List<TravelTourBindingModel>();
                for (int i = 0; i < travelTours.Count; ++i)
                {
                    travelTourBM.Add(new TravelTourBindingModel
                    {
                        Id = travelTours[i].Id,
                        TravelId = travelTours[i].TravelId,
                        TourId = travelTours[i].TourId,
                        Count = travelTours[i].Price
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new TravelBindingModel
                    {
                        Id = id.Value,
                        TravelName = textBoxName.Text,
                        PriceTravel = Convert.ToInt32(textBoxPrice.Text),
                        TravelTours = travelTourBM
                    });
                }
                else
                {
                    service.AddElement(new TravelBindingModel
                    {
                        TravelName = textBoxName.Text,         
                        TravelTours = travelTourBM
                    });
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