using OrdersManager.Library;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace WinFormOrdersManager
{
    public partial class OrdersManagerForm : Form
    {
        List<Order> orders = new List<Order>();

        public OrdersManagerForm()
        {
            InitializeComponent();
            LoadOrders();
            LoadSampleData();
            RefreshScreen();
        }

        private void LoadSampleData()
        {
            if (orders.Count < 1)
            {
                SqliteDataAccess.AddOrder(new Order { Address = "White Oak, 1989  Coulter Lane", Delivery = new DateTime(2020, 11, 25), Receiver = "Dustin Brown" });
                SqliteDataAccess.AddOrder(new Order { Address = "Denver, 2178  Logan Lane", Delivery = new DateTime(2020, 10, 15), Receiver = "Linda Hill" });
                SqliteDataAccess.AddOrder(new Order { Address = "COMSTOCK, 3494  Loving Acres Road", Delivery = new DateTime(2021, 09, 18), Receiver = "Janice Torres" });
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            orders = SqliteDataAccess.LoadOrders();
        }

        private void RefreshScreen()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = orders;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            TextBoxesToOrderMapping(order);
            SqliteDataAccess.AddOrder(order);
            LoadOrders();
            RefreshScreen();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var selectedOrder = GetSelectedOrder();
            SqliteDataAccess.DeleteOrder(selectedOrder);
            LoadOrders();
            RefreshScreen();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedOrder = GetSelectedOrder();
            OrderToTextBoxesMapping(selectedOrder);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var selectedOrder = GetSelectedOrder();
            TextBoxesToOrderMapping(selectedOrder);
            selectedOrder.Edited = DateTime.UtcNow;
            SqliteDataAccess.EditOrder(selectedOrder);
            LoadOrders();
            RefreshScreen();
        }

        private void TextBoxesToOrderMapping(Order order)
        {
            bool deliveryIsValid = DateTime.TryParse(textBoxDelivery.Text, out DateTime delivery);
            if (deliveryIsValid)
                order.Delivery = delivery;
            bool quantityIsValid = int.TryParse(textBoxQuantity.Text, out int quantity);
            if (quantityIsValid)
                order.Quantity = quantity;
            order.Address = textBoxAddress.Text;
            order.Receiver = textBoxReceiver.Text;
            order.SelfPickUp = checkBoxSelfPickUp.Checked;
        }

        private void OrderToTextBoxesMapping(Order selectedOrder)
        {
            textBoxDelivery.Text = selectedOrder.Delivery.ToString();
            textBoxAddress.Text = selectedOrder.Address;
            textBoxQuantity.Text = selectedOrder.Quantity.ToString();
            textBoxReceiver.Text = selectedOrder.Receiver;
            checkBoxSelfPickUp.Checked = selectedOrder.SelfPickUp;
        }

        private Order GetSelectedOrder()
        {
            return dataGridView.SelectedRows[0].DataBoundItem as Order;
        }
    }
}
