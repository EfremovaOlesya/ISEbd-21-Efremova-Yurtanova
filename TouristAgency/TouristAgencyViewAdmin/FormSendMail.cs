using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IvanAgencyViewAdmin
{
    public partial class FormSendMail : Form
    {
        public static string file;
        public FormSendMail()
        {
            InitializeComponent();
        }

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fo = new OpenFileDialog();
            fo.ShowDialog();
            file = fo.FileName;

        }

        private void buttonSendEmail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxClientEmail.Text))
            {
                MessageBox.Show("Заполните Email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SmtpClient client = new SmtpClient("smtp.yandex.ru", 25);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("arina.yurtanova@yandex.ru", "05061998");
            string from = "arina.yurtanova@yandex.ru";
            string mail = textBoxClientEmail.Text;
            if (!string.IsNullOrEmpty(mail))
            {
                if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            string subject = "Туристическое агенство";
            String text = "Уважаемый, клиент! Просим оплатить ваши путешествия";
            MailMessage message = new MailMessage(from, mail, subject, text);
            try
            {
                Attachment sendfile = new Attachment(file);
                message.Attachments.Add(sendfile);

            }
            catch
            {
                MessageBox.Show("Файл не выбран ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {

                client.Send(message);
                MessageBox.Show("Письмо отправлено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
    }
}
