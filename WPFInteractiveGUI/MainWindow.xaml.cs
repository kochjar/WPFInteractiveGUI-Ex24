using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

using System.Diagnostics;


namespace WPFInteractiveGUI
{

    public partial class MainWindow : Window
    {
        private Controller controller;
        public MainWindow()
        {
            InitializeComponent();

            controller = new Controller();
        }

        private void UpdateLabels()
        {
            labelPersonCount.Content = $"Person Count: {controller.PersonCount}";
            labelPersonIndex.Content = $"Index: {controller.PersonIndex}";
        }

        private bool ignoreTextBoxChanged = false;
        private void NewPersonClick(object sender, RoutedEventArgs e)
        {
            tbFirstName.IsEnabled = true;
            tbLastName.IsEnabled = true;
            tbAge.IsEnabled = true;
            tbPhone.IsEnabled = true;
            btnDeletePerson.IsEnabled = true;
            btnDown.IsEnabled = true;
            btnUp.IsEnabled = true;  
            controller.AddPerson();
            UpdateLabels();
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbAge.Text = "";
            tbPhone.Text = "";
        }
        
        private void DeletePersonClick(object sender, RoutedEventArgs e)
        {
            controller.DeletePerson();
            UpdateLabels();

            if (controller.PersonCount == 0)
            {
                ignoreTextBoxChanged = true;
                tbFirstName.Text = "";
                tbLastName.Text = "";
                tbAge.Text = "";
                tbPhone.Text = "";
                ignoreTextBoxChanged = false;
                tbFirstName.IsEnabled = false;
                tbLastName.IsEnabled = false;
                tbAge.IsEnabled = false;
                tbPhone.IsEnabled = false;
                btnDeletePerson.IsEnabled = false;
                btnDown.IsEnabled = false;
                btnUp.IsEnabled = false;
                
            } else
            {
                GetPerson();
            }
        }

        private void TextBoxChanged(object sender, RoutedEventArgs e)
        {
            if (!ignoreTextBoxChanged)
            {
                SetPerson();
                UpdateLabels();
            }
        }

        private void GetPerson()
        {
            ignoreTextBoxChanged = true;
            tbFirstName.Text = controller.CurrentPerson.FirstName;
            tbLastName.Text = controller.CurrentPerson.LastName;
            tbAge.Text = controller.CurrentPerson.Age.ToString();
            tbPhone.Text = controller.CurrentPerson.TelephoneNo;
            ignoreTextBoxChanged = false;
        }

        private void SetPerson() {
            controller.CurrentPerson.FirstName = tbFirstName.Text;
            controller.CurrentPerson.LastName = tbLastName.Text;
            int age;
            if (int.TryParse(tbAge.Text, out age))
            { controller.CurrentPerson.Age = age; } 
            else 
            { controller.CurrentPerson.Age = 0; }
           
            controller.CurrentPerson.TelephoneNo = tbPhone.Text;
        }


        private void NextPersonClick(object sender, RoutedEventArgs e)
        {
            controller.NextPerson();
            GetPerson();
            UpdateLabels();
        }

        private void PrevPersonClick(object sender, RoutedEventArgs e)
        {
            controller.PrevPerson();
            GetPerson();
            UpdateLabels();
        }

    }
}
