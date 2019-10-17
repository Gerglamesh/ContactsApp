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
using System.IO;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for AddContactWindow.xaml
    /// </summary>
    public partial class AddContactWindow : Window
    {
        MainWindow mainWindow;

        public TextBox firstNameTxtBox;
        public TextBox lastNameTxtBox;
        public TextBox birthDateTxtBox;
        public TextBox phoneTxtBox;

        public TextBox streetTxtBox;
        public TextBox houseNumTxtBox; //Really necessary?
        public TextBox zipCodeTxtBox;
        public TextBox cityTxtBox;
        public TextBox countryTxtBox;

        Grid mainGrid;
        Grid adressGrid;

        string contactToEdit;

        Button addBtn;
        Button cancelBtn;

        public AddContactWindow(MainWindow mainWindow, Contact contact = null)
        {
            SizeToContent = SizeToContent.WidthAndHeight;
            this.mainWindow = mainWindow;

            InitializeComponent();
            Start();

            if (contact != null)
            {
                contactToEdit = contact.ToString();
                LoadContact(contact);
            }
        }

        private void Start()
        {
            mainGrid = (Grid)Content;

            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            //Define controls for main grid
            Label header = new Label()
            {
                Content = "New Contact",
                FontSize = 16,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            Grid.SetColumnSpan(header, 4);

            Label fName = CreateLabel("First Name", "Right");
            firstNameTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label lName = CreateLabel("Last Name", "Right");
            lastNameTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label birthDate = CreateLabel("Birthday", "Right");
            birthDateTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };
            birthDateTxtBox.LostFocus += BirthDateValidityCheck;

            Label phoneNum = CreateLabel("Phone", "Right");
            phoneTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            //Add controls to main grid
            AddToCell(header, mainGrid, 0, 0);
            AddToCell(fName, mainGrid, 0, 1);
            AddToCell(firstNameTxtBox, mainGrid, 1, 1);
            AddToCell(lName, mainGrid, 0, 2);
            AddToCell(lastNameTxtBox, mainGrid, 1, 2);
            AddToCell(birthDate, mainGrid, 0, 3);
            AddToCell(birthDateTxtBox, mainGrid, 1, 3);
            AddToCell(phoneNum, mainGrid, 0, 4);
            AddToCell(phoneTxtBox, mainGrid, 1, 4);


            //Add address section
            GroupBox adressGroupBox = new GroupBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Header = "Adress Information",
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Gray,
                Margin = new Thickness(5)
            };
            adressGrid = new Grid();
            adressGroupBox.Content = adressGrid;

            //Define adress grid
            adressGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            adressGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            adressGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            adressGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            adressGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            adressGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            adressGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            //Add address group to main grid and set span
            AddToCell(adressGroupBox, mainGrid, 0, 5);
            Grid.SetColumnSpan(adressGroupBox, 4);

            //Define controls for adress group
            Label streetName = CreateLabel("Street", "Right");
            streetTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label houseNum = CreateLabel("House Number", "Right");
            houseNumTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label zipCode = CreateLabel("Zip Code", "Right");
            zipCodeTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };
            zipCodeTxtBox.LostFocus += ZipCodeValidityCheck;

            Label city = CreateLabel("City", "Right");
            cityTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label country = CreateLabel("Country", "Right");
            countryTxtBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            //Add controls to address group
            AddToCell(streetName, adressGrid, 0, 0);
            AddToCell(streetTxtBox, adressGrid, 1, 0);
            AddToCell(houseNum, adressGrid, 2, 0);
            AddToCell(houseNumTxtBox, adressGrid, 3, 0);
            AddToCell(zipCode, adressGrid, 0, 1);
            AddToCell(zipCodeTxtBox, adressGrid, 1, 1);
            AddToCell(city, adressGrid, 2, 1);
            AddToCell(cityTxtBox, adressGrid, 3, 1);
            AddToCell(country, adressGrid, 0, 2);
            AddToCell(countryTxtBox, adressGrid, 1, 2);

            //Add buttons to main grid bottom
            addBtn = new Button()
            {
                Content = "Add Contact",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5),
                Padding = new Thickness(5)
            };
            AddToCell(addBtn, mainGrid, 0, 6);
            addBtn.Width = 100;
            Grid.SetColumnSpan(addBtn, 2);
            addBtn.Click += AddBtn_Click;

            cancelBtn = new Button()
            {
                Content = "Cancel",
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(5),
                Padding = new Thickness(5)
            };
            AddToCell(cancelBtn, mainGrid, 4, 6);
            cancelBtn.Width = 100;
            cancelBtn.Click += CancelBtn_Click;

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            mainWindow.addBtn.IsEnabled = true;
            mainWindow.editBtn.IsEnabled = true;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (zipCodeTxtBox.BorderBrush == Brushes.Red ||
                birthDateTxtBox.BorderBrush == Brushes.Red ||
                zipCodeTxtBox.Text == "" || birthDateTxtBox.Text == "")
            {
                MessageBox.Show("Not all text boxes contain valid information.\n" +
                    "Please revise contact information and try again!");
            }
            else
            {
                if (contactToEdit != null)
                {
                    ContactsInformation.RemoveContact(contactToEdit);
                }
                Contact contact = new Contact();
                contact.firstName = firstNameTxtBox.Text;
                contact.lastName = lastNameTxtBox.Text;
                contact.birthDate = DateTime.Parse(birthDateTxtBox.Text);
                contact.phoneNumber = phoneTxtBox.Text;
                contact.address.street = streetTxtBox.Text;
                contact.address.houseNumber = houseNumTxtBox.Text;
                contact.address.zipCode = int.Parse(zipCodeTxtBox.Text);
                contact.address.city = cityTxtBox.Text;
                contact.address.country = countryTxtBox.Text;

                ContactsInformation.AddContact(contact);
                mainWindow.UpdateContactList();
                InitializeBoxes();
            }
        }

        private void LoadContact(Contact contact)
        {
            firstNameTxtBox.Text = contact.firstName;
            lastNameTxtBox.Text = contact.lastName;
            birthDateTxtBox.Text = contact.birthDate.ToString("yyyy/MM/dd");
            phoneTxtBox.Text = contact.phoneNumber;
            streetTxtBox.Text = contact.address.street;
            houseNumTxtBox.Text = contact.address.houseNumber;
            zipCodeTxtBox.Text = contact.address.zipCode.ToString();
            cityTxtBox.Text = contact.address.city;
            countryTxtBox.Text = contact.address.country;
        }

        private void InitializeBoxes()
        {
            foreach (TextBox textBox in mainGrid.Children.OfType<TextBox>())
            {
                textBox.Text = "";
            }
            foreach (TextBox textBox in adressGrid.Children.OfType<TextBox>())
            {
                textBox.Text = "";
            }
        }

        private void ZipCodeValidityCheck(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(zipCodeTxtBox.Text, out int result))
            {
                zipCodeTxtBox.BorderBrush = Brushes.Red;
            }
            else
            {
                zipCodeTxtBox.BorderBrush = Brushes.Gray;
            }
        }

        private void BirthDateValidityCheck(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(birthDateTxtBox.Text, out DateTime result2))
            {
                birthDateTxtBox.BorderBrush = Brushes.Red;
            }
            else
            {
                birthDateTxtBox.BorderBrush = Brushes.Gray;
            }
        }

        /// <summary>
        /// Places an UIElement in specific grid cell based on supplied column and row.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void AddToCell(UIElement control, Grid grid, int column, int row)
        {
            grid.Children.Add(control);
            Grid.SetColumn(control, column);
            Grid.SetRow(control, row);
        }

        /// <summary>
        /// Creates a new label
        /// </summary>
        /// <param name="content"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public Label CreateLabel(string content, string alignment = "Left")
        {
            HorizontalAlignment align = new HorizontalAlignment();
            if (alignment.ToLower() == "right")
            {
                align = HorizontalAlignment.Right;
            }
            else if (alignment.ToLower() == "center")
            {
                align = HorizontalAlignment.Center;
            }
            else
            {
                align = HorizontalAlignment.Left;
            }

            return new Label()
            {
                Content = content,
                HorizontalContentAlignment = align,
                Margin = new Thickness(5)
            };
        }
    }
}
