using System.Windows;

namespace ErikProject
{
    public partial class EmployeeEditDialog : Window
    {
        public Employee Employee { get; private set; }

        public EmployeeEditDialog()
        {
            InitializeComponent();
            Employee = new Employee();
            Title = "Добавление сотрудника";
        }

        public EmployeeEditDialog(Employee employee) : this()
        {
            Employee = employee;
            FullNameTextBox.Text = employee.FullName;
            PositionTextBox.Text = employee.Position;
            DepartmentComboBox.Text = employee.Department;
            Title = "Редактирование сотрудника";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО сотрудника", "Ошибка", MessageBoxButton.OK);
                return;
            }

            Employee.FullName = FullNameTextBox.Text;
            Employee.Position = PositionTextBox.Text;
            Employee.Department = DepartmentComboBox.Text;

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}