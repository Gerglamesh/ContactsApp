using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for AddContactWindow.xaml
    /// </summary>
    public partial class AddContactWindow : Window
    {
        MainWindow mainWindow;

        public TextBox firstName;
        public TextBox lastName;
        public TextBox birthDate;
        public TextBox phone;

        public TextBox street;
        public TextBox houseNum;
        public TextBox zipCode;
        public TextBox city;
        public TextBox country;

        Grid mainGrid;
        Grid adressGrid;

        string contactToEdit;

        Button add;
        Button cancel;

        public AddContactWindow(MainWindow mainWindow, Person contact = null)
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
            firstName = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label lName = CreateLabel("Last Name", "Right");
            lastName = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label birthDate = CreateLabel("Birthday", "Right");
            this.birthDate = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };
            this.birthDate.LostFocus += BirthDateValidityCheck;

            Label phoneNum = CreateLabel("Phone", "Right");
            phone = new TextBox()
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
            AddToCell(firstName, mainGrid, 1, 1);
            AddToCell(lName, mainGrid, 0, 2);
            AddToCell(lastName, mainGrid, 1, 2);
            AddToCell(birthDate, mainGrid, 0, 3);
            AddToCell(this.birthDate, mainGrid, 1, 3);
            AddToCell(phoneNum, mainGrid, 0, 4);
            AddToCell(phone, mainGrid, 1, 4);


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
            street = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label houseNum = CreateLabel("House Number", "Right");
            this.houseNum = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label zipCode = CreateLabel("Zip Code", "Right");
            this.zipCode = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };
            this.zipCode.LostFocus += ZipCodeValidityCheck;

            Label city = CreateLabel("City", "Right");
            this.city = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };

            Label country = CreateLabel("Country", "Right");
            this.country = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5),
                MinWidth = 100,
                MaxWidth = 200
            };


            //Add controls to address group
            AddToCell(streetName, adressGrid, 0, 0);
            AddToCell(street, adressGrid, 1, 0);
            AddToCell(houseNum, adressGrid, 2, 0);
            AddToCell(this.houseNum, adressGrid, 3, 0);
            AddToCell(zipCode, adressGrid, 0, 1);
            AddToCell(this.zipCode, adressGrid, 1, 1);
            AddToCell(city, adressGrid, 2, 1);
            AddToCell(this.city, adressGrid, 3, 1);
            AddToCell(country, adressGrid, 0, 2);
            AddToCell(this.country, adressGrid, 1, 2);


            //Add buttons to main grid bottom
            add = new Button()
            {
                Content = "Add Contact",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5),
                Padding = new Thickness(5)
            };
            AddToCell(add, mainGrid, 0, 6);
            add.Width = 100;
            Grid.SetColumnSpan(add, 2);
            add.Click += AddBtn_Click;

            cancel = new Button()
            {
                Content = "Cancel",
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(5),
                Padding = new Thickness(5)
            };
            AddToCell(cancel, mainGrid, 4, 6);
            cancel.Width = 100;
            cancel.Click += CancelBtn_Click;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            mainWindow.add.IsEnabled = true;
            mainWindow.edit.IsEnabled = true;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (zipCode.BorderBrush == Brushes.Red ||
                birthDate.BorderBrush == Brushes.Red ||
                zipCode.Text == "" || birthDate.Text == "")
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
                Person contact = new Person();
                contact.FirstName = firstName.Text;
                contact.LastName = lastName.Text;
                contact.BirthDate = DateTime.Parse(birthDate.Text);
                contact.PhoneNumber = phone.Text;
                contact.Address.Street = street.Text;
                contact.Address.HouseNumber = houseNum.Text;
                contact.Address.ZipCode = int.Parse(zipCode.Text);
                contact.Address.City = city.Text;
                contact.Address.Country = country.Text;

                ContactsInformation.AddContact(contact);
                mainWindow.UpdateContactsList();
                InitializeBoxes();
            }
        }

        private void LoadContact(Person contact)
        {
            firstName.Text = contact.FirstName;
            lastName.Text = contact.LastName;
            birthDate.Text = contact.BirthDate.ToString("yyyy/MM/dd");
            phone.Text = contact.PhoneNumber;
            street.Text = contact.Address.Street;
            houseNum.Text = contact.Address.HouseNumber;
            zipCode.Text = contact.Address.ZipCode.ToString();
            city.Text = contact.Address.City;
            country.Text = contact.Address.Country;
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
            if (!int.TryParse(zipCode.Text, out int result))
            {
                zipCode.BorderBrush = Brushes.Red;
            }
            else
            {
                zipCode.BorderBrush = Brushes.Gray;
            }
        }

        private void BirthDateValidityCheck(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(birthDate.Text, out DateTime result2))
            {
                birthDate.BorderBrush = Brushes.Red;
            }
            else
            {
                birthDate.BorderBrush = Brushes.Gray;
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
