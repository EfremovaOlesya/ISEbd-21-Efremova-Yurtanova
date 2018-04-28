using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TouristAgencyService.Interfaces;
using TouristAgencyService.ViewModel;
using Unity;
using Unity.Attributes;

namespace TouristAgencyView
{
    public partial class FormTravelTour : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public TravelTourViewModel Model { set { model = value; } get { return model; } }

        private readonly ITourService service;

        private TravelTourViewModel model;

        public FormTravelTour(ITourService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormProductComponent_Load(object sender, EventArgs e)
        {
            try
            {
                List<TourViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxComponent.DisplayMember = "TourName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = list;
                    comboBoxComponent.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxComponent.Enabled = false;
                comboBoxComponent.SelectedValue = model.TourId;
                textBoxCount.Text = model.Price.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
