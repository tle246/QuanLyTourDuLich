﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessEntity;
using DTO;
using System.Reflection;
using QuanLyTourDuLich.Presenters;
using QuanLyTourDuLich.Views;

namespace QuanLyTourDuLich.Forms
{
    public partial class frmTourGroupManager : Form, ITourGroupSearchView, ITransportView
    {
        TourGroupBUS _tourGroupBus;
        IEnumerable<TourGroup> _tourGroups;

        TourBUS _tourBus;
        IEnumerable<Tour> _tours;

        PassengerBUS _passengerBus;
        IEnumerable<GetAllPassengers_Result> _passengers;

        EmployeeTourGroupBUS _employeeTourGroupBus;
        IEnumerable<GetAllEmployeeTourGroup_Result> _employees;

        TransportTourGroupBUS _transportTourGroupBus;
        IEnumerable<GetAllTransportTourGroup_Result> _transports;

        CustomerBUS _customerBus;
        TransportBUS _transportBus;
        EmployeeBUS _employeeBus;

        ITourGroupSearchPresenter _tourGroupSearchPresenter;
        ITransportPresenter _transportPresenter;

 
        public frmTourGroupManager()
        {
            InitializeComponent();

            _tourGroupBus = new TourGroupBUS();

            _tourBus = new TourBUS();

            _passengerBus = new PassengerBUS();

            _customerBus = new CustomerBUS();

            _employeeTourGroupBus = new EmployeeTourGroupBUS();

            _transportTourGroupBus = new TransportTourGroupBUS();

            _transportBus = new TransportBUS();

            _employeeBus = new EmployeeBUS();

            _transportPresenter = new TransportPresenter(this);

            _tourGroupSearchPresenter = new TourGroupSearchPresenter(this);

            init();
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmTouristsGroupManager_Load(object sender, EventArgs e)
        {
            _tourCb.DisplayMember = "name";
            _tourCb.ValueMember = "id";

            _tourCb2.DisplayMember = "name";
            _tourCb2.ValueMember = "id";

            _tourGroupCb.DisplayMember = "name";
            _tourGroupCb.ValueMember = "id";

            _departDateDtp.MinDate = DateTime.Now;
            _returnDateDtp.MinDate = DateTime.Now;

            _transportClb.DataSource = _searchtransportBs;                // todo

            _transportPresenter.loadAllActiveTransports();

            _tourGroupSearchPresenter.loadMinDepartDate();
            _tourGroupSearchPresenter.loadMaxReturnDate();
        }

        private void _tourCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            _filterBs.Filter = "tour_id = " + _tourCb.SelectedValue;
        }

        private void _tourGroupDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = e.RowIndex + 1;
            }
            if (e.ColumnIndex == 0 && e.Value != null && (Boolean)e.Value == true)
            {
                _tourGroupDgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
            }
            else if (e.ColumnIndex == 0 && e.Value != null)
            {
                _tourGroupDgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btnCSDL_Click(object sender, EventArgs e)
        {
            if (_groupNameTb.Text.Length <= 0)
            {
                MessageBox.Show("Tên đoàn không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
      
            if (_groupNameTb.Text.Length >= 1)
            {
                TourGroup newTourGroup = new TourGroup();
                newTourGroup.name = _groupNameTb.Text;
                newTourGroup.tour_id = int.Parse(_tourCb.SelectedValue.ToString());
                newTourGroup.depart_date = _departDateDtp.Value;
                newTourGroup.return_date = _returnDateDtp.Value;

                _tourGroupBus.add(newTourGroup);

                init();
            }
        }

        private void _departDateDtp_ValueChanged(object sender, EventArgs e)
        {
            _returnDateDtp.MinDate = _departDateDtp.Value;
        }

        private void init()
        {
            _tourGroups = _tourGroupBus.getEntries();
            _tourGroupBs.DataSource = _tourGroups;

            _filterBs.DataSource = _tourGroups.ToDataTable();

            _tours = _tourBus.getEntries();
            _tourBs.DataSource = _tours;


            _passengers = _passengerBus.getEntries();
            _passengerBs.DataSource = _passengers.ToDataTable();

            _employees = _employeeTourGroupBus.getAllEmployeeTourGroup();
            _employeeBs.DataSource = _employees.ToDataTable();

            _transports = _transportTourGroupBus.getAllTransportTourGroup();
            _transportBs.DataSource = _transports.ToDataTable();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa những dòng đã chọn?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                foreach(DataGridViewRow row in _tourGroupDgv.Rows) {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    {
                        _tourGroupBus.deletaById(int.Parse(row.Cells[2].Value.ToString()));
                    }
                }
                init();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _tourGroupCb_SelectedValueChanged(object sender, EventArgs e)
        {
            _passengerBs.Filter = "tour_group_id = " + _tourGroupCb.SelectedValue;
            _transportBs.Filter = "tour_group_id = " + _tourGroupCb.SelectedValue;
            _employeeBs.Filter = "tour_group_id = " + _tourGroupCb.SelectedValue;
        }

        private void _passengerDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void _transportDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void _employeeDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void _addCustomerBtn_Click(object sender, EventArgs e)
        {
            if (_customerIdTb.Text.Length <=0 )
            {
                MessageBox.Show("Mã khách hàng không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_customerBus.isExists(int.Parse(_customerIdTb.Text)))
            {
                MessageBox.Show("Mã khách hàng không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _passengerBus.addPassenger(int.Parse(_customerIdTb.Text), int.Parse(_tourGroupCb.SelectedValue.ToString()));
                init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại trong đoàn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _customerDeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa những dòng đã chọn?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in _passengerDgv.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    {
                        _passengerBus.deletePassenger(int.Parse(row.Cells[2].Value.ToString()), int.Parse(_tourGroupCb.SelectedValue.ToString()));
                    }
                }
                init();
            }
        }

        private void _addTransportBtn_Click(object sender, EventArgs e)
        {
            if (_transportIdTb.Text.Length <= 0)
            {
                MessageBox.Show("Mã phương tiện không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_transportBus.isExists(int.Parse(_transportIdTb.Text)))
            {
                MessageBox.Show("Mã phương tiện không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _transportTourGroupBus.addTransportTourGroup(int.Parse(_transportIdTb.Text), int.Parse(_tourGroupCb.SelectedValue.ToString()));
                init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mã phương tiện đã tồn tại trong đoàn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _transportDeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa những dòng đã chọn?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in _transportDgv.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    {
                        _transportTourGroupBus.deleteTransportTourGroup(int.Parse(row.Cells[2].Value.ToString()), int.Parse(_tourGroupCb.SelectedValue.ToString()));
                    }
                }
                init();
            }
        }

        private void _addEmployeeBtn_Click(object sender, EventArgs e)
        {
            if (_employeeIdTb.Text.Length <= 0)
            {
                MessageBox.Show("Mã nhân viên không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_employeeBus.exists(int.Parse(_employeeIdTb.Text)))
            {
                MessageBox.Show("Mã nhân viên không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _employeeTourGroupBus.addEmployeeTourGroup(int.Parse(_employeeIdTb.Text), int.Parse(_tourGroupCb.SelectedValue.ToString()));
                init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mã nhân viên đã tồn tại trong đoàn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _employeeDeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa những dòng đã chọn?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in _employeeDgv.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    {
                        _employeeTourGroupBus.deleteEmployeeTourGroup(int.Parse(row.Cells[2].Value.ToString()), int.Parse(_tourGroupCb.SelectedValue.ToString()));
                    }
                }
                init();
            }
        }

        private void _returnDateDtp_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        #region Tab Tour Group

        private void _startDtp_ValueChanged(object sender, EventArgs e)
        {
            var mindate = _startDtp.Value;
            var maxdate = _endDtp.Value;
            _tourGroupSearchPresenter.filterDate(new DateTime[] { mindate, maxdate });
        }

        private void _endDtp_ValueChanged(object sender, EventArgs e)
        {
            var mindate = _startDtp.Value;
            var maxdate = _endDtp.Value;
            _tourGroupSearchPresenter.filterDate(new DateTime[] { mindate, maxdate });
        }

        public void updateMinDepartDate(DateTime minValue)
        {
            _startDtp.Value = minValue;
        }

        public void udpateMaxReturnDate(DateTime minValue)
        {
            _endDtp.Value = minValue;
        }

        public void upateTransportsView(IEnumerable<Transport> transports)
        {
            _searchtransportBs.DataSource = transports;
            _transportClb.DisplayMember = "name";       // todo

        }

        private void _transportClb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = _transportClb.CheckedItems.Cast<Transport>();
            _tourGroupSearchPresenter.filterTransport(selected);
        }

        private void _tourGroupKeywordTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.handleSearchTourGroupClick(_tourGroupKeywordTb.Text);
        }

        private void tourGroupSearchBtn_Click(object sender, EventArgs e)
        {
            var keyword = _tourGroupKeywordTb.Text.Trim();
            this.handleSearchTourGroupClick(keyword);
        }

        private void _tourGroupGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            var senderGrid = sender as DataGridView;
            if (senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
            {
                if (e.ColumnIndex == 6)
                {
                    var tourGroup = senderGrid.CurrentRow.DataBoundItem as TourGroup;
                    new fmTourGroupPassengers(tourGroup).ShowDialog();
                }
                if (e.ColumnIndex == 7)
                {
                    // ds nhân viên
                }
                if (e.ColumnIndex == 8)
                {
                    // ds phương tiện
                }
            }
        }

        private void _tourGroupGv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex + 1;
            }
            if (e.Value == null)
                return;
            if (e.ColumnIndex == 3)
            {
                e.Value = (e.Value as Tour).name;
            }
            if (e.ColumnIndex == 6)
            {
                //if (e.Value == null)
                //    e.Value = 0;
                //else
                //    e.Value = (e.Value as IEnumerable<Customer>).Count().ToString();
            }
        }

        private void _tourGroupGv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
                _tourGroupSearchPresenter.sortByTourGrouId();
            else if (e.ColumnIndex == 2)
                _tourGroupSearchPresenter.sortByTourGroupName();
            else if (e.ColumnIndex == 3)
                _tourGroupSearchPresenter.sortByTourName();
            else if (e.ColumnIndex == 4)
                _tourGroupSearchPresenter.sortByDepartDate();
            else if (e.ColumnIndex == 5)
                _tourGroupSearchPresenter.sortByReturnDate();
            //else if (e.ColumnIndex == 6)
            //    _tourGroupSearchPresenter.sortByEmployees();

        }

        public void handleSearchTourGroupClick(string keyword)
        {
            _tourGroupSearchPresenter.handleSearchClick(keyword);
        }

        public void updateSearchResult(IEnumerable<TourGroup> searchResult)
        {
            _searchTourGroupBs.DataSource = searchResult;
        }
        #endregion

    }
}
