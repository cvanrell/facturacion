﻿using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.DataModel.Queries;
using WIS.Billing.BusinessLogicCore.DataModel.Repositories;
using WIS.Billing.DataAccessCore.Database;


namespace WIS.Billing.BusinessLogicCore.DataModel
{
    public class UnitOfWork : IDisposable
    {        
        
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;

        private IDbContextTransaction Transaction;

        
        public ClientRepository ClientRepository { get; set; }
        public ProjectRepository ProjectRepository { get; set; }
        public AdjustmentRepository AdjustmentRepository { get; set; }
        public FeeRepository FeeRepository { get; set; }
        public SupportRepository SupportRepository { get; set; }
        public BillingRepository BillingRepository { get; set; }
        public GridConfigRepository GridConfigRepository { get; set; }



        public UnitOfWork(string application, int userId)
        {
            this._context = new WISDB();
            this._application = application;
            this._userId = userId;
            
            this.ClientRepository = new ClientRepository(this._context, this._application, this._userId);
            this.ProjectRepository = new ProjectRepository(this._context, this._application, this._userId);
            this.AdjustmentRepository = new AdjustmentRepository(this._context, this._application, this._userId);
            this.FeeRepository = new FeeRepository(this._context, this._application, this._userId);
            this.SupportRepository = new SupportRepository(this._context, this._application, this._userId);
            this.BillingRepository = new BillingRepository(this._context, this._application, this._userId);
            this.GridConfigRepository = new GridConfigRepository(this._context, this._application, this._userId);
        }

        public void BeginTransaction()
        {
            this.Transaction = this._context.Database.BeginTransaction();           
        }

        public IQueryable<T> BuildQuery<T>(IQueryObject<T> query)
        {
            return query.BuildQuery(this._context);
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
