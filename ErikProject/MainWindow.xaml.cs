using System.Windows;
using System.Collections.Generic;

namespace ErikProject
{
    public partial class MainWindow : Window
    {
        private DatabaseHelper db = new DatabaseHelper();
        private List<Employee> workers;
        private List<Vacation> holidays;

        public MainWindow()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            workers = db.GetAllWorkers();
            EmployeesGrid.ItemsSource = workers;

            holidays = db.GetAllVacations();
            VacationsGrid.ItemsSource = holidays;
        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            var window = new EmployeeEditDialog();
            if (window.ShowDialog() == true && db.AddWorker(window.Employee))
                RefreshData();
        }

        private void EditWorker_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is Employee worker)
            {
                var window = new EmployeeEditDialog(worker);
                if (window.ShowDialog() == true && db.UpdateWorker(window.Employee))
                    RefreshData();
            }
            else
                MessageBox.Show("Выберите сотрудника");
        }

        private void DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem is Employee worker)
            {
                if (MessageBox.Show($"Удалить {worker.FullName}?", "Подтвердите", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    if (db.DeleteWorker(worker.EmployeeID))
                        RefreshData();
            }
            else
                MessageBox.Show("Выберите сотрудника");
        }

        private void AddHoliday_Click(object sender, RoutedEventArgs e)
        {
            var window = new VacationEditDialog(workers);
            if (window.ShowDialog() == true && db.AddVacation(window.Vacation))
                RefreshData();
        }

        private void EditHoliday_Click(object sender, RoutedEventArgs e)
        {
            if (VacationsGrid.SelectedItem is Vacation holiday)
            {
                var window = new VacationEditDialog(workers, holiday);
                if (window.ShowDialog() == true && db.UpdateVacation(window.Vacation))
                    RefreshData();
            }
            else
                MessageBox.Show("Выберите отпуск");
        }

        private void DeleteHoliday_Click(object sender, RoutedEventArgs e)
        {
            if (VacationsGrid.SelectedItem is Vacation holiday)
            {
                if (MessageBox.Show($"Удалить отпуск {holiday.EmployeeName}?", "Подтвердите", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    if (db.DeleteVacation(holiday.VacationID))
                        RefreshData();
            }
            else
                MessageBox.Show("Выберите отпуск");
        }
    }
}