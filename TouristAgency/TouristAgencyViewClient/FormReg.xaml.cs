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
using System.Text.RegularExpressions;

namespace IvanAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormReg.xaml
    /// </summary>
    public partial class FormReg : Window
    {       
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IClient service;

        private int? id;

        public FormReg(IClient service)
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
                MessageBox.Show("Придумайте пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMail.Text))
            {
                MessageBox.Show("Введите майл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                string fio = textBoxFIO.Text;
                string mail = textBoxMail.Text;
                string pass = textBoxPass.Text;
                if (!string.IsNullOrEmpty(mail))
                {
                    if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                    {
                        MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if(pass.Length>8)
                    {
                        MessageBox.Show("Пароль должен содержать меньше 8 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }             
                if (id.HasValue)
                {
                    service.UpdElement(new ClientBindingModel
                    {
                        Id = id.Value,
                        ClientFIO = textBoxFIO.Text,
                        Password = pass,
                        Mail = mail
                });
                }
                else
                {
                    service.AddElement(new ClientBindingModel
                    {
                        ClientFIO = textBoxFIO.Text,
                        Password = pass,
                        Mail = mail
                    });
                }
                MessageBox.Show("Регистрация прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);               
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }



    }
}
