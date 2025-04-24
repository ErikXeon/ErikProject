using System.Windows;
using System.Collections.Generic;

namespace ErikProject
{
    public partial class VacationEditDialog : Window
    {
        public Vacation Vacation { get; private set; }
        private List<Employee> workers;

        public VacationEditDialog(List<Employee> workers, Vacation vacation = null)
        {
            InitializeComponent();
            this.workers = workers;
            EmployeeComboBox.ItemsSource = workers;
            Vacation = vacation ?? new Vacation();

            if (vacation != null)
            {
                EmployeeComboBox.SelectedValue = vacation.EmployeeID;
                StartDatePicker.SelectedDate = vacation.StartDate;
                EndDatePicker.SelectedDate = vacation.EndDate;
                TypeComboBox.Text = vacation.VacationType;
                Title = "Изменить отпуск";
            }
            else
            {
                Title = "Добавить отпуск";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeComboBox.SelectedItem == null ||
                StartDatePicker.SelectedDate == null ||
                EndDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            Vacation.EmployeeID = ((Employee)EmployeeComboBox.SelectedItem).EmployeeID;
            Vacation.StartDate = StartDatePicker.SelectedDate.Value;
            Vacation.EndDate = EndDatePicker.SelectedDate.Value;
            Vacation.VacationType = TypeComboBox.Text;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}