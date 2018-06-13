using IvanAgencyService.BindingModel;
using IvanAgencyService.Interfaces;
using IvanAgencyService.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
namespace IvanAgencyViewAdmin
{
    public partial class FormBonus : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMain service;

        public int Id { set { id = value; } }

        private int? id;

        public FormBonus(IMain service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormBonus_Load(object sender, EventArgs e)
        {
            try
            {
                if (id.HasValue)
                {

                    OrderViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxSumm.Text = view.Summa.ToString();
                       
                    }                 
                }
                else
                {
                    MessageBox.Show("Не указана заявка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxBonus.Text))
            {
                MessageBox.Show("Введите бонус", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          
            if (!string.IsNullOrEmpty(textBoxBonus.Text))
            {
                try
                {                    
                    int bon = Convert.ToInt32(textBoxBonus.Text);
                    int summ = Decimal.ToInt32(Convert.ToDecimal(textBoxSumm.Text));
                    if (bon > 50)
                    {
                        MessageBox.Show("Одумайтесь, скидка слишком велика ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    service.BonusOrder(new OrderBindingModel
                        {
                            Id = id.Value,
                            Bonus = summ * bon / 100,
                        });
                                       
                    MessageBox.Show("Бонус начислен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
