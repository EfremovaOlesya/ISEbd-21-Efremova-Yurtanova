﻿using IvanAgencyService.BindingModel;
using IvanAgencyService.Interfaces;
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
    public partial class FormReg : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IAdmin service;

        private int? id;

        public FormReg(IAdmin service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text))
            {
                MessageBox.Show("Заполните логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPass.Text))
            {
                MessageBox.Show("Придумайте пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }          
            try
            {
                string fio = textBoxLogin.Text;               
                string pass = textBoxPass.Text;               
                if (id.HasValue)
                {
                    service.UpdElement(new AdminBindingModel
                    {
                        Id = id.Value,
                        AdminFIO = textBoxLogin.Text,
                        Password = pass,                       
                    });
                }
                else
                {
                    service.AddElement(new AdminBindingModel
                    {
                        AdminFIO = textBoxLogin.Text,
                        Password = pass,                      
                    });
                }
                MessageBox.Show("Регистрация прошла успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {           
            Close();
        }



    }
}
