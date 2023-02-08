using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Everydaynik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, CRUD
    {
        private string PATH = "C:\\Users\\gastf\\source\\repos\\Everydaynik\\Everydaynik\\data.json";
        private DateTime DisplayDate = DateTime.Now.Date;
        public MainWindow()
        {
            if (!File.Exists(PATH))
            {
                File.Create(PATH);
            }
            InitializeComponent();

            DP.DisplayDate = DisplayDate;
            DP.Text = DP.DisplayDate.ToString();
            Read(DisplayDate);
        }
        private void Clear()
        {
            textboxName.Text = "Название записки...";
            descTextBox.Text = "Описание...";
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try//ломаетца говно
            {
                List<DataClass> allNotes = datashow.Items.Cast<DataClass>().ToList();//это пипец
                                                                                     //var a = allNotes.IndexOf(datashow.SelectedItem as DataClass);
                textboxName.Text = (datashow.SelectedItem as DataClass).Name;
                descTextBox.Text = (datashow.SelectedItem as DataClass).Description;
            }
            catch (Exception ex)
            {
                datashow.SelectedItem = null;
            }

        }

        public void Create(string Name, string Desc)
        {
            DataClass newData = new DataClass(DateOnly.FromDateTime(DP.DisplayDate), Name, Desc);
            List<DataClass> allNotes = SuperCringe.getInfo<List<DataClass>>();
            allNotes.Add(newData);
            SuperCringe.SaveInfo(allNotes);
            Clear();
            Read(DisplayDate);
        }

        public void Update()
        {
            List<DataClass> allNotes = datashow.Items.Cast<DataClass>().ToList();
            if (datashow.SelectedItem != null)
            {
                var pointer = datashow.SelectedItem as DataClass;
                int index = allNotes.IndexOf(pointer);
                if(textboxName.Text != "")
                {
                    DataClass newdata = new DataClass(pointer.Date, textboxName.Text, descTextBox.Text);
                    allNotes[index] = newdata;
                }
                else
                {
                    MessageBox.Show("Вы не ввели имя!");
                }

            }
            else
            {
                MessageBox.Show("Вы не выбрали записку, которую хотите изменить!");
            }
            SuperCringe.SaveInfo(allNotes);
            datashow.SelectedItem = null;//кароче я вообще хз но он у меня выкидывает ошибку мол selected item не найдет или не объявлен, я пишу что selecteditem = null и все равно ошибка, в итоге бахнул через костыль если знаете как решить - памагите
            Clear();
            Read(DP.DisplayDate);
        }

        public void Delete()
        {
            List<DataClass> allNotes = datashow.Items.Cast<DataClass>().ToList();
            if (datashow.SelectedItem != null)
            {
                var pointer = datashow.SelectedItem as DataClass;
                int index = allNotes.IndexOf(pointer);
                allNotes.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Вы не выбрали записку, которую хотите удалить!");
            }
            Clear();
            SuperCringe.SaveInfo(allNotes);
            Read(DP.DisplayDate);
        }

        public void Read(DateTime Displate)
        {
            List<DataClass> allNotes = SuperCringe.getInfo<List<DataClass>>();
            List<DataClass> notesforday = new List<DataClass>();
            foreach(DataClass item in allNotes)
            {
                if(item.Date == DateOnly.FromDateTime(Displate))
                {
                    notesforday.Add(item);
                }
            }
            datashow.ItemsSource = notesforday;
            datashow.DisplayMemberPath = "Name";
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((e.OriginalSource as Button).Name == "createButton") {
                if (textboxName.Text == "" || textboxName.Text == "Название записки...")
                {
                    MessageBox.Show("Вы ввели пустое имя!");
                }
                else
                {
                    if (descTextBox.Text == "Описание...") { Create(textboxName.Text, ""); }
                    else
                    {
                        Create(textboxName.Text, descTextBox.Text);
                    }
                }
            }
            else if((e.OriginalSource as Button).Name == "changeButton")
            {
                Update();
            }
            else if((e.OriginalSource as Button).Name == "DeleteButton")
            {
                Delete();
            }
        }

        private void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((e.OriginalSource as TextBox).Text == "" || (e.OriginalSource as TextBox).Text == "Название записки..." || (e.OriginalSource as TextBox).Text == "Описание...")
            {
                (e.OriginalSource as TextBox).Text = "";
            }
        }

        private void textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if((e.OriginalSource as TextBox).Name == "textboxName" && (e.OriginalSource as TextBox).Text == "")
            {
                textboxName.Text = "Название записки...";
            }
            else if((e.OriginalSource as TextBox).Name == "descTextBox" && (e.OriginalSource as TextBox).Text == "")
            {
                descTextBox.Text = "Описание...";
            }
        }

        private void DP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayDate = DP.DisplayDate;
            Read(DisplayDate);
        }
    }
}
