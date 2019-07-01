using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.DataModel.Mappers;
using WIS.BusinessLogic.WMS;
using WIS.Persistance.Database;

namespace WIS.BusinessLogic.DataModel.Repositories
{
    public class ProductoRepository
    {
        private readonly WISDB _context;
        private readonly string _cdAplicacion;
        private readonly int _userId;
        private readonly ProductoMapper _mapper;

        public ProductoRepository(WISDB context, string cdAplicacion, int userId)
        {
            this._context = context;
            this._cdAplicacion = cdAplicacion;
            this._userId = userId;
            this._mapper = new ProductoMapper();
        }

        public Producto Get(int cdEmpresa, string cdProducto)
        {
            var productoEntity = this._context.T_PRODUTO.Where(d => d.CD_EMPRESA == cdEmpresa && d.CD_PRODUTO == cdProducto)
                .AsNoTracking().FirstOrDefault();

            return this._mapper.MapToObject(productoEntity);
        }
    }
}
