using GlamoraHairdresser.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.SalonForms
{
    public partial class SalonForm : Form
    {
        private readonly GlamoraDbContext _db;
        private readonly BindingSource _bs = new();
        public SalonForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;
            this.Load += SalonForm_Load;

        }

        private void SalonForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _db.ChangeTracker.Clear();            // تنظيف أي تتبع قديم
            _db.Salons.Load();                    // تحميل بيانات جدول Salons من قاعدة البيانات
            _bs.DataSource = _db.Salons.Local.ToBindingList();
            dataGridViewSalon.DataSource = _bs;       // ربط البيانات بالـGridView
        }
    }
}
