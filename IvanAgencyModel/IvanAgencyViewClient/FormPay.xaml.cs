using IvanAgencyService.BindingModel;
using IvanAgencyService.Interfaces;
using IvanAgencyService.ViewModel;
using System;
using System.Windows;
using Unity;
using Unity.Attributes;
namespace IvanAgencyViewClient
{
    public partial class FormPay : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMain service;

        public int Id { set { id = value; } }

        private int? id;

        public FormPay(IMain service)
        {
            InitializeComponent();
            Loaded += FormPay_Load;
            this.service = service;
        }

        private void FormPay_Load(object sender, EventArgs e)
        {
            try
            {
                if (id.HasValue)
                {
                    OrderViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxSumm.Text = view.Summa.ToString();
                        textBoxSumma.Text = view.SummaOplaty.ToString();
                    }
                    decimal summ = Convert.ToDecimal(textBoxSumm.Text);
                    decimal summa = Convert.ToDecimal(textBoxSumma.Text);
                    textBoxDop.Text = (summ - summa).ToString();
                }
                else
                {
                    MessageBox.Show("Не указана заявка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDopOp.Text))
            {
                MessageBox.Show("Введите сумму оплаты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!string.IsNullOrEmpty(textBoxDopOp.Text))
            {
                try
                {
                    decimal dop = Convert.ToDecimal(textBoxDop.Text);
                    decimal dopop = Convert.ToDecimal(textBoxDopOp.Text);

                    if (dop > dopop)
                    {
                        service.PayOrder(new OrderBindingModel
                        {
                            Id = id.Value,
                            SummaOplaty = Convert.ToDecimal(textBoxSumma.Text) + dopop,
                            Status = "Оплачен_частично"
                        });
                    }
                    if (dop < dopop)
                    {
                        MessageBox.Show("Вы переплатили", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        textBoxDopOp.Clear();
                        return;
                    }

                    if (dop == dopop)
                    {
                        service.PayOrder(new OrderBindingModel
                        {
                            Id = id.Value,
                            SummaOplaty = Convert.ToDecimal(textBoxSumma.Text) + dopop,
                            Status = "Оплачен"
                        });
                    }
                    MessageBox.Show("Оплата прошла успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
