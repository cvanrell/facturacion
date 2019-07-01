using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.DataModel.Mappers;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.DataModel.Repositories
{
    public class EmpresaRepository
    {
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;
        private readonly EmpresaMapper _mapper;

        public EmpresaRepository(WISDB context, string application, int userId)
        {
            this._context = context;
            this._application = application;
            this._userId = userId;
            this._mapper = new EmpresaMapper();
        }

        public string GetNombre(int cdEmpresa)
        {
            return this._context.T_EMPRESA.Where(d => d.CD_EMPRESA == cdEmpresa).Select(d => d.NM_EMPRESA).FirstOrDefault();
        }
    }
}
