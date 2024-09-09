using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FetchWeather
{
    public partial class Form1 : Form
    {
        private dynamic info = "";
        private string api = "d397a995d6b587ba479889ad3dc3cf56";
        private string path = @"weather.json";
        public Form1()
        {
            InitializeComponent();
        }
        private void FetchWeatherInfo()
        {
            string city = textBox1.Text.Trim();
            if (String.IsNullOrEmpty(city))
            {
                MessageBox.Show("Введите название города", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (WebClient webClient = new WebClient { Encoding=Encoding.UTF8}) {
                    string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={api}&lang=ru&units=metric";
                    var json = webClient.DownloadString(url);
                    info = JsonConvert.DeserializeObject<dynamic>(json);
                }
                this.Text = $"Погода в г. {city}";
            } catch
            {
                MessageBox.Show("Не удалось выполнить запрос", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (info.main.temp > 0) label2.Text += $"+{info.main.temp}°C";
            else label2.Text += $"{info.main.temp}°C";
            label3.Text += $"{info.main.humidity}%";
            label5.Text += $"{Math.Round(Convert.ToDouble(info.main.pressure) * 0.750064), 2} мм рт.ст.";
            label4.Text += $"{info.weather[0].description}";

            if (info.weather[0].icon == "01d") pictureBox1.Image = Properties.Resources._01d_2x;
            else if (info.weather[0].icon == "01n") pictureBox1.Image = Properties.Resources._01n_2x;
            else if (info.weather[0].icon == "02d") pictureBox1.Image = Properties.Resources._02d_2x;
            else if (info.weather[0].icon == "02n") pictureBox1.Image = Properties.Resources._02n_2x;
            else if (info.weather[0].icon == "03d") pictureBox1.Image = Properties.Resources._03d_2x;
            else if (info.weather[0].icon == "03n") pictureBox1.Image = Properties.Resources._03n_2x;
            else if (info.weather[0].icon == "04d") pictureBox1.Image = Properties.Resources._04d_2x;
            else if (info.weather[0].icon == "04n") pictureBox1.Image = Properties.Resources._04n_2x;
            else if (info.weather[0].icon == "09d") pictureBox1.Image = Properties.Resources._09d_2x;
            else if (info.weather[0].icon == "09n") pictureBox1.Image = Properties.Resources._09n_2x;
            else if (info.weather[0].icon == "10d") pictureBox1.Image = Properties.Resources._10d_2x;
            else if (info.weather[0].icon == "10n") pictureBox1.Image = Properties.Resources._10n_2x;
            else if (info.weather[0].icon == "11d") pictureBox1.Image = Properties.Resources._11d_2x;
            else if (info.weather[0].icon == "11n") pictureBox1.Image = Properties.Resources._11n_2x;
            else if (info.weather[0].icon == "13d") pictureBox1.Image = Properties.Resources._13d_2x;
            else if (info.weather[0].icon == "13n") pictureBox1.Image = Properties.Resources._13n_2x;
            else if (info.weather[0].icon == "50d") pictureBox1.Image = Properties.Resources._50d_2x;
            else if (info.weather[0].icon == "50n") pictureBox1.Image = Properties.Resources._50n_2x;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "Температура: ";
            label3.Text = "Влажность: ";
            label4.Text = "Состояние: ";
            label5.Text = "Давление: ";
            this.Text = "Погода";
            pictureBox1.Image = null;

            FetchWeatherInfo();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Действительно ли Вы планируете закрыть программу?", "Закрытие", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (res != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
            var save = MessageBox.Show("Сохранить результат в файл?", "Сохранение", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (save == DialogResult.Yes)
            {
                string prettyJson = JsonConvert.SerializeObject(info, Formatting.Indented);
                File.WriteAllText(Path.GetFullPath(path), prettyJson);
            }
        }
    }
}
