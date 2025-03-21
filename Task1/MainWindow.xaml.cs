using System;
using System.Linq;
using System.Windows;

namespace LocalDataStorageApp
{
    public partial class MainWindow : Window
    {
        
        private Person[] people = new Person[0];
        private int nextId = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            int age = int.TryParse(AgeTextBox.Text, out age) ? age : 0;
            string address = AddressTextBox.Text;

            
            Person newPerson = new Person(nextId++, name, age, address);

            
            Array.Resize(ref people, people.Length + 1);
            people[people.Length - 1] = newPerson;

            
            NameTextBox.Clear();
            AgeTextBox.Clear();
            AddressTextBox.Clear();
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            
            PersonsListBox.Items.Clear();
            foreach (var person in people)
            {
                PersonsListBox.Items.Add($"{person.Name}, {person.Age}, {person.Address}");
            }
        }

        private void SortByAgeButton_Click(object sender, RoutedEventArgs e)
        {
            
            Array.Sort(people, (x, y) => x.Age.CompareTo(y.Age));
            DisplayButton_Click(sender, e);
        }

        private void SortByNameButton_Click(object sender, RoutedEventArgs e)
        {
      
            Array.Sort(people, (x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
            DisplayButton_Click(sender, e); 
        }

        private void SearchByAgeButton_Click(object sender, RoutedEventArgs e)
        {
            int age = int.TryParse(AgeTextBox.Text, out age) ? age : 0;

            
            var foundPeople = people.Where(p => p.Age == age).ToArray();

            
            DisplaySearchResults(foundPeople);
        }

        private void SearchByNameButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;

         
            var foundPeople = people.Where(p => p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToArray();

            
            DisplaySearchResults(foundPeople);
        }




        private void DisplaySearchResults(Person[] foundPeople)
        {
            // Display search results
            PersonsListBox.Items.Clear();
            foreach (var person in foundPeople)
            {
                PersonsListBox.Items.Add($"{person.Name}, {person.Age}, {person.Address}");
            }
        }

        private void RemoveByNameButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;

           
            int index = Array.FindIndex(people, p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (index != -1)
            {
              
                for (int i = index; i < people.Length - 1; i++)
                {
                    people[i] = people[i + 1];
                }

                
                Array.Resize(ref people, people.Length - 1);
                DisplayButton_Click(sender, e); 
            }
            else
            {
                MessageBox.Show("Person not found!");
            }
        }

        private void RemoveByAgeButton_Click(object sender, RoutedEventArgs e)
        {
            int age = int.TryParse(AgeTextBox.Text, out age) ? age : 0;

            
            int index = Array.FindIndex(people, p => p.Age == age);

            if (index != -1)
            {
                
                for (int i = index; i < people.Length - 1; i++)
                {
                    people[i] = people[i + 1];
                }

               
                Array.Resize(ref people, people.Length - 1);
                DisplayButton_Click(sender, e); 
            }
            else
            {
                MessageBox.Show("Person not found!");
            }
        }
    }

    public struct Person
    {
        public int Id;
        public string Name;
        public int Age;
        public string Address;

        public Person(int id, string name, int age, string address)
        {
            Id = id;
            Name = name;
            Age = age;
            Address = address;
        }
    }
}
