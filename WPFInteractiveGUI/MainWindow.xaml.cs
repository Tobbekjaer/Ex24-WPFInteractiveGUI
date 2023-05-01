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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WPFInteractiveGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller controller;
        public MainWindow()
        {
            InitializeComponent();

            controller = new Controller();

            if(controller.PersonCount != 0) {
                personCount.IsEnabled = false;
                index.IsEnabled = false;

            }
        }

        private void NewPersonbtn_Click(object sender, RoutedEventArgs e)
        {
            // isEnabled changed to true, when newPersonbtn is clicked
            fnTxtBox.IsEnabled = true;
            lnTxtBox.IsEnabled = true;
            ageTxtBox.IsEnabled = true;
            tlfTxtBox.IsEnabled = true;
            deletePersonbtn.IsEnabled = true;
            upbtn.IsEnabled = true;
            downbtn.IsEnabled = true;

            // Moves focus to first name textbox
            fnTxtBox.Focus();

            if (NewPersonbtn_Click != null) {

                try {
                    
                    // If the textboxes contains something, append the text to the current persons respective property
                    //if (fnTxtBox.Text.Length > 0 && lnTxtBox.Text.Length > 0 && ageTxtBox.Text.Length > 0 && tlfTxtBox.Text.Length > 0) {
                    string firstName = fnTxtBox.Text;
                    string lastName = lnTxtBox.Text;
                    int age = int.Parse(ageTxtBox.Text);
                    string tlf = tlfTxtBox.Text;

                    // Calls AddPerson()
                    controller.AddPerson();

                    controller.CurrentPerson.FirstName = firstName;
                    controller.CurrentPerson.LastName = lastName;
                    controller.CurrentPerson.Age = age;
                    controller.CurrentPerson.TelephoneNo = tlf;
                    
                    //}
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    ShowCurrentPerson.IsEnabled = true;
                    fnTxtBox.Clear();
                    lnTxtBox.Clear();
                    ageTxtBox.Clear();
                    tlfTxtBox.Clear();
                    newPersonbtn.Content = "New Person";

                    // Updating Person Count label and Person Index label everytime a new person is added
                    string count = Convert.ToString(controller.PersonCount);
                    personCount.Content = $"Person Count [ {count} ]";
                    string personIndex = Convert.ToString(controller.PersonIndex);
                    index.Content = $"Index [ {personIndex} ]";

                }
            }

        }

        private void AppendPerson(object sender, RoutedEventArgs e)
        {
            if (controller.CurrentPerson != null) {
                try {
                    fnTxtBox.Text = controller.CurrentPerson.FirstName;
                    lnTxtBox.Text = controller.CurrentPerson.LastName;
                    string age = Convert.ToString(controller.CurrentPerson.Age);
                    ageTxtBox.Text = age;
                    tlfTxtBox.Text = controller.CurrentPerson.TelephoneNo;

                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void DeletePersonbtn_Click(object sender, RoutedEventArgs e)
        {
            // Deletes CurrentPerson if repository is not empty
            controller.DeletePerson();
            AppendPerson(sender, e);

            // Updating Person Count label and Person Index label everytime a new person is deleted
            string count = Convert.ToString(controller.PersonCount);
            personCount.Content = $"Person Count [ {count} ]";
            string personIndex = Convert.ToString(controller.PersonIndex);
            index.Content = $"Index [ {personIndex} ]";

            if (controller.PersonCount == 0) {
                controller.DeletePerson();
                ShowCurrentPerson.IsEnabled = false;
                fnTxtBox.Clear();
                lnTxtBox.Clear();
                ageTxtBox.Clear();
                tlfTxtBox.Clear();
                newPersonbtn.Content = "New Person";

            }
            
        }


        // When text is changed in a TextBox the content for the currentPerson is opdated in the Controller()
        private void fnTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Hvis man starter med at skrive, ændrer button sig til submit person. 
            if (fnTxtBox.Text.Length > 0) {
                newPersonbtn.Content = "Submit Person";
            }
            //if (controller.CurrentPerson != null) {
            //    controller.CurrentPerson.FirstName = fnTxtBox.Text;
            //}
            
        }

        private void lnTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (controller.CurrentPerson != null) {
            //    controller.CurrentPerson.LastName = lnTxtBox.Text;
            //}
            
        }

        private void ageTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (controller.CurrentPerson != null) {
            //    int age = int.Parse(ageTxtBox.Text);
            //    controller.CurrentPerson.Age = age;
            //}
            
        }

        private void tlfTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (controller.CurrentPerson != null) {
            //    controller.CurrentPerson.TelephoneNo = tlfTxtBox.Text;
            //}
            
        }

        // When Up button is clicked NextPerson() is called 
        private void Upbtn_Click(object sender, RoutedEventArgs e)
        {
            controller.NextPerson();
            AppendPerson(sender, e);

            // Updating Person Index label everytime a person is moved up
            string personIndex = Convert.ToString(controller.PersonIndex);
            index.Content = $"Index [ {personIndex} ]";
        }

        // When Down button is clicked PrevPerson() is called 
        private void Downbtn_Click(object sender, RoutedEventArgs e)
        {
            controller.PrevPerson();
            AppendPerson(sender, e);

            // Updating Person Index label everytime a person is moved down
            string personIndex = Convert.ToString(controller.PersonIndex);
            index.Content = $"Index [ {personIndex} ]";
        }

        private void ShowCurrentPerson_Click(object sender, RoutedEventArgs e)
        {
            AppendPerson(sender, e);

            // Updating Person Index label everytime the Current Person is shown
            if (controller.CurrentPerson != null) {
                int updatedIndex = controller.PersonIndex - 1;
                string personIndex = Convert.ToString(updatedIndex);
                index.Content = $"Index [ {personIndex} ]";
            }
            else {
                string personIndex = Convert.ToString(controller.PersonIndex);
                index.Content = $"Index [ {personIndex} ]";
            }
        }



    }
}
