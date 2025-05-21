using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bus_Reservation_System
{
    public partial class Form1 : Form
    {
        private List<string> reservations = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (reservations.Count == 0)
            {
                MessageBox.Show("No reservations to display.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Display each reservation in a more readable format
            int index = 1;
            foreach (string reservation in reservations)
            {
                // Parse the reservation string
                string[] parts = reservation.Split(new[] { "PassengerName:", "Destination:", "NumberOfSeats:", "TravelDate:", "ContactNumber:" },
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 5)
                {
                    string passengerName = parts[0].Trim();
                    string destination = parts[1].Trim();
                    string seats = parts[2].Trim();
                    string date = parts[3].Trim();
                    string contact = parts[4].Trim();

                    // Format the display string for better readability
                    string displayText = $"#{index}: {passengerName} - {destination} ({seats} seats) on {date}";
                    listBox1.Items.Add(displayText);
                }
                else
                {
                    // If the reservation data is corrupted, display as is
                    listBox1.Items.Add($"#{index}: {reservation}");
                }

                index++;
            }

            // Optional: Add ability to view details of selected reservation
            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                MessageBox.Show("Select a reservation to view details or delete.", "View Reservations",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < reservations.Count)
            {
                string reservation = reservations[listBox1.SelectedIndex];
                string[] parts = reservation.Split(new[] { "PassengerName:", "Destination:", "NumberOfSeats:", "TravelDate:", "ContactNumber:" },
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 5)
                {
                    string passengerName = parts[0].Trim();
                    string destination = parts[1].Trim();
                    string seats = parts[2].Trim();
                    string date = parts[3].Trim();
                    string contact = parts[4].Trim();

                    // Populate the form fields with the selected reservation details
                    textBox2.Text = passengerName;
                    comboBox1.Text = destination;
                    comboBox2.Text = seats;
                    dateTimePicker1.Value = DateTime.Parse(date);
                    textBox3.Text = contact;
                }
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string passengerName = textBox2.Text;
            string destination = comboBox1.SelectedItem.ToString();
            string numberOfSeats = comboBox2.SelectedItem.ToString();
            string travelDate =dateTimePicker1.Value.ToShortDateString();
            string contactNumber = textBox3.Text;

            if (string.IsNullOrWhiteSpace(passengerName) || 
                string.IsNullOrWhiteSpace(destination) || 
                string.IsNullOrWhiteSpace(numberOfSeats) || 
                string.IsNullOrWhiteSpace(travelDate))
            {
                MessageBox.Show("All fields are required to book!!!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string reservation = $"PassengerName:{passengerName}" +
                    $"Destination:{destination} NumberOfSeats:{numberOfSeats}" +
                    $"TravelDate:{travelDate} ContactNumber:{contactNumber}";
                reservations.Add(reservation);
                listBox1.Items.Add(reservation);

                MessageBox.Show("Reservation is Successfully created!!!", "Successfull",
                    MessageBoxButtons.OK);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >=0)
            {
                reservations.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                MessageBox.Show("Reseravation cleared Successfully!!!");
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                dateTimePicker1.Text = "";
            }
            else
            {
                MessageBox.Show("Reservation is not found");
            }
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim().ToLower();

            listBox1.Items.Clear();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a search term.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var matchedReservations = reservations
                .Where(res => res.ToLower().Contains(searchTerm))
                .ToList();

            if (matchedReservations.Count == 0)
            {
                MessageBox.Show("No matching reservations found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = 1;
            foreach (var reservation in matchedReservations)
            {
                string[] parts = reservation.Split(new[] { "PassengerName:", "Destination:", "NumberOfSeats:", "TravelDate:", "ContactNumber:" },
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 5)
                {
                    string passengerName = parts[0].Trim();
                    string destination = parts[1].Trim();
                    string seats = parts[2].Trim();
                    string date = parts[3].Trim();
                    string contact = parts[4].Trim();

                    string displayText = $"#{index}: {passengerName} - {destination} ({seats} seats) on {date}";
                    listBox1.Items.Add(displayText);
                }
                else
                {
                    listBox1.Items.Add($"#{index}: {reservation}");
                }

                index++;
            }

            MessageBox.Show($"Found {matchedReservations.Count} result(s) for '{searchTerm}'", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}

