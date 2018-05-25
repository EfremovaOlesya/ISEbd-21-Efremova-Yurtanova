using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
namespace IvanAgencyViewClient
{
    /// <summary>
    /// Логика взаимодействия для FormLogin.xaml
    /// </summary>
    public partial class FormLogin : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public FormLogin()
        {
            InitializeComponent();
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

            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-J57HGAF\SQLEXPRESS;Initial Catalog=IvanSuDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Clients where ClientFIO = '" + textBoxFIO.Text + "' and Password = '" + textBoxPass.Text + "'", conn);          
            SqlDataReader dt;
            dt = cmd.ExecuteReader();
            int count = 0;
            
            while(dt.Read())
            {
                count += 1; 
                
            }
           if(count ==1)
            {
                Close();
                var form = Container.Resolve<FormMain>();
                form.ShowDialog();
               
              
            }
            else
            {
                MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }



    }
}
