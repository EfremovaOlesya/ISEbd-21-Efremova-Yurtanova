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
using System.Windows.Forms;
using System.Data;

namespace IvanAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormTravel.xaml
    /// </summary>
    public partial class FormTravel : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ITravel service;

        private int? id;

        private List<TravelTourViewModel> travelTours;

        public FormTravel(ITravel service)
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
                        textBoxPrice.Text = view.Price.ToString();
                        travelTours = view.TravelTours;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.InnerException.Message);
                    System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                System.Windows.MessageBox.Show(ex.InnerException.Message);
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (System.Windows.MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        travelTours.RemoveAt(dataGridViewProduct.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
            decimal sum = 0;
            for (int i = 0; i < travelTours.Count; i++)
            {               
                TravelTourViewModel product = travelTours[i];
                sum += Convert.ToDecimal(product.TourPrice);
            }
            textBoxPrice.Text = sum.ToString();

        }
       
            private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                System.Windows.MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (travelTours == null || travelTours.Count == 0)
            {
                System.Windows.MessageBox.Show("Выберите туры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                System.Windows.MessageBox.Show("Обновите, чтоб увидеть сумму", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<TravelTourBindingModel> productComponentBM = new List<TravelTourBindingModel>();
                for (int i = 0; i < travelTours.Count; ++i)
                {
                    productComponentBM.Add(new TravelTourBindingModel
                    {
                        Id = travelTours[i].Id,
                        TravelId = travelTours[i].TravelId,
                        TourId = travelTours[i].TourId,
                        TourPrice = travelTours[i].TourPrice
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new TravelBindingModel
                    {
                        Id = id.Value,
                        TravelName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        TravelTours = productComponentBM
                    });
                }
                else
                {
                    service.AddElement(new TravelBindingModel
                    {
                        TravelName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        TravelTours = productComponentBM
                    });
                }
                System.Windows.MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());

            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
