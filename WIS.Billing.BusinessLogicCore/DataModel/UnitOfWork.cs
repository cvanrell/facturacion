using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.DataModel.Queries;
using WIS.BusinessLogicCore.DataModel.Repositories;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.DataAccessCore.Extensions;

namespace WIS.BusinessLogicCore.DataModel
{
    public class UnitOfWork : IDisposable
    {        
        
        private readonly WISDB _context;
        private readonly string _appplication;
        private readonly int _userId;

        private DbContextTransaction Transaction;

        public EmpresaRepository EmpresaRepository { get; set; }
        public DocumentoRepository DocumentoRepository { get; set; }
        public ProductoRepository ProductoRepository { get; set; }        

        public UnitOfWork(string application, int userId)
        {
            this._context = new WISDB();
            this._appplication = application;
            this._userId = userId;

            this.EmpresaRepository = new EmpresaRepository(this._context, this._appplication, this._userId);
            this.DocumentoRepository = new DocumentoRepository(this._context, this._appplication, this._userId);
            this.ProductoRepository = new ProductoRepository(this._context, this._appplication, this._userId);
        }

        public void BeginTransaction()
        {
            this.Transaction = this._context.Database.BeginTransaction();           
        }

        public void Commit()
        {
            if(this.Transaction != null)
                this.Transaction.Commit();
        }
        public void Rollback()
        {
            if (this.Transaction != null)
                this.Transaction.Rollback();
        }

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        public void Dispose()
        {
            if(this.Transaction != null)
                this.Transaction.Dispose();

            this._context.Dispose();            
        }
    }
}
