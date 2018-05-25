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
    public partial class FormAddPunishment : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IMain serviceM;

        private int? id;


        public FormAddPunishment(IMain serviceM)
        {
            InitializeComponent();
            this.serviceM = serviceM;

        }

        private void FormAddPunishment_Load(object sender, EventArgs e)
        {
           
           

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPunishment.Text))
            {
                MessageBox.Show("Заполните поле бонусы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.AddPunishment(new OrderBindingModel
                {
                    
                    
                    Id = id.Value,
                    Punishment = Convert.ToInt32(textBoxPunishment.Text)
                });
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
