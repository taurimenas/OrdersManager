using OrdersManager.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            RefreshScreen();
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

            bool deliveryIsValid = DateTime.TryParse(textBoxDelivery.Text, out DateTime delivery); //TODO perkelt
            if (deliveryIsValid)
                order.Delivery = delivery;
            bool quantityIsValid = int.TryParse(textBoxQuantity.Text, out int quantity);
            if (quantityIsValid)
                order.Quantity = quantity;
            order.Address = textBoxAddress.Text;
            order.Receiver = textBoxReceiver.Text;
            order.SelfPickUp = checkBoxSelfPickUp.Checked;

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



        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedOrder = GetSelectedOrder();
            textBoxDelivery.Text = selectedOrder.Delivery.ToString();
            textBoxAddress.Text = selectedOrder.Address;
            textBoxQuantity.Text = selectedOrder.Quantity.ToString();
            textBoxReceiver.Text = selectedOrder.Receiver;
            checkBoxSelfPickUp.Checked = selectedOrder.SelfPickUp;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var selectedOrder = GetSelectedOrder();

            bool deliveryIsValid = DateTime.TryParse(textBoxDelivery.Text, out DateTime delivery); //TODO perkelt
            if (deliveryIsValid)
                selectedOrder.Delivery = delivery;
            bool quantityIsValid = int.TryParse(textBoxQuantity.Text, out int quantity);
            if (quantityIsValid)
                selectedOrder.Quantity = quantity;
            selectedOrder.Address = textBoxAddress.Text;
            selectedOrder.Receiver = textBoxReceiver.Text;
            selectedOrder.SelfPickUp = checkBoxSelfPickUp.Checked;
            selectedOrder.Edited = DateTime.UtcNow;

            SqliteDataAccess.EditOrder(selectedOrder);
            LoadOrders();
            RefreshScreen();
        }
        private Order GetSelectedOrder()
        {
            return dataGridView.SelectedRows[0].DataBoundItem as Order;
        }
    }
}
