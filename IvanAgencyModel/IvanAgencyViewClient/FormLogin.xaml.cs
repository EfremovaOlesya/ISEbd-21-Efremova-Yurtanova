using System;
using System.Collections.Generic;
using System.Windows;
using Unity.Attributes;
using Unity;
using IvanAgencyService.ViewModel;
using IvanAgencyService.Interfaces;
namespace IvanAgencyViewClient
{
    public partial class FormLogin : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClient service;

        public FormLogin(IClient service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPass.Text))
            {
                MessageBox.Show("Заполните пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<ClientViewModel> list = service.GetList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ClientFIO == textBoxFIO.Text && list[i].Password == textBoxPass.Text)
                {
                    App.id = list[i].Id;                  
                    var form = Container.Resolve<FormMain>();
                    form.ShowDialog();                   
                }              
            }          
            textBoxFIO.Clear();
            textBoxPass.Clear();
            return;
        }
       
        private void buttonReg_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReg>();
            form.ShowDialog();
        }
    }
}
