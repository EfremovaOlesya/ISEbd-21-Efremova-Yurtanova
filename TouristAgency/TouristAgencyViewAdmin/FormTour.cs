﻿using System;
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
using TouristAgencyService.Interfaces;
using TouristAgencyService.ViewModel;
using TouristAgencyService.BindingModel;

namespace TouristAgencyView
{
    public partial class FormTour : Form
    {
            [Dependency]
            public new IUnityContainer Container { get; set; }

            public int Id { set { id = value; } }

            private readonly ITourService service;

            private int? id;

            public FormTour(ITourService service)
            {
                InitializeComponent();
                this.service = service;
            }

            private void FormComponent_Load(object sender, EventArgs e)
            {
                if (id.HasValue)
                {
                    try
                    {
                        TourViewModel view = service.GetElement(id.Value);
                        if (view != null)
                        {
                            textBoxName.Text = view.TourName;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            private void buttonSave_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(textBoxName.Text))
                {
                    MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    if (id.HasValue)
                    {
                        service.UpdElement(new TourBindingModel
                        {
                            Id = id.Value,
                            TourName = textBoxName.Text
                        });
                    }
                    else
                    {
                        service.AddElement(new TourBindingModel
                        {
                            TourName = textBoxName.Text
                        });
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

